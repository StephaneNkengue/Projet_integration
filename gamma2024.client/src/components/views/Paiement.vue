<template>
    <div class="d-flex w-100 justify-content-center flex-column align-items-center">
        <h1>Paiement</h1>

        <div class="row w-100">
            <form id="payment-form">
                <div id="payment-element">
                    <!--Stripe.js injects the Payment Element-->
                </div>
                <button id="submit" class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond">
                    <div class="spinner hidden" id="spinner"></div>
                    <span id="button-text">Pay now</span>
                </button>
                <div id="payment-message" class="hidden"></div>
            </form>
        </div>
    </div>
</template>

<script setup>
    import { onMounted } from "vue"
    import { useStore } from "vuex"

    const store = useStore();
    const props = defineProps({
        idFacture: String,
    });
    let elements;
    let stripe;

    onMounted(async () => {
        stripe = Stripe("pk_test_51QFxRwDmrnnrqPfFn5CtjHhvCblvpE3iKzknTreLsdR4tA4eotRBtTBlRKwWJbbwhuqszvIMKTgcILWEskhWYlP900bvv1U1ly");
        initialize();

        document.querySelector("#payment-form").addEventListener("submit", handleSubmit);
    })

    async function initialize() {
        const response = await store.dispatch("creerPaymentIntent", props.idFacture)
        const clientSecret = response.data.clientSecret;

        const appearance = {
            theme: 'stripe',
        };
        elements = stripe.elements({ appearance, clientSecret });

        const paymentElementOptions = {
            layout: "tabs",
        };

        const paymentElement = elements.create("payment", paymentElementOptions);
        paymentElement.mount("#payment-element");
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setLoading(true);

        const { error } = await stripe.confirmPayment({
            elements,
            confirmParams: {
                // Make sure to change this to your payment completion page
                return_url: "http://localhost:4242/complete.html",
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

    // Show a spinner on payment submission
    function setLoading(isLoading) {
        if (isLoading) {
            // Disable the button and show a spinner
            document.querySelector("#submit").disabled = true;
            document.querySelector("#spinner").classList.remove("hidden");
            document.querySelector("#button-text").classList.add("hidden");
        } else {
            document.querySelector("#submit").disabled = false;
            document.querySelector("#spinner").classList.add("hidden");
            document.querySelector("#button-text").classList.remove("hidden");
        }
    }

    function setDpmCheckerLink(url) {
        document.querySelector("#dpm-integration-checker").href = url;
    }
</script>

<style scoped></style>