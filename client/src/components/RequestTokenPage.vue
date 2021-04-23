<template>
  <div>
    <h2 class="ui header"> Request new token</h2>
    <div v-if="$root.configured && $root.verified">
      You already have an api key
      <a class="ui button" href="#"></a>
    </div>
    <div v-else-if="$root.configured && !$root.verified">
      Requested new token
    </div>
    <div v-else>
      <form class="ui form" id="requestTokenPage_frmRequestToken">
        <div class="field">
            <label>Name</label>
            <input type="text" name="name" v-model="name" placeholder="Name">
        </div>
        <div class="field">
            <label>Class</label>
            <input type="text" name="class" v-model="className" placeholder="Class" data-validate="minLength[4]">
        </div>
      </form>
      <button class="ui button" type="submit" @click="sendRequest">Send request</button>
    </div>
  </div>
</template>

<script>
const axios = require("axios");
export default {
  props : {
    'token' : String
  },
  data() {
    return {
      name : this.$root.settings.username,
      className : '',
    }
  },
  methods : {
    error() {

    },
    validate() {
      if (this.name.length < 4) this.error();
    },
    sendRequest() {

      let self = this;
      let endpoint = this.$root.settings.remote_host
        + "token/request"
      this.$root.settings.username = this.name;
      this.$root.settings.classname = this.className;
      
      axios.post(endpoint, {
        owner: this.name,
        class: this.className,
      })
      .then(function (response) {
        if (self.$root.configured == false) {
          var tokenId = response.data.tokenId;
          self.$root.settings.key_token = tokenId;
          console.log("Received token id: "+tokenId);
          self.$root.save();
          document.location.href="#";
        }

      })
      .catch(function (error) {
        console.log(error);
      });
      
    }
  },
  mounted() {
    if (this.$root.configured && !this.$root.verified) {
      this.sendRequest()
    }
  }
}
</script>

<style>

</style>