<template><div>
    
  <setup v-if="!$root.configured" v-bind:key_token="$root.settings.key_token"></setup>
  <container-list v-else></container-list>
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
            key_token : ""
        }
    },
    props : {
        configured: Boolean
    },
    mounted() {
        this.key_token = this.$root.settings.key_token;
        console.log("mounted token: "+this.key_token)
    }
}
</script>

<style>

</style>