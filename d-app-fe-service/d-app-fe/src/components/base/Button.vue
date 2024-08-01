<template lang="">
    <router-link class="button" :class="type" v-bind:to="'/' + link">
        <div>{{text}}</div>
    </router-link>
</template>

<script>
export default {
    props: ["text", "type", "link"],
};
</script>

<style scope>
.button{
    color: black;
    text-decoration: none;
    padding: 0.5rem 1rem;
    border-radius: 10px;
    margin: 10px;
    transition: background-color 0.5s;
}
.primary{
    color: white !important;
    background-color: black;
}
.secondary:hover{    
    background-color:  rgba(0, 0, 0, 0.06);
    transition: background-color 0.5s;
}
</style>
