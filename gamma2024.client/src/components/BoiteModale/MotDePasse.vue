<template>

    <div class="modal fade" :id="props.h.idModal" tabindex="-1" role="dialog" aria-labelledby="passwordModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document">
            <form @submit.prevent="handleSubmit">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="passwordModalLabel">
                            Changer le mot de passe
                        </h5>

                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="currentPassword" class="form-label">Mot de passe actuel:</label>
                            <input type="password"
                                   class="form-control"
                                   id="currentPassword"
                                   v-model="form.currentPassword" />
                        </div>
                        <div class="mb-3">
                            <label for="newPassword" class="form-label">Nouveau mot de passe:</label>
                            <input type="password"
                                   :class="[
                    'form-control',
                    { 'is-invalid': v.form.newPassword.$error },
                  ]"
                                   id="newPassword"
                                   v-model="form.newPassword"
                                   placeholder="Nouveau mot de passe"
                                   @blur="v.form.newPassword.$touch()" />
                            <div class="invalid-feedback" v-if="v.form.newPassword.$error">
                                {{ v.form.newPassword.$errors[0].$message }}
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="confirmPassword" class="form-label">Confirmation du mot de passe:</label>
                            <input type="password"
                                   id="confirmPassword"
                                   v-model="form.confirmPassword"
                                   placeholder="Confirmer le mot de passe"
                                   :class="[
                    'form-control',
                    { 'is-invalid': v.form.confirmPassword.$error },
                  ]"
                                   @blur="v.form.confirmPassword.$touch()" />
                            <div class="invalid-feedback"
                                 v-if="v.form.confirmPassword.$error">
                                {{ v.form.confirmPassword.$errors[0].$message }}
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button"
                                class="btn btn-secondary"
                                data-dismiss="modal">
                            Annuler
                        </button>
                        <button type="submit" class="btn btn-primary">
                            Sauvegarder
                        </button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<script setup>
    import { reactive, computed } from "vue";
    import { required, helpers, minLength, sameAs } from "@vuelidate/validators";
    import { useVuelidate } from "@vuelidate/core";


    const props = defineProps({
        h: {
            type: Object,
            default: () => ({}),
        },
    });

    const messageRequis = helpers.withMessage("Ce champ est requis", required);
    const passwordMinLength = helpers.withMessage(
        "Le mot de passe doit contenir au moins 8 caractères, minimum 1 majuscule, 1 minuscule, 1 chiffre et 1 caractère spécial.",
        minLength(8)
    );

    let form = reactive({
        currentPassword: props.h.dataCurrentPassword,
        newPassword: "",
        confirmPassword: "",
    });

    const messageSameAsPassword = helpers.withMessage(
        "Les mots de passe ne correspondent pas.",
        sameAs(computed(() => form.newPassword))
    );

    let rules = computed(() => {
        return {
            form: {
                currentPassword: {},
                newPassword: {
                    required: messageRequis,
                    minLength: passwordMinLength,
                },
                confirmPassword: {
                    required: messageRequis,
                    sameAsPassword: messageSameAsPassword,
                },
            },
        };
    });

    const v = useVuelidate(rules, form);

    const handleSubmit = async () => {
        const result = await v.value.$validate();
        if (result) {
            form.currentPassword = "";
            form.newPassword = "";
            form.confirmPassword = "";

            document.getElementById('saveChangesBtn').addEventListener('click', function () {
                $('#exampleModal').modal('hide');
            });
        } else {
            return;
        }
    };

</script>

<style scoped>
    .is-invalid,
    span {
        border-color: red;
        font-weight: 600;
    }
</style>
