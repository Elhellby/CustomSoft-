<template>
  <v-data-table :headers="headers" :items="desserts" :sort-by="[{ key: 'id', order: 'asc' }]">
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
                    <v-text-field v-model="editedItem.cantidad_en_stock" label="Stock" type="number"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="editedItem.proveedor" label="Proveedor"></v-text-field>
                  </v-col>
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

  <AlertMessage :message="alertMessage" :visible="showAlert" :time="2000" :type="type"/>
</template>

<script>
import productosService from '../services/productosService.ts'


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
      { title: 'Stock', key: 'cantidad_en_stock' },
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
      cantidad_en_stock: 0,
      proveedor: ''

    },
    defaultItem: {
      nombre: '',
      descripcion: '',
      precio: 0,
      categoria: '',
      cantidad_en_stock: 0,
      proveedor: ''
    },
    showAlert: false,
      alertMessage:'',
      type:''
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

      productosService.getProductos().then((response) => {
        this.desserts=response.data
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

      

      this.dialogDelete = true

    },

    deleteItemConfirm() {
      this.closeDelete()
      productosService.deleteProducto(this.editedItem.id).then((response) => {
          this.desserts=response.data
          this.type='success'
          this.alertMessage='Registro eliminado¡¡¡'
          this.showAlert = true;
        }).catch(()=>{
          this.type='error'
          this.alertMessage='Ocurrio un error al eliminar¡¡¡'
          this.showAlert = true;
        })

        setTimeout(() => {
          this.showAlert = false; 
        }, 2000);
      
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

      const producto= this.editedItem;

      if(
        this.editedItem.nombre!='' &&
        this.editedItem.descripcion!='' &&
        this.editedItem.precio >=0 &&
        this.editedItem.categoria!='' &&
        this.editedItem.cantidad_en_stock >=0 &&
        this.editedItem.proveedor !=''
      ){
        if(this.editedIndex <= -1){
          productosService.createProducto(producto).then((response) => {
            this.desserts=response.data
          })
        }else{
          productosService.updateProducto(producto).then((response) => {
            console.log(response)
            this.desserts=response.data
          })
        }
      this.close()
      this.type='success'
          this.alertMessage='Registro guardado¡¡¡'
          this.showAlert = true;
      }else{
        this.type='warning'
          this.alertMessage='Falta informacion¡¡¡'
          this.showAlert = true;
      }
      setTimeout(() => {
          this.showAlert = false; 
        }, 2000);
      
    },
  },
}
</script>