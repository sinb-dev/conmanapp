<template>
  <div>
    <h2 class="ui header">Request new container</h2>
    <form class="ui form" id="requestContainerPage_frmRequestContainer">
      <div class="fields">
        <div class="ten wide field">
          <label>Docker image</label>
          <div class="ui input">
            <input type="text" name="image" v-model="form.image" placeholder="Enter docker image (e.g. sinb-dev/possum)">
          </div>
        </div>
        <div class="three wide field">
          <label>Container port</label>
          <div class="ui input">
            <input type="number" name="containerPort" v-model="form.port" placeholder="Port inside container">
          </div>
        </div>
      </div>
      <div class="field">
          <label>Additional parameters</label>
          <input type="text" name="form.parameters" placeholder="Docker parameters">
      </div>
      <input type="hidden" v-model="form.reservedFor">
      </form><br>
      <button class="ui button" type="submit" @click="sendRequest">Request</button>
  </div>
</template>

<script>
const axios = require("axios");
//import LandingPage from './LandingPage.vue'
export default {
  data() {
      return {
        form : {
          image : '',
          port : 0,
          parameters : '',
          reservedFor : this.$root.settings.key_token.tokenId
        }
      }
  },
  methods : {
    toJson() {
      return {
        image : this.image,
        port : this.port,
        parameters : this.parameters,
        reservedFor : this.reservedFor
      }
    },
    saveForm() {
      this.$root.storage.reservationForm = this.toJson();
      this.$root.save();
    },
    loadForm() {
      console.log(this.$root.storage)
      if (!this.$root.storage.reservationForm)
        return false;
      
      this.form = this.$root.storage.reservationForm;
    },
    sendRequest() {
      this.saveForm(); //Save information inside this form. Helps if device is not connected
      //let self = this;
      let endpoint = this.$root.settings.remote_host
        + "reservation/" + this.$root.settings.key_token.tokenId

      axios.post(endpoint, this.$root.storage.reservationForm)
      .then(function (response) {
        console.log("Request container: " + response.data)
      })
      .catch(function (error) {
        console.log(error);
      });
      
      }
    },
    mounted() {
      if (this.$root.verified < 1) {
        //redirect
        document.location.href="#"
      }
      this.loadForm();
    }
    
}
</script>

<style>

</style>