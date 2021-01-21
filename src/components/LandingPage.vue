<template><div>
    {{message}}
  <setup v-if="!$root.configured || !$root.verified" v-bind:key_token="$root.settings.key_token.tokenId"></setup>
  <div v-else>
      <a class="ui button" href="#/tokens">API Nøgler</a>
      <container-list></container-list>
  </div>
  </div>
</template>

<script>
import ContainerList from "./ContainerList.vue";
import Setup from "./Setup.vue";
export default {
    components : {
        'container-list' : ContainerList,
        Setup
    },
    data() {
        return {
            message : this.message,
            key_token : ""
        }
    },
    props : {
        configured: Boolean
    },
    mounted() {
        this.key_token = this.$root.tokenId();
        console.log("mounted token: "+this.key_token)
    },
    requireSetup : true,
}
</script>

<style>

</style>