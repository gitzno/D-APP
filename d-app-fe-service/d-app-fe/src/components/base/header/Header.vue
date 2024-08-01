<script setup>
import LogoApp from "../LogoApp.vue";
import Button from "../Button.vue";
import {mapGetters} from 'vuex';

</script>
<template lang="">
    <div class="bg-header">
        <div class="header shadow-slim">
            <div class="action action--left">
                <LogoApp />
                <div class="option--header" style="display:flex; align-items:center;">
                    <Button type="primary" link="RichEditor" text="Viết Nhật Ký Hôm Nay"/>
                    <Button type="secondary" link="" text="Your Journal"/>
                    <Button type="secondary" link="report" text="Report"/>
                    <Button type="secondary" link="about" text="Profile"/>
                    <Button type="secondary" link="Favorite" text="Favorite"/>
                </div>
            </div>
          
            <div class="action action--right">
                
                <Button @click="handleBtn()" type="secondary" link="login" text="Logout"/>
            </div>
            
        </div>
    </div>
</template>

<script>
export default {
    methods:{
        handleBtn(){
            localStorage.removeItem('token');
            // this.$store.dispatch('user', null);
            // this.$route.push('/login');
            window.location.href = 'http://localhost:5173/';
        }
    },
}
</script>
<style scoped>
.action{
    display: flex;
}
.bg-header {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100vw;
    height: 70px;
    color: black;
    position: fixed;
    z-index: 1;
    top: 0;
}
.header {
    overflow: hidden;
    box-sizing: border-box;
    height: 50px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    min-width: 1280px;
    border-radius: 15px;
    padding: 10px;
    
    background-color: #fff;
    box-shadow: 0 1px rgba(0, 0, 0, 0.08);
}
@media only screen and (max-width: 1280px) {
    .header {
        background-color: aqua;
        min-width: 80vw;
    }
}
</style>
