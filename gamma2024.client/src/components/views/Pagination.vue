<template>
    <div class="d-flex flex-row justify-content-around gap-2 flex-wrap p-3">
        <button type="button" class="btn btn-primary"><</button>

        <div v-for="index in nbPages">
            <button type="button"
                    class="btn btn-primary"
                    v-bind:id="index"
                    @click="$emit('nouvellePageCourante', $event.srcElement.id)">
                {{index}}
            </button>
        </div>

        <button type="button" class="btn btn-primary">></button>
    </div>
</template>

<script setup>
    const props = defineProps({
        nbPages: Number,
    });
</script>

<style scoped>
</style>