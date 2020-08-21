<template>
    <v-layout aling-start>
        <v-flex>
            <v-data-table
            :headers="headers"
            :items="configmails"
            :search="search"
            class="elevation-1"
            >
              <template v-slot:top>
                  <v-toolbar flat color="white">
                      <v-toolbar-title>Configuración Mail</v-toolbar-title>
                      <v-divider
                          class="mx-4"
                          inset
                          vertical
                      ></v-divider>
                      <v-spacer></v-spacer>
                      <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                      <v-spacer></v-spacer>
                      <v-dialog v-model="dialog" max-width="600px">
                          <v-card>
                              <v-card-title>
                              <span class="headline">{{ formTitle }}</span>
                              </v-card-title>
                          <v-card-text>
                              <v-container>
                              <v-row>
                                  <v-layout wrap>
                                      <v-flex xs12 sm12 md12>
                                          <v-text-field v-model="dia" label="Día a notificar"></v-text-field>
                                      </v-flex>
                                      <v-flex xs12 sm12 md12>
                                          <v-text-field v-model="asunto" label="Asunto..."></v-text-field>
                                      </v-flex>
                                      <v-flex xs12 sm12 md12>
                                          <v-text-field v-model="cuerpomail" label="Cuerpo..."></v-text-field>
                                      </v-flex>
                                      <v-flex xs12 sm12 md12 v-show="valida">
                                          <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                                      </div>
                                      </v-flex>
                                  </v-layout>
                              </v-row>
                              </v-container>
                          </v-card-text>
                          <v-card-actions>
                              <v-spacer></v-spacer>
                              <v-btn color="blue darken-1" text @click="close">Cancelar</v-btn>
                              <v-btn color="blue darken-1" text @click="guardar">Guardar</v-btn>
                          </v-card-actions>
                          </v-card>
                      </v-dialog> 
                  </v-toolbar>
              </template>

              <template v-slot:item.estado="{ item }">
                  <v-card-text v-if="item.estado" class="blue--text">Activo</v-card-text>
                  <v-card-text v-if="!item.estado" class="red--text">Inactivo</v-card-text>
              </template>

              <template v-slot:item.opciones="{ item }">
                  <v-icon
                  small
                  class="mr-2"
                  @click="editItem(item)"
                  >
                      edit
                  </v-icon>
              </template>

              <template v-slot:no-data>
                  <v-btn color="primary" @click="listar">Resetear</v-btn>
              </template>
            </v-data-table>
        </v-flex>
    </v-layout>
</template>
<script>
    import axios from 'axios'
    export default {
        data(){
            return{
                configmails:[],
                dialog: false,
                headers: [
                  { text: 'Opciones', value: 'opciones', sortable: false },
                  { text: 'Nombre Notificación', value: 'nameperiodo', sortable: false},
                  { text: 'Dia de Notificación', value: 'dia'},
                  { text: 'Asunto...', value: 'asunto', sortable: false},
                  { text: 'Cuerpo del mensaje', value: 'cuerpomail', sortable: false},
                  { text: 'Estado', value: 'estado' },                
                ],
                search: '',
                editedIndex: -1,
                idperiodo: '',
                nameperiodo: '',
                dia: '',
                asunto: '',
                cuerpomail: '',
                valida: 0,
                validaMensaje:[],
            }    
        },
        computed: {
            formTitle () {
              return this.editedIndex === -1 ? 'Nuevo Periodo' : 'Actualizando Periodo'
            },
        },
        watch: {
            dialog (val) {
            val || this.close()
            },
        },
        created () {
            this.listar();
        },
        methods:{
            listar(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              axios.get('api/ConfigMails/Listar',configuracion).then(function(response){
                  //console.log(response);
                  me.configmails=response.data;
              }).catch(function(error){
                  console.log(error);
              });
            },
            editItem (item) {
              this.id=item.idperiodo;
              this.nameperiodo=item.nameperiodo;
              this.dia=item.dia;
              this.asunto=item.asunto;
              this.cuerpomail=item.cuerpomail;
              this.editedIndex=1;
              this.dialog = true
            },
            deleteItem (item) {
              const index = this.desserts.indexOf(item)
              confirm('Are you sure you want to delete this item?') && this.desserts.splice(index, 1)
            },
            close () {
              this.dialog = false
              this.limpiar();
            },
            limpiar () {
              this.idpool="";
              this.poolname="";
              this.pooldescripcion="";
              this.editedIndex=-1;
            },
            guardar () {
              if (this.validar()){
                return;
              }
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              if (this.editedIndex > -1) {
                  //Codigo para editar
                  let me = this;
                  axios.put('api/ConfigMails/Actualizar', {
                    'idperiodo': me.id,
                    'nameperiodo': me.poolname,
                    'dia': me.dia,
                    'asunto': me.asunto,
                    'cuerpomail' : me.cuerpomail
                  },configuracion).then(function(response){
                    me.close();
                    me.listar();
                    me.limpiar();
                  }).catch(function(error){
                    console.log(error);
                  });
              } else {
                  //Codigo para guardar
                    console.log(error);
              }
            },
            validar(){
              this.valida=0;
              this.validaMensaje=[];

              if(this.asunto.length<3 || this.asunto.length>50){
                this.validaMensaje.push("El Asunto debe tener más de 3 caracteres y menos de 50 caracteres");
              }
              if(this.cuerpomail.length<3 || this.cuerpomail.length>500){
                this.validaMensaje.push("El Cuerpo debe tener más de 3 caracteres y menos de 500 caracteres");
              }
              if(!this.dia){
                this.validaMensaje.push("Ingrese un Día.");
              }
              if(this.validaMensaje.length){
                this.valida=1;
              }
              return this.valida;
            }
        }
    }
</script>