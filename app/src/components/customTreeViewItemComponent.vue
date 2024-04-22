<template>
    <v-row align="center">
      <v-icon v-if="!item.file">{{ item.title }}</v-icon>
      <v-icon v-else>{{ files[item.file] }}</v-icon>
      <v-spacer></v-spacer>
      <span>{{ item.title }}</span>
      <v-btn-icon @click="edit">
        <v-icon>mdi-pencil</v-icon>
      </v-btn-icon>
      <v-btn-icon @click="remove">
        <v-icon>mdi-delete</v-icon>
      </v-btn-icon>
    </v-row>
  </template>
  
  <script>
  export default {
    props: {
      item: {
        type: Object,
        required: true,
      },
    },
    methods: {
      edit() {
        this.$emit('edit', this.item);
      },
      remove() {
        this.$emit('delete', this.item);
      },
    },
  };
  </script>
  
  <style>
  </style>
  