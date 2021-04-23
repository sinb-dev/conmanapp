<template><div>
    {{message}}
  <setup v-if="!$root.configured() || $root.verified < 1" v-bind:token_id="$root.settings.key_token.tokenId"></setup>
  <div v-else >
      <a v-if="$root.verified == 2" class="ui button" href="#/tokens">API Nøgler</a>
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
        }
    },
    props : {
        configured: Boolean
    },

    requireSetup : true,
}
</script>

<style>

</style>