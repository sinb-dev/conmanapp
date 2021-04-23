<template>
<div>
    <h1>Setup</h1>
    <div v-if="key == ''">
    A token is required to work with containers
    </div>
    <div v-else-if="$root.verified == -1">
    Your token is invalid. Enter a valid token or request a new token
    </div>
    <div v-else-if="$root.verified == 0">
    Your token is awaiting approval
    </div>
    <div v-else>
    This is a valid token
    </div>
    <div class="ui left icon input">
        <input type="text" v-model="key" @change="changed" placeholder="Your API key" />
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
            document.location.hash="#token/request"
        },
        changed() {
            this.saveSetup();
        }
    },
    mounted() {
        this.key = this.$root.tokenId();
        console.log("setup " + this.key);
    }
}
</script>

<style>

</style>