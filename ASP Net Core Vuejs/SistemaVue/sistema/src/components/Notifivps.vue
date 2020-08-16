<template>
    <v-layout aling-start>
        <v-flex>
            <v-data-table
            :headers="headers"
            :items="notifivps"
            :search="search"
            class="elevation-1"
            >
            <template v-slot:top>
                <v-toolbar flat color="white">
                    <v-toolbar-title>Notificaciones VPS</v-toolbar-title>
                    <v-divider
                        class="mx-4"
                        inset
                        vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>

                    <v-dialog v-model="adModal" max-width="290">
                          <v-card>
                            <v-card-title class="headline" v-if="adAccion==1">¿Activar Registro?</v-card-title>
                            <v-card-title class="headline" v-if="adAccion==2">¿Desactivar Registro?</v-card-title>
                            <v-card-text>
                              Estás a punto de
                              <span v-if="adAccion==1">Activar</span>
                              <span v-if="adAccion==2">Desactivar</span>
                              el Registro {{ adVmname }}
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
                notifivps:[],
                dialog: false,
                headers: [
                  { text: 'Opciones', value: 'action', sortable: false },
                  { text: 'VPS', value: 'vmname' },
                  { text: 'Cliente', value: 'clientname' },
                  { text: 'Contacto Tecnico', value: 'clientcontact' },
                  { text: 'Email Contacto Tecnico', value: 'emailcontact_tecnico' },
                  { text: 'Estado', value: 'estado' }, 
                ],
                search: '',
                idnotivps: '',
                vmname: '',
                adModal: 0,
                adAccion: 0,
                adVmname: '',
                adId: ''
            }    
        },
        computed: {
        },
        watch: {
        },
        created () {
            this.listar();
        },
        methods:{
            listar(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              axios.get('api/Notifivps/Listar',configuracion).then(function(response){
                  //console.log(response);
                  me.notifivps=response.data;
              }).catch(function(error){
                  console.log(error);
              });
            },
            activarDesactivarMostrar(accion,item){
              this.adModal=1;
              this.adVmname=item.vmname;
              this.adId=item.idnotivps;
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
              axios.put('api/Notifivps/Activar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adVmname="";
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
              axios.put('api/Notifivps/Desactivar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adVmname="";
                me.adId="";
                me.listar();
              }).catch(function(error){
                console.log(error);
              });
            }
        }
    }
</script>