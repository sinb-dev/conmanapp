<template>
  <token-list v-bind:tokens="tokens"></token-list>
</template>

<script>
import tokenlist from './TokenList.vue'
export default {
    components : {
        'token-list' : tokenlist,
    },
    data() {
        return {
            tokens : [{}]
        }
    },
    mounted() {
        /*axios.get('/path/to/my/request', { params: { id: id }})
        .then(response => {
            console.log(response)
        }).catch(error => {
            console.log(response)
        })*/
    }
}
</script>

<style>

</style>