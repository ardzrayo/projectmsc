<template>
    <v-layout aling-start>
        <v-flex>
            <v-data-table
            :headers="headers"
            :items="vmclients"
            :search="search"
            class="elevation-1"
            >
            <template v-slot:top>
                <v-toolbar flat color="white">
                    <v-toolbar-title>VM clientes</v-toolbar-title>
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
                                <v-text-field v-model="clientname" label="Cliente"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                <v-text-field v-model="clientfullname" label="Nombre Completo"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                <v-text-field v-model="clientemail" label="Email"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                <v-text-field v-model="clientphone" label="Teléfono"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                <v-text-field v-model="clientcontact" label="Contacto Técnico"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                <v-text-field v-model="emailcontact_tecnico" label="Email Contacto Técnico"></v-text-field>
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
                                el Registro {{ adclientname }}
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

            <template v-slot:item.opciones="{ item }">
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
                vmclients:[],
                dialog: false,
                headers: [
                  { text: 'Opciones', value: 'opciones', sortable: false },
                  { text: 'Nombre', value: 'clientname' },
                  { text: 'Nombre Completo', value: 'clientfullname' },
                  { text: 'Email', value: 'clientemail' },
                  { text: 'Teléfono', value: 'clientphone', sortable: false },
                  { text: 'Cliente Contacto', value: 'clientcontact', sortable: false },
                  { text: 'Email Contacto Técnico', value: 'emailcontact_tecnico', sortable: false },
                  { text: 'Estado', value: 'estado' },
                
                ],
                search: '',
                editedIndex: -1,
                idclient: '',
                clientname: '',
                clientfullname: '',
                clientemail: '',
                clientphone: '',
                clientcontact: '',
                emailcontact_tecnico: '',
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adclientname: '',
                adId: ''
            }    
        },
        computed: {
            formTitle () {
              return this.editedIndex === -1 ? 'Nuevo VM Cliente' : 'Actualizando VM Cliente'
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
              axios.get('api/VMClients/Listar',configuracion).then(function(response){
                  //console.log(response);
                  me.vmclients=response.data;
              }).catch(function(error){
                  console.log(error);
              });
            },
            editItem (item) {
              this.id=item.idclient;
              this.clientname=item.clientname;
              this.clientfullname=item.clientfullname;
              this.clientemail=item.clientemail;
              this.clientphone=item.clientphone;
              this.clientcontact=item.clientcontact;
              this.emailcontact_tecnico=item.emailcontact_tecnico;
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
              this.idclient="";
              this.clientname="";
              this.clientfullname="";
              this.clientemail="";
              this.clientphone="";
              this.clientcontact="";
              this.emailcontact_tecnico="";
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
                  axios.put('api/VMClients/Actualizar', {
                    'idclient': me.id,
                    'clientname': me.clientname,
                    'clientfullname' : me.clientfullname,
                    'clientemail' : me.clientemail,
                    'clientphone': me.clientphone,
                    'clientcontact': me.clientcontact,
                    'emailcontact_tecnico': me.emailcontact_tecnico
                  },configuracion).then(function(response){
                    me.close();
                    me.listar();
                    me.limpiar();
                  }).catch(function(error){
                    console.log(error);
                  });
              } else {
                  //Codigo para guardar
                  let me = this;
                  axios.post('api/VMClients/Crear', {
                    'clientname': me.clientname,
                    'clientfullname' : me.clientfullname,
                    'clientemail' : me.clientemail,
                    'clientphone': me.clientphone,
                    'clientcontact': me.clientcontact,
                    'emailcontact_tecnico': me.emailcontact_tecnico
                  },configuracion).then(function(response){
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

              if(this.clientname.length<3 || this.clientname.length>50){
                this.validaMensaje.push("El nombre debe tener más de 3 caracteres y menos de 50 caracteres");
              }
              if(this.validaMensaje.length){
                this.valida=1;
              }
              return this.valida;
            },
            activarDesactivarMostrar(accion,item){
              this.adModal=1;
              this.adclientname=item.clientname;
              this.adId=item.idclient;
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
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              axios.put('api/VMClients/Activar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adclientname="";
                me.adId="";
                me.listar();
              }).catch(function(error){
                console.log(error);
              });
            },
            desactivar(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              axios.put('api/VMClients/Desactivar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adclientname="";
                me.adId="";
                me.listar();
              }).catch(function(error){
                console.log(error);
              });
            }
        }
    }
</script>