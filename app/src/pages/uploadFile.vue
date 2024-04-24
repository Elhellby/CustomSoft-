<template>
  <div>
    <v-file-input
      v-model="file"
      label="Selecciona un archivo"
      @change="fileSelected"
    ></v-file-input>
    <v-btn @click="sendFile" color="primary">Enviar Archivo</v-btn>

    <AlertMessage :message="alertMessage" :visible="showAlert" :time="2000" :type="type"/>
    
  </div>
</template>



<script>
import axios from 'axios';
import AlertMessage from '../components/alertMessage.vue';

export default {
  data() {
    return {
      file: null,

      showAlert: false,
      alertMessage:'',
      type:''
    };
  },
  methods: {
    fileSelected() {
      console.log("Archivo seleccionado:", this.file ? this.file.name : 'Ninguno');
    },
    sendFile() {
      if (!this.file) {
        this.type='error'
        this.alertMessage='El archivo no puede estar vacio¡¡¡'
        this.showAlert = true;
        setTimeout(() => {
          this.showAlert = false; 
        }, 2000);
        return;
      }
      
      let formData = new FormData();
      formData.append('files', this.file);

      let headers = {
        'x-api-key': 'dGIzcWJ4YWU0dTFxYXk5a3o5NnFiMmQ5N2xtcTVwZzA='
      };

      axios.post('http://localhost:9093/api/File', formData, { headers: headers })
        .then(response => {
          console.log("Archivo enviado exitosamente");
          this.file = null;
          this.type='success'
          this.alertMessage='Archivo enviado exitosamente¡¡¡'
          this.showAlert = true;
        })
        .catch(error => {
          console.error("Error al enviar el archivo:", error);
        });
    }
  }
};
</script>
