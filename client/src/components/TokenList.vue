<template>
  <div>
    <div v-for="token in tokens" v-bind:key="token.tokenId">{{token.tokenId}}</div>
  </div>
</template>

<script>
export default {
    props : [
      "tokens"
    ]
}
</script>

<style>

</style>