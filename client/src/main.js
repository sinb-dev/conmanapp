import Vue from 'vue'

Vue.config.productionTip = false
//import HelloWorld from './components/HelloWorld.vue'
import NotFound from './components/NotFound.vue'
import LandingPage from './components/LandingPage.vue'
import TokenPage from './components/TokenPage.vue'
import RequestTokenPage from './components/RequestTokenPage.vue'
import RequestContainerPage from './components/RequestContainerPage.vue'
const routes = {
  '^$' : LandingPage,
  'tokens' : TokenPage,
  'token/request' : RequestTokenPage,
  'container/request' : RequestContainerPage
}
const axios = require("axios");
var app = new Vue({
  data : {
    currentRoute : document.location.hash.substr(1),
    verified : false,
    settings : {
      key_token : {tokenId: ""},
      remote_host : "https://10.0.0.155:5001/",
    },
    storage : {},
    dat : "meh",
    connected : false
  },
  methods : {
    save() {
      console.log("Saving")
      let json_object = {
        settings : this.settings,
        storage : this.storage
      }
      let json_string = JSON.stringify(json_object);
      
      localStorage.setItem("conman",json_string);
      this.verify();
    },
    load() {
      console.log("Loading")
      let json_string = localStorage.getItem("conman");
      if (json_string) {
        let json_object = JSON.parse(json_string);
        if (json_object) {
          console.log("Loaded");
          this.settings = json_object.settings;
          this.storage = json_object.storage;
          if (this.storage == undefined) this.storage = {}
          console.log("main " + this.tokenId());
        }
      }
    },
    
    verify() {
      let self = this;
      let endpoint = this.$root.settings.remote_host
        + "token/verify"

      axios.post(endpoint, {
        tokenId: this.tokenId()
      })
      .then(function (response) {
        self.verified = response.data == "verified";
      })
      .catch(function () {
        self.verified = true;
      });
    },
    tokenId() {
      return this.settings.key_token.tokenId;
    },
    connnectedToInternet() {
        var xhr = new XMLHttpRequest();
        var file = this.settings.remove_host;
        var randomNum = Math.round(Math.random() * 10000);
    
        xhr.open('HEAD', file + "?rand=" + randomNum, true);
        xhr.send();
        
        var self = this;

        function processRequest() {
          if (xhr.readyState == 4) {
            if (xhr.status >= 200 && xhr.status < 304) {
              self.connected = true;
            } else {
              self.connected = false;
            }
          }
        }
        xhr.addEventListener("readystatechange", processRequest, false);
    }
  },
  computed : {
    configured() {
      console.log("Key token: "+this.tokenId());
      return this.tokenId() != ""
        && this.settings.class
    },
    ViewComponent() {
      for (var k in routes) {
        var regex = new RegExp(k);
        if (regex.test(this.currentRoute)) {
          return routes[k]
        }
      }
      return NotFound
    }
  },
  render(c) {
    if (this.ViewComponent != NotFound && this.ViewComponent.requireSetup && !this.configured) {
      
      return c(LandingPage);
    }
    return c(this.ViewComponent);
  },
  beforeMount() {
    this.load();
    this.verify();
  }
}).$mount('#app')
window.onhashchange = function() {
  app.currentRoute = document.location.hash.substr(1)
}
