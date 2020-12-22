import Vue from 'vue'

Vue.config.productionTip = false
//import HelloWorld from './components/HelloWorld.vue'
import NotFound from './components/NotFound.vue'
import LandingPage from './components/LandingPage.vue'
import TokenPage from './components/TokenPage.vue'
const routes = {
  '^$' : LandingPage,
  '/tokens' : TokenPage,
}
var app = new Vue({
  data : {
    currentRoute : document.location.hash.substr(1),
    settings : {
      key_token : ""
    }
  },
  methods : {
    save() {
      console.log("Saving")
      let json_object = {
        settings : this.settings
      }
      let json_string = JSON.stringify(json_object);
      
      localStorage.setItem("conman",json_string);
    },
    load() {
      console.log("Loading")
      let json_string = localStorage.getItem("conman");
      if (json_string) {
        let json_object = JSON.parse(json_string);
        if (json_object) {
          console.log("Loaded");
          this.settings = json_object.settings;
          console.log("main " + this.settings.key_token);
        }
      }
    }
  },
  computed : {
    configured() {
      return this.settings.key_token != ""
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
    return c(this.ViewComponent);
  },
  beforeMount() {
    this.load();
  }
}).$mount('#app')
window.onhashchange = function() {
  app.currentRoute = document.location.hash.substr(1)
}
