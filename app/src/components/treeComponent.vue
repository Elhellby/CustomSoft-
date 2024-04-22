<template>
  <div>
    <v-btn v-if="isSelectable" class="btn-float" color="primary" @click="toggleSelectable">
      <v-icon>mdi-pencil</v-icon>
      Editar
    </v-btn>
    <v-btn v-if="!isSelectable" class="btn-float" color="primary" @click="openModal">
      <v-icon>mdi-plus</v-icon>
      Agregar
    </v-btn>
    <v-treeview
      :items="items"
      item-disabled="locked"
      :selectable="!isSelectable"
    >
      <!-- Template personalizado para cada elemento -->
      <template v-slot:prepend="{ item, open }">
        <v-icon v-if="!item.file">
          {{ open ? 'mdi-folder-open' : 'mdi-folder' }}
        </v-icon>
        <v-icon v-else>
          {{ files[item.file] }}
        </v-icon>
      </template>
      <template v-slot:title="{ item }">
        <div class="item-container">
          <template v-if="!item.editing">
            <span>{{ item.title }}</span>
          </template>
          <template v-else>
            <v-text-field v-model="item.title" dense outlined style="width: 250px;"></v-text-field>
          </template>
          <div class="actions-container">
            <v-btn v-if="!item.editing" density="compact" icon="mdi-pencil" color="primary" @click="editItem(item)" class="mr-5"></v-btn>
            <v-btn v-else density="compact" icon="mdi-check" color="success" @click="saveItem(item)" class="mr-5"></v-btn>
            <v-btn density="compact" icon="mdi-delete" color="error" @click="deleteItem(item)" class="mr-5"></v-btn>
            <v-btn v-if="!item.file" density="compact" icon="mdi-plus" color="primary" @click="openModal(item)"></v-btn>
          </div>
        </div>
      </template>
    </v-treeview>
    <!-- Modal para agregar un nuevo ítem -->
    <v-dialog v-model="modalOpen" max-width="400">
      <template v-slot:activator="{ on }"></template>
      <v-card>
        <v-card-title>Agregar Nuevo Ítem</v-card-title>
        <v-card-text>
          <v-text-field v-model="newItem.title" label="Nombre" outlined></v-text-field>
          <v-select v-model="newItem.file" :items="Object.keys(files)" label="Archivo" outlined></v-select>
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" @click="addNewItem">Aceptar</v-btn>
          <v-btn @click="closeModal">Cancelar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import treeSource from '../assets/treeSource.json';

export default {
  data() {
    return {
      initiallyOpen: ['public'],
      files: {
        html: 'mdi-language-html5',
        js: 'mdi-nodejs',
        json: 'mdi-code-json',
        md: 'mdi-language-markdown',
        pdf: 'mdi-file-pdf-box',
        png: 'mdi-file-image',
        txt: 'mdi-file-document-outline',
        xls: 'mdi-file-excel',
      },
      tree: [],
      items: treeSource,
      isSelectable: true,
      modalOpen: false,
      newItem: {
        title: '',
        file: null,
        parentItem: null, // Referencia al elemento padre
      },
    };
  },
  computed: {
    itemsVisible() {
      // Verificar si todos los ítems tienen la propiedad visible como true
      return this.items.every(item => item.visible !== undefined ? item.visible : true);
    }
  },
  methods: {
    handleNodeClick(activeNodes) {
      console.log('Se ha hecho clic en el nodo:', activeNodes);
    },
    toggleSelectable() {
      this.isSelectable = !this.isSelectable;
    },
    editItem(item) {
      item.editing = true;
    },
    saveItem(item) {
      item.editing = false;
      // Agrega aquí la lógica para guardar el elemento editado
    },
    deleteItem(item) {
      console.log('Eliminando el elemento:', item);
      this.removeItem(item, this.items);
    },
    removeItem(item, items) {
      const index = items.indexOf(item);
      if (index > -1) {
        items.splice(index, 1);
      } else {
        items.forEach(subItem => {
          if (subItem.children) {
            this.removeItem(item, subItem.children);
          }
        });
      }
    },
    openModal(parentItem) {
      this.newItem.parentItem = parentItem; // Almacena la referencia al elemento padre
      this.modalOpen = true;
    },
    closeModal() {
      this.modalOpen = false;
    },
    addNewItem() {
      if (this.newItem.title && this.newItem.file && this.newItem.parentItem) {
        if (!this.newItem.parentItem.children) {
          this.newItem.parentItem.children = [];
        }
        this.newItem.parentItem.children.push({
          title: this.newItem.title,
          file: this.newItem.file,
        });
        this.newItem.title = '';
        this.newItem.file = null;
        this.newItem.parentItem = null; // Restablece la referencia al elemento padre
        this.closeModal();
      }
    },
  },
};
</script>

<style>
.btn-float {
  position: fixed;
  top: 65px; /* ajuste del top */
  right: 20px;
  z-index: 100; /* ajuste del z-index */
}

.item-container {
  display: flex;
  align-items: center;
}

.actions-container {
  margin-left: auto; /* Mover el div de acciones al lado derecho */
  display: flex;
}

.actions-container > .v-btn {
  margin-right: 5px; /* Agregar margen entre los botones */
}
</style>
