import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(router)

app.mount('#app');


// app.mount('#app')
// // import Vue from 'vue'
// import store from './router/vuex'
// import App from './App.vue'
// import router from './router'
// import './router/axios';


// createApp(App).use(store).use(router).mount('#app');