<template>
  <v-container class="fill-height">
    <v-responsive
      class="align-centerfill-height mx-auto"
      max-width="900"
    >
      <v-img
        class="mb-4"
        height="150"
        src="https://jumpseller.mx/generated/images/learn/los-10-productos-mas-vendidos-en-mexico/online-shopping-mexico-800-3423d44e0.png"
      />

      <div class="text-center">
        <h1 class="text-h2 font-weight-bold">Administrador de productos</h1>
      </div>


      <v-row class="py-4">
        <v-col cols="6">
          <v-card
            append-icon="mdi-open-in-new"
            class="py-4"
            color="surface-variant"
            href="/productos"
            prepend-icon="mdi-nutrition"
            rel="noopener noreferrer"
            rounded="lg"
            subtitle="Administracion de los productos del inventario"
            title="Productos"
            variant="text"
          >
            <v-overlay
              opacity=".06"
              scrim="primary"
              contained
              model-value
              persistent
            />
          </v-card>
        </v-col>

        <v-col cols="6">
          <v-card
            append-icon="mdi-open-in-new"
            class="py-4"
            color="surface-variant"
            href="/archivos"
            prepend-icon="mdi-file-multiple"
            rel="noopener noreferrer"
            rounded="lg"
            subtitle="Explore y administre los archivos de producto."
            title="Archivos"
            variant="text"
          >
            <v-overlay
              opacity=".06"
              scrim="primary"
              contained
              model-value
              persistent
            />
          </v-card>
        </v-col>

      </v-row>
    </v-responsive>
  </v-container>
</template>

<script lang="ts" setup>
  //
</script>
