<template>
    <v-layout aling-start>
        <v-flex>
            <v-data-table 
            :headers="headers" 
            :items="sqlversions" 
            :search="search"  
            class="elevation-1"
            >
                <template v-slot:top>
                    <v-toolbar flat color="white">
                        <v-toolbar-title>SQL - Versión</v-toolbar-title>
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
                                                <v-select v-model="idsql"
                                                :items="sqlfamilys" label="SQL Family">
                                                </v-select>
                                            </v-flex>
                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="mssqlversion" label="SQL Version"></v-text-field>
                                            </v-flex>
                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="mssqldescription" label="Descripción"></v-text-field>
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
                              el Registro {{ adSQLVersion }}
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
                sqlversions:[],
                dialog: false,
                headers: [
                  { text: 'Opciones', value: 'action', sortable: false },
                  { text: 'SQL Family', value: 'sqlfamily' },
                  { text: 'SQL Version', value: 'mssqlversion' },
                  { text: 'Descripción', value: 'mssqldescription' },
                  { text: 'Estado', value: 'estado' },
                
                ],
                search: '',
                editedIndex: -1,
                idsqlversion: '',
                idsql: '',
                sqlfamilys:[
                ],
                mssqlversion: '',
                mssqldescription: '',
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adSQLVersion: '',
                adId: ''
            }    
        },
        computed: {
            formTitle () {
              return this.editedIndex === -1 ? 'Nueva Versión SQL' : 'Actualizando Versión SQL'
            },
        },
        watch: {
            dialog (val) {
            val || this.close()
            },
        },
        created () {
            this.listar();
            this.select();
        },
        methods:{
            listar(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              axios.get('api/SQLVersions/Listar',configuracion).then(function(response){
                  //console.log(response);
                  me.sqlversions=response.data;
              }).catch(function(error){
                  console.log(error);
              });
            },
            select(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var sqlfamilyArray=[];
              axios.get('api/SQLFamilys/Select',configuracion).then(function(response){
                  sqlfamilyArray=response.data;
                  sqlfamilyArray.map(function(x){
                    me.sqlfamilys.push({text: x.mssql,value:x.idsql});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            editItem (item) {
              this.id=item.idsqlversion;
              this.idsql=item.idsql;
              this.mssqlversion=item.mssqlversion;
              this.mssqldescription=item.mssqldescription;
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
              this.idsqlversion="";
              this.idsql="";
              this.mssqlversion="";
              this.mssqldescription="";
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
                  axios.put('api/SQLVersions/Actualizar', {
                    'idsqlversion': me.id,
                    'idsql': me.idsql,
                    'mssqlversion': me.mssqlversion,
                    'mssqldescription': me.mssqldescription
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
                  axios.post('api/SQLVersions/Crear', {
                    'idsql': me.idsql,
                    'mssqlversion': me.mssqlversion,
                    'mssqldescription': me.mssqldescription
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

              if(this.mssqlversion.length<3 || this.mssqlversion.length>50){
                this.validaMensaje.push("La version de SQL debe tener más de 3 caracteres y menos de 50 caracteres.");
              }
              if(!this.idsql){
                this.validaMensaje.push("Seleccione un Gestor de base de datos.")
              }
              if(this.validaMensaje.length){
                this.valida=1;
              }
              return this.valida;
            },
            activarDesactivarMostrar(accion,item){
              this.adModal=1;
              this.adSQLVersion=item.mssqlversion;
              this.adId=item.idsqlversion;
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
              axios.put('api/SQLVersions/Activar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adSQLVersion="";
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
              axios.put('api/SQLVersions/Desactivar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adSQLVersion="";
                me.adId="";
                me.listar();
              }).catch(function(error){
                console.log(error);
              });
            }
        }
    }
</script>