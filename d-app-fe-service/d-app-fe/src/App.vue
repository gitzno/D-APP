<script setup>
import { RouterLink, RouterView } from "vue-router";
import Header from "./components/base/header/Header.vue";
import HeaderLogin from "./components/base/header/HeaderLogin.vue";
import Footer from "./components/base/footer/Footer.vue";
import { ref, watch } from "vue";
import HeaderLoginVue from "./components/base/header/HeaderLogin.vue";
</script>

<template>
    <div class="app" v-if="tokenz == null">
        <HeaderLoginVue />
        <div class="content">
            <router-view />
        </div>
        <Footer />
    </div>
    <div class="app" v-if="tokenz != null">
        <Header />
        <div class="content">
            <router-view :token="tokenz" />
        </div>
        <Footer />
    </div>
</template>
<script>
export default {
    name: "App",

    data() {
        return {
            tokenz: null,
        };
    },
    async created() {
        // this.token;
        this.tokenz = localStorage.getItem('token');
        // console.log(this.token);
        // if(localStorage.getItem('token'))
        // const response = await axios.get("/Users/Profile");
        // console.log(response);
        //  this.$store.dispatch("user", response.data.data);
    },
    components: {
        Header,
    },
    
};
</script>
<style scoped>
.app {
    height: 100vh;
    flex-direction: column;
    display: flex;
    align-items: center;
}
.content {
    min-width: 1280px;
    padding-top: 70px;
    min-height: 800vh;
}
</style>
