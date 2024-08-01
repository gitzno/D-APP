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
                <!-- <div class="option--header" style="display:flex; align-items:center;">
                    <Button type="secondary" link="newRichEditor" text="Today"/>
                    <Button type="secondary" link="" text="Journal"/>
                    <Button type="secondary" link="newRichEditor" text="Report"/>
                    <Button type="secondary" link="about" text="Document"/>
                </div> -->
            </div>
            <!-- {{user.userName}} -->
            <div class="action action--right" >
                <Button type="secondary" link="login" text="Login"/>
                <Button type="primary" link="register" text="Sign up"/>
            </div>
            
            
        </div>
    </div>
</template>

<script>
export default {
   
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
