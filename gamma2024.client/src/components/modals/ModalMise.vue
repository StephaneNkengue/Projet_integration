<template>
    <div class="modal fade"
         :id="`modalMise_${lot.id}`"
         tabindex="-1"
         :aria-labelledby="`modalMiseLabel_${lot.id}`"
         aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" :id="`modalMiseLabel_${lot.id}`">
                        Êtes-vous sûr de faire cette mise?
                    </h5>
                    <button type="button"
                            class="btn-close"
                            data-bs-dismiss="modal"
                            aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="d-flex flex-column gap-3">
                        <p class="fs-5 mb-0">lot {{ lot.numero }}</p>
                        <p class="fs-5 mb-0">votre mise : {{ affichageMiseActuelle }}</p>

                        <div class="d-flex flex-column gap-2">
                            <p class="mb-0">Ma mise maximale à ne pas dépasser :</p>
                            <div class="d-flex align-items-center gap-2">
                                <button class="btn btn-outline-secondary"
                                        @click="decrementerMise"
                                        :disabled="montantMise <= getMiseMinimale">
                                    -{{ pasEnchere }}$
                                </button>
                                <span class="fs-5">{{ montantMise }}$</span>
                                <button class="btn btn-outline-secondary"
                                        @click="incrementerMise">
                                    +{{ pasEnchere }}$
                                </button>
                            </div>
                            <p class="text-muted small">
                                Ajouter le montant maximum souhaité
                            </p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button"
                            class="btn btn-secondary"
                            data-bs-dismiss="modal">
                        Annuler
                    </button>
                    <button type="button"
                            class="btn btn-primary"
                            @click="confirmerMise"
                            :disabled="!isMiseValide">
                        Miser
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, computed, onMounted, h, watch } from "vue";
    import { useStore } from "vuex";
    import { useRouter } from "vue-router";
    import { toast } from "vue3-toastify";
    import ToastContent from "../Toast/toastConfirm.vue";

    const props = defineProps({
        lot: {
            type: Object,
            required: true,
        },
    });

    const store = useStore();
    const router = useRouter();
    const montantMise = ref(0);
    let modalInstance = null;

    const userHasBid = computed(() => {
        return store.getters.hasUserBidOnLot(props.lot.id);
    });

    const getMiseInitiale = computed(() => {
        return props.lot.mise ? props.lot.mise : props.lot.prixOuverture;
    });

    // Ajout des computed properties pour le montant initial et le pas
    const montantInitial = computed(() => {
        // Récupérer la mise actuelle du lot depuis le store, peu importe qui a misé
        const lotStore = store.getters.getLot(props.lot.id);
        return lotStore?.mise || props.lot.mise || props.lot.prixOuverture || 0;
    });

    const pasEnchere = computed(() => {
        const miseActuelle = props.lot?.mise || 0;
        // Définir les paliers pour le pas d'enchère
        if (miseActuelle < 100) return 5;
        if (miseActuelle < 500) return 25;
        if (miseActuelle < 1000) return 50;
        if (miseActuelle < 5000) return 100;
        return 250;
    });

    const getMiseMinimale = computed(() => {
        // Si la mise actuelle est 0, utiliser le prix d'ouverture
        const miseBase =
            props.lot.mise > 0 ? props.lot.mise : props.lot.prixOuverture;
        return miseBase + pasEnchere.value;
    });

    const isMiseValide = computed(() => {
        return montantMise.value >= getMiseMinimale.value;
    });

    const incrementerMise = () => {
        montantMise.value += pasEnchere.value;
    };

    const decrementerMise = () => {
        const nouveauMontant = montantMise.value - pasEnchere.value;
        if (nouveauMontant >= montantInitial.value + pasEnchere.value) {
            montantMise.value = nouveauMontant;
        }
    };

    const confirmerMise = async () => {
        try {
            const response = await store.dispatch("placerMise", {
                idLot: props.lot.id,
                montant: montantMise.value,
            });

            if (response.success) {
                // Fermer le modal
                modalInstance.hide();

                // Émettre l'événement
                emit("miseConfirmee", montantMise.value);

                // Forcer la mise à jour du montant pour la prochaine ouverture
                props.lot.mise = montantMise.value;

                // Afficher le toast de succès
                toast.success(
                    h(ToastContent, {
                        title: "Succès",
                        description: "Votre mise a été placée avec succès",
                    })
                );
            }
        } catch (error) {
            toast.error(
                h(ToastContent, {
                    title: "Erreur",
                    description: error.message || "Erreur lors de la mise",
                })
            );
        }
    };

    onMounted(() => {
        const modalElement = document.getElementById(`modalMise_${props.lot.id}`);
        modalInstance = new bootstrap.Modal(modalElement);
        // Même logique que pour show()
        if (!props.lot.mise) {
            montantMise.value = props.lot.prixOuverture;
        } else {
            montantMise.value = montantInitial.value + pasEnchere.value;
        }
    });

    defineExpose({
        show: () => {
            // Si aucune mise n'a été faite, utiliser le prix d'ouverture directement
            if (!props.lot.mise) {
                montantMise.value = props.lot.prixOuverture;
            } else {
                // S'il y a déjà une mise, ajouter le pas d'enchère
                const miseActuelle = montantInitial.value;
                montantMise.value = miseActuelle + pasEnchere.value;
            }
            modalInstance.show();
        },
        hide: () => modalInstance.hide(),
    });

    // Ajouter un watch pour surveiller les changements de mise
    watch(
        () => props.lot.mise,
        (newMise) => {
            if (newMise) {
                montantMise.value = newMise + pasEnchere.value;
            }
        }
    );

    // Définir les émissions au début du script
    const emit = defineEmits(["miseConfirmee"]);

    // L'affichage de "votre mise" reste conditionnel à l'utilisateur actuel
    const affichageMiseActuelle = computed(() => {
        return hasUserBidOnLot.value ? `${props.lot.mise}$` : "aucune mise";
    });

    const hasUserBidOnLot = computed(() => {
        return store.getters.hasUserBidOnLot(props.lot.id);
    });
</script>

<style scoped>
    .modal-body {
        padding: 1.5rem;
    }

    .gap-3 {
        gap: 1rem !important;
    }

    .gap-2 {
        gap: 0.75rem !important;
    }
</style>
