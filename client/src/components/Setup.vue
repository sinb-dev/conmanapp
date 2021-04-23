<template>
<div>
    <h1>Setup</h1>
    <div v-if="key_token == ''">
    A token is required to work with containers
    </div>
    <div v-else-if="!$root.verified">
    Your token is invalid. Enter a valid token or request a new token
    </div>
    <div class="ui left icon input">
        <input type="text" v-model="key" placeholder="Your API key" />
        <i class="key icon"></i>
    </div>&nbsp;
    <div class="ui buttons">
        <button class="ui button" @click="saveSetup">Save</button>
        <div class="or"></div>
        <button class="ui positive button" @click="requestKey">Request new</button>
    </div>
</div>
</template>

<script>
export default {
    data() {
        return {
            key : "",
        }
    },
    methods : {
        saveSetup() {
            this.$root.settings.key_token.tokenId = this.key;
            this.$root.save();
        },
        requestKey() {
            document.location.hash="#request"
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