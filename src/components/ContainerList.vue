<template>
  <div>Containerlist</div>
</template>

<script>
export default {
    mounted() {
        axios.get('/path/to/my/request', { params: { id: id }})
            .then(response => {
            console.log(response)
            }).catch(error => {
            console.log(response)
            })
    }
}
</script>

<style>

</style>