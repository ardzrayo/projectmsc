<template>
    <v-layout aling-start>
        <v-flex>
            
          <v-data-table
            :headers="headers" 
            :items="clientes" 
            :search="search" 
            class="elevation-1" 
          >
            <template v-slot:top>
              <v-toolbar flat color="white">
                <v-toolbar-title>Clientes</v-toolbar-title>
                <v-divider
                  class="mx-4"
                  inset
                  vertical
                ></v-divider>
                <v-spacer></v-spacer>
                <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                <v-spacer></v-spacer>
                <v-dialog v-model="dialog" max-width="500px">
                  <template v-slot:activator="{ on }">
                    <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo</v-btn>
                  </template>
                    <v-card>
                      <v-card-title>
                        <span class="headline">{{ formTitle }}</span>
                      </v-card-title>



                    <v-card-text>
                      <v-container>
                        <v-row>
                          <v-layout wrap>
                          <v-flex xs12 sm12 md12>
                            <v-text-field v-model="razonsocial" label="Razón Social"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm12 md12>
                            <v-text-field v-model="ubicacion" label="Ubicación"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm12 md12>
                            <v-text-field v-model="contacto" label="Contacto"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm12 md12>
                            <v-text-field v-model="email" label="Email"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm12 md12>
                            <v-text-field v-model="telefono" label="Teléfono"></v-text-field>
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
                <v-dialog v-model="adModal" max-width="290">
                  <v-card>
                    <v-card-title class="headline" v-if="adAccion==1">¿Activar Registro?</v-card-title>
                    <v-card-title class="headline" v-if="adAccion==2">¿Desactivar Registro?</v-card-title>
                    <v-card-text>
                      Estás a punto de
                      <span v-if="adAccion==1">Activar</span>
                      <span v-if="adAccion==2">Desactivar</span>
                      el Registro {{ adRazonsocial }}
                    </v-card-text>
                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn color="green darken-1" text @click="activarDesactivarCerrar">
                      Cancelar
                      </v-btn>
                      <v-btn v-if="adAccion==1" color="orange darken-4" text @click="activar">
                        Activar
                      </v-btn>
                      <v-btn v-if="adAccion==2" color="orange darken-4" text @click="desactivar">
                        Desactivar
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </v-toolbar>
            </template>

            <template v-slot:item.estado="{ item }">
              <v-card-text v-if="item.estado" class="blue--text">Activo</v-card-text>
              <v-card-text v-if="!item.estado" class="red--text">Inactivo</v-card-text>
            </template>

            <template v-slot:item.action="{ item }">
              <v-icon
                small
                class="mr-2"
                @click="editItem(item)"
                >
                  edit
                </v-icon>
                <template v-if="item.estado">
                    <v-icon
                    small
                    @click="activarDesactivarMostrar(2,item)"
                    >
                    block
                    </v-icon>
                </template>
                <template v-else>
                    <v-icon
                    small
                    @click="activarDesactivarMostrar(1,item)"
                    >
                    check
                    </v-icon>
                </template>
              </template>

            <template v-slot:no-data>
              <v-btn color="primary" @click="listar">Reset</v-btn>
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
                clientes:[],
                dialog: false,
                headers: [
                  { text: 'Opciones', value: 'action', sortable: false },
                  { text: 'Razón Social', value: 'razonsocial' },
                  { text: 'Ubicación', value: 'ubicacion' },
                  { text: 'Contacto', value: 'contacto' },
                  { text: 'Email', value: 'email' },
                  { text: 'Teléfono', value: 'telefono', sortable: false },
                  { text: 'Estado', value: 'estado' },
                
                ],
                search: '',
                editedIndex: -1,
                idcliente: '',
                razonsocial: '',
                ubicacion: '',
                contacto: '',
                email: '',
                telefono: '',
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adRazonsocial: '',
                adId: ''
            }    
        },
        computed: {
            formTitle () {
              return this.editedIndex === -1 ? 'Nuevo Cliente' : 'Actualizando Cliente'
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
              axios.get('api/Clientes/Listar').then(function(response){
                  //console.log(response);
                  me.clientes=response.data;
              }).catch(function(error){
                  console.log(error);
              });
            },
            editItem (item) {
              this.id=item.idcliente;
              this.razonsocial=item.razonsocial;
              this.ubicacion=item.ubicacion;
              this.contacto=item.contacto;
              this.email=item.email;
              this.telefono=item.telefono;
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
              this.idcliente="";
              this.razonsocial="";
              this.ubicacion="";
              this.contacto="";
              this.email="";
              this.telefono="";
              this.editedIndex=-1;
            },
            guardar () {
              if (this.validar()){
                return;
              }
              if (this.editedIndex > -1) {
                  //Codigo para editar
                  let me = this;
                  axios.put('api/Clientes/Actualizar', {
                    'idcliente': me.id,
                    'razonsocial': me.razonsocial,
                    'ubicacion' : me.ubicacion,
                    'contacto' : me.contacto,
                    'email': me.email,
                    'telefono': me.telefono
                  }).then(function(response){
                    me.close();
                    me.listar();
                    me.limpiar();
                  }).catch(function(error){
                    console.log(error);
                  });
              } else {
                  //Codigo para guardar
                  let me = this;
                  axios.post('api/Clientes/Crear', {
                    'razonsocial': me.razonsocial,
                    'ubicacion' : me.ubicacion,
                    'contacto' : me.contacto,
                    'email': me.email,
                    'telefono': me.telefono
                  }).then(function(response){
                    me.close();
                    me.listar();
                    me.limpiar();
                  }).catch(function(error){
                    console.log(error);
                  });
              }
            },
            validar(){
              this.valida=0;
              this.validaMensaje=[];

              if(this.razonsocial.length<3 || this.razonsocial.length>50){
                this.validaMensaje.push("La Razón Social debe tener más de 3 caracteres y menos de 50 caracteres");
              }
              if(this.validaMensaje.length){
                this.valida=1;
              }
              return this.valida;
            },
            activarDesactivarMostrar(accion,item){
              this.adModal=1;
              this.adRazonsocial=item.razonsocial;
              this.adId=item.idcliente;
              if (accion==1){
                this.adAccion=1;
              }
              else if(accion==2){
                this.adAccion=2;
              }
              else{
                this.adModal=0;
              }
            },
            activarDesactivarCerrar(){
              this.adModal=0;
            },
            activar(){
              let me = this;
              axios.put('api/Clientes/Activar/'+this.adId, { }).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adRazonsocial="";
                me.adId="";
                me.listar();
              }).catch(function(error){
                console.log(error);
              });
            },
            desactivar(){
              let me = this;
              axios.put('api/Clientes/Desactivar/'+this.adId, { }).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adRazonsocial="";
                me.adId="";
                me.listar();
              }).catch(function(error){
                console.log(error);
              });
            }
        }
    }
</script>