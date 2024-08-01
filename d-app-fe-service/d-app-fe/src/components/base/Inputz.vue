<template>
    <label>
      {{ label }}
      <input type="text" :name="name" v-model="model" @input="$emit('input', $event.target.value)" />
    </label>
  </template>
  
  <script>
  export default {
    name: 'CustomInput',
    props: {
      label: {
        type: String,
        required: true,
      },
      value: {
        type: String,
        required: true,
      },
    },
    computed: {
      name() {
        return this.label.toLowerCase();
      },
    },
  }
  </script>