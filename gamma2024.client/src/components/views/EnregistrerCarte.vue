<template>
    <div class="d-flex w-100 justify-content-center flex-column align-items-center">
        <h1>Enregistrer votre carte</h1>

        <div class="row w-100">
            <form id="payment-form">
                <div class="d-flex align-items-center justify-content-center gap-3" v-if="chargement">
                    <div class="spinner-border" role="status">
                    </div>Chargement...
                </div>
                <div id="payment-element">
                    <!--Stripe.js injects the Payment Element-->
                </div>
                <button id="submit" class="btn btn-outline bleuMoyenFond text-white btnSurvolerBleuMoyenFond my-2">
                    <div class="spinner-grow d-none" id="spinner"></div>
                    <span id="button-text">Enregistrer</span>
                </button>
                <div id="payment-message" class="hidden"></div>
            </form>
        </div>
    </div>
</template>

<script setup>
    import { onMounted, ref } from "vue"
    import { useStore } from "vuex"

    const store = useStore();
    const props = defineProps({
        idFacture: String,
    });
    let elements;
    let stripe;
    const chargement = ref(true);

    onMounted(async () => {
        stripe = Stripe(import.meta.env.VITE_STRIPE_PK);
        initialize()

        document.querySelector("#payment-form").addEventListener("submit", handleSubmit);
    })

    async function initialize() {
        const response = await store.dispatch("creerSetupIntent")
        const clientSecret = response.data.clientSecret;

        const appearance = {
            theme: 'stripe',
        };
        elements = stripe.elements({ appearance, clientSecret, locale: "fr", loader: 'always' });

        const paymentElementOptions = {
            layout: "tabs",
        };

        const paymentElement = elements.create("payment", paymentElementOptions);
        paymentElement.mount("#payment-element");
        chargement.value = false
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setLoading(true);

        const { error } = await stripe.confirmSetup({
            elements,
            confirmParams: {
                // Make sure to change this to your payment completion page
                return_url: "https://sqlinfocg.cegepgranby.qc.ca/2162067/",
            },
        });

        // This point will only be reached if there is an immediate error when
        // confirming the payment. Otherwise, your customer will be redirected to
        // your `return_url`. For some payment methods like iDEAL, your customer will
        // be redirected to an intermediate site first to authorize the payment, then
        // redirected to the `return_url`.
        if (error.type === "card_error" || error.type === "validation_error") {
            showMessage(error.message);
        } else {
            showMessage("An unexpected error occurred.");
        }

        setLoading(false);
    }

    function showMessage(messageText) {
        const messageContainer = document.querySelector("#payment-message");

        messageContainer.classList.remove("hidden");
        messageContainer.textContent = messageText;

        setTimeout(function () {
            messageContainer.classList.add("hidden");
            messageContainer.textContent = "";
        }, 4000);
    }

    function setLoading(isLoading) {
        if (isLoading) {
            document.querySelector("#submit").classList.add("disabled")
            document.querySelector("#spinner").classList.remove("d-none");
            document.querySelector("#button-text").classList.add("d-none");
        } else {
            document.querySelector("#submit").classList.remove("disabled");
            document.querySelector("#spinner").classList.add("d-none");
            document.querySelector("#button-text").classList.remove("d-none");
        }
    }
</script>

<style scoped>
</style>