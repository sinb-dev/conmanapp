<template>
<div>
    <h1>My containers</h1>
    <div v-if="containers == null">
        Loading containers...
    </div>
    <div v-else-if="containers.length == 0">No running containers</div>
    <div v-else class="ui middle aligned divided list">
        <div class="item" v-for="container in containers" v-bind:key="container.port">
            <div class="right floated content">
                <button class="ui icon button large green" v-if="container.state != 'running'"><i class="play icon"></i></button>
                <button class="ui icon button large orange" v-if="container.state == 'running'"><i class="redo icon"></i></button>
                <button class="ui icon button large red" v-if="container.state == 'running'"><i class="stop icon"></i></button>
            </div>
            <div class="content">
                <div class="header">{{container.name}}</div>
                <small class="description">Port {{container.port}} {{container.image}}</small>
            </div>
        </div>
    </div>
    <a class="ui button" href="#/container/request">Request new container</a>
</div>
</template>

<script>
const axios = require("axios");
export default {
    data() {
        return {
            containers : null
        }
    },
    methods : {
        loadContainers(json) {
            this.containers = json;
        },
        startContainer() {
            
        }
    },
    mounted() {
        /*this.loadContainers([{
            image : "sinbdev/possum",
            port : 10000,
            name : "possum",
            state : "running"
        }, {
            image : "sinbdev/openssh-server",
            port : 3000,
            name : "openssh",
            state : "running"
        }]);*/
        let self = this;
        let endpoint = this.$root.settings.remote_host
        + "docker/"+ this.$root.settings.key_token.tokenId
        axios.get(endpoint)
        .then(response => {
            self.loadContainers(response.data);
        }).catch(error => {
            console.log(error)
        })
    }
}
</script>

<style>

</style>