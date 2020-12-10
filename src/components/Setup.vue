<template>
<div>
    <h1>Setup</h1>
    <div class="ui input"><input type="text" v-model="key" placeholder="Your key" /></div>
    <button class="ui button" @click="saveSetup">Save</button>
</div>
</template>

<script>
export default {
    props : {
        key_token : String
    },
    data() {
        return {
            key : "damN!",
        }
    },
    methods : {
        saveSetup() {
            this.$root.settings.key_token = this.key;
            this.$root.save();
        }
    },
    mounted() {
        this.key = this.key_token;
        console.log("setup " + this.key_token);
    }
}
</script>

<style>

</style>