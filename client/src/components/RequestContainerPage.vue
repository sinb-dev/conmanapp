<template>
  <div>
    <h2 class="ui header">Request new container</h2>
    <form class="ui form" id="requestContainerPage_frmRequestContainer">
      
        <!--<div class="field">
            <label>Name</label>
            <input type="text" name="ReservedFor" v-model="name" placeholder="Name">
        </div>-->
      <div class="fields">
        <div class="ten wide field">
          <label>Docker image</label>
          <div class="ui input">
            <input type="text" name="image" v-model="image" placeholder="Enter docker image">
          </div>
        </div>
        <div class="three wide field">
          <label>Container port</label>
          <div class="ui input">
            <input type="text" name="containerPort" placeholder="Port inside container">
          </div>
        </div>
      </div>
      <div class="field">
          <label>Additional parameters</label>
          <input type="text" name="parameters" placeholder="Docker parameters">
      </div>
      <input type="text" name="reservedFor" v-bind:value="$root.settings.key_token.tokenId">
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
        image : ''
      }
  },
  methods : {
    toJson() {
      return {
        image : this.image,
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
      
      this.image = this.$root.storage.reservationForm.image;

    },
    sendRequest() {
      this.saveForm(); //Save information inside this form. Helps if device is not connected
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
      this.loadForm();
    },
    
}
</script>

<style>

</style>