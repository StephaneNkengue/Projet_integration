<template>
    <div class="d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-between">
            <h2 class="d-flex-1">Liste des ventes</h2>
        </div>

        <div class="d-flex flex-row-reverse me-1 gap-1 align-items-center">

            <input data-bs-theme="light"
                   type="search"
                   aria-label="Recherche" /><label for="Recherche">Rechercher: </label>
        </div>
        <div class="card cardtest">
            <Tabs value="0">
                <TabList>
                    <Tab class="tab" @click="voirTabs='parEncan'">Par encan</Tab>
                    <Tab class="tab" @click="voirTabs='parClient'">Par client</Tab>
                    <Tab class="tab" @click="voirTabs='parDate'">Par date</Tab>
                </TabList>
                <TabPanels v-if="voirTabs=='parEncan'">
                    <TabPanel>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th scope="col">Encan</th>
                                    <th scope="col">Client</th>
                                    <th scope="col">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th scope="row">1</th>
                                    <td>Mark</td>
                                    <td>
                                        <button class="btn btn-info" @click="detailsDuMembre(membre.id)">
                                            <img src="/images/ice.png" class="img-fluid" alt="..." />
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </TabPanel>
                </TabPanels>
                <TabPanels v-if="voirTabs=='parClient'">
                    <TabPanel>
                        <p class="m-0">bb</p>
                    </TabPanel>
                </TabPanels>
                <TabPanels v-if="voirTabs=='parDate'">
                    <TabPanel>
                        <p class="m-0">cc</p>
                    </TabPanel>
                </TabPanels>
            </Tabs>
        </div>
    </div>
</template>

<script setup>
    import Tabs from 'primevue/tabs';
    import TabList from 'primevue/tablist';
    import Tab from 'primevue/tab';
    import TabPanels from 'primevue/tabpanels';
    import TabPanel from 'primevue/tabpanel';
    import { onMounted, ref } from "vue";

    const voirTabs = ref("");

    onMounted(async () => {
        voirTabs.value = "parEncan";

        var btns = document.getElementsByClassName("tab");
        for (var i = 0; i < btns.length; i++) {
            btns[0].className += " active";

            btns[i].addEventListener("click", function () {
                var current = document.getElementsByClassName("active");
                current[0].className = current[0].className.replace(" active", "");
                this.className += " active";
            });
        }
    });
</script>

<style scoped>
    img {
        width: 25px;
        height: 30px;
    }

    table,
    input {
        box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 3px 5px 0 rgba(0, 0, 0, 0.19);
    }

    th {
        font-size: 18px;
    }

    td {
        font-size: 16px;
    }

    .p-tab {
        text-align: center;
        border-radius: 10px 10px 0px 0px;
        width: 100px;
        margin-right: 1px;
        border: none;
        background-color: #5A708A;
        color: white;
    }

    .p-tab:hover {
        background-color: #83a0ba;
        color: white;
    }

    .p-tab:not(.p-tab-active):not(.p-disabled):hover{
        color: white;
        background-color: #83a0ba;
    }

    .active {
        background-color: #1C3755;
    }

    .cardtest {
        --bs-card-border-color: none;
        margin: 10px;
    }

    table {
        box-shadow: none;
    }
</style>