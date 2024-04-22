<template>
  <v-data-table :headers="headers" :items="desserts" :sort-by="[{ key: 'calories', order: 'asc' }]">
    <template v-slot:top>
      <v-toolbar flat>

        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ props }">
            <v-btn class="mb-2" color="primary" dark v-bind="props">
              Nuevo
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.id" label="Id" disabled></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.nombre" label="Nombre"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.descripcion" label="Descripcion"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.precio" label="Precio" type="number"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.categoria" label="Categoria"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.cantidadEnStock" label="Stock" type="number"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.proveedor" label="Proveedor"></v-text-field>
                  </v-col>
                  <!-- <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.fechaCreacion" label="Creado"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.fechaModificacion" label="Modificado"></v-text-field>
                  </v-col> -->
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="close">
                Cancelar
              </v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="save">
                Guardar
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Estás seguro de que quieres eliminar este artículo?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="closeDelete">Cancelar</v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="deleteItemConfirm">Confirmar</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon class="me-2" size="small" @click="editItem(item)">
        mdi-pencil
      </v-icon>
      <v-icon size="small" @click="deleteItem(item)">
        mdi-delete
      </v-icon>
    </template>
    <template v-slot:no-data>
      <v-btn color="primary" @click="initialize">
        Restablecer
      </v-btn>
    </template>
  </v-data-table>
</template>

<script>
import axios from "axios";
export default {
  data: () => ({
    dialog: false,
    dialogDelete: false,
    headers: [
      {
        title: 'Id',
        align: 'start',
        sortable: false,
        key: 'id',
      },
      { title: 'Nombre', key: 'nombre' },
      { title: 'Descripcion', key: 'descripcion' },
      { title: 'Precio', key: 'precio' },
      { title: 'Categoria', key: 'categoria' },
      { title: 'Stock', key: 'cantidadEnStock' },
      { title: 'Proveedor', key: 'proveedor' },
      // { title: 'Creado', key: 'fechaCreacion' },
      // { title: 'Modificado', key: 'fechaModificacion' },
      { title: 'Aciones', key: 'actions', sortable: false },
    ],
    desserts: [],
    editedIndex: -1,
    editedItem: {

      nombre: '',
      descripcion: '',
      precio: 0,
      categoria: '',
      cantidadEnStock: 0,
      proveedor: ''

    },
    defaultItem: {
      nombre: '',
      descripcion: '',
      precio: 0,
      categoria: '',
      cantidadEnStock: 0,
      proveedor: ''
    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? 'Nuevo' : 'Editar'
    },
  },

  watch: {
    dialog(val) {
      val || this.close()
    },
    dialogDelete(val) {
      val || this.closeDelete()
    },
  },

  created() {
    this.initialize()
  },

  methods: {
    initialize() {

      let config = {
        method: 'get',
        url: 'http://localhost:9094/api/Products',
        headers: {
          'x-api-key': 'dGIzcWJ4YWU0dTFxYXk5a3o5NnFiMmQ5N2xtcTVwZzA=',
          'Access-Control-Allow-Origin': '*'
        }
      };

      axios.request(config)
        .then((response) => {
          this.desserts = response.data
        })
    },

    editItem(item) {
      this.editedIndex = this.desserts.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    deleteItem(item) {
      this.editedIndex = this.desserts.indexOf(item)
      this.editedItem = Object.assign({}, item)

      let config = {
        method: 'delete',
        maxBodyLength: Infinity,
        url: 'http://localhost:9094/api/Products?id=' + item.id,
        headers: {
          'x-api-key': 'dGIzcWJ4YWU0dTFxYXk5a3o5NnFiMmQ5N2xtcTVwZzA='
        }
      };

      axios.request(config)
      // .then((response) => {
      //   console.log(JSON.stringify(response.data));
      // })

      this.dialogDelete = true

    },

    deleteItemConfirm() {
      this.desserts.splice(this.editedIndex, 1)
      this.closeDelete()
    },

    close() {
      this.dialog = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    closeDelete() {
      this.dialogDelete = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    save() {
      if (this.editedIndex > -1) {
        Object.assign(this.desserts[this.editedIndex], this.editedItem)
      } else {
        this.desserts.push(this.editedItem)
      }

      let config = {
        method: this.editedIndex > -1 ? 'patch' : 'post',
        maxBodyLength: Infinity,
        url: 'http://localhost:9094/api/Products',
        headers: {
          'x-api-key': 'dGIzcWJ4YWU0dTFxYXk5a3o5NnFiMmQ5N2xtcTVwZzA=',
          'Content-Type': 'application/json'
        },
        data: JSON.stringify(this.editedItem)
      };

      axios.request(config)
        .then((response) => {
          console.log(JSON.stringify(response.data));
        })
        .catch((error) => {
          console.log(error);
        });
      this.close()
    },
  },
}
</script>