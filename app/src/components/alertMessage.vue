<template>
  <v-alert
    v-show="visible"
    v-bind:type="type"
    dismissible
    class="bottom-alert"
  >
    {{ message }}
  </v-alert>
</template>

<script>
export default {
  props: {
    message: '',
    visible: false,
    time: 1,
    type:'success'
  }
};
</script>

<style scoped>
.bottom-alert {
  position: fixed;
  bottom: 0;
  left: 50%;
  transform: translateX(-50%);
  margin:10px;
  z-index:345345
}
</style>
