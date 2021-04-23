<template>
  <div>
    <h2 class="ui header"> Request new access token</h2>
    <div v-if="$root.configured() && $root.verified >= 1">
      You already have an api key<br>
      <a class="ui button" href="#">Go back</a>
      <a class="ui red button" @click="reset()">Reset</a>
    </div>
    <div v-else-if="$root.configured() && $root.verified == 0">
      Requested new token, waiting for approval
    </div>
    <div v-else>
      <form class="ui form" id="requestTokenPage_frmRequestToken">
        <div class="field">
            <label>Your name</label>
            <input type="text" name="name" v-model="name" placeholder="Your name">
        </div>
        <div class="field">
            <label>Your class</label>
            <input type="text" name="class" v-model="className" placeholder="Class" data-validate="minLength[4]">
        </div>
      </form><br>
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
      
      axios.post(endpoint, {
        owner: this.name,
        class: this.className,
      })
      .then(function (response) {
        console.log("New token id: " + response.data.tokenId);
        if (self.$root.configured() == false) {
          var tokenId = response.data.tokenId;
          self.$root.settings.key_token.tokenId = tokenId;
          self.$root.save();
          document.location.href="#";
        }

      })
      .catch(function (error) {
        console.log(error);
      });
      
    },
    reset() {
      this.$root.settings.key_token.tokenId = "";
      this.$root.save();
    }
  },
  mounted() {
    if (this.$root.configured() && !this.$root.verified) {
      this.sendRequest()
    }
  }
}
</script>

<style>

</style>