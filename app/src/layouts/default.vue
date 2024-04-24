<template>
  <v-app>
    <v-app-bar prominent color="primary" density="compact">
      <v-app-bar-nav-icon variant="text" @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>Administrador de productos</v-toolbar-title>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" :location="$vuetify.display.mobile ? 'bottom' : undefined" temporary>
      <v-list-item link title="Inicio" href="/" prepend-icon="mdi-home"></v-list-item>
      <v-divider></v-divider>
      <v-list-item link title="Productos" href="/productos" prepend-icon="mdi-nutrition"></v-list-item>
      <v-list-item link title="Arbol de archivos" href="/archivos" prepend-icon="mdi-file-multiple"></v-list-item>
      <v-list-item link title="Subir Archivos" href="/uploadFile" prepend-icon="mdi-file-upload"></v-list-item>
      
    </v-navigation-drawer>

    <v-main class="ma-8">
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts" setup>
import { ref } from 'vue'

const drawer = ref(null)

</script>

<style>
.bottom-alert {
  position: fixed;
  bottom: 0;
  left: 50%;
  transform: translateX(-50%);
}
</style>
