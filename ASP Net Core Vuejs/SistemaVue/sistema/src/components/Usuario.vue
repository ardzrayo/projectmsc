<template>
    <v-layout aling-start>
        <v-flex>
            <v-data-table
            :headers="headers"
            :items="usuarios"
            :search="search"
            class="elevation-1"
            >
            <template v-slot:top>
                <v-toolbar flat color="white">
                    <v-toolbar-title>Usuarios</v-toolbar-title>
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
                                    <v-select v-model="idrol" :items="rols" label="Rol"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="nombre" label="Nombre"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="direccion" label="Dirección"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="telefono" label="Teléfono"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="email" label="Correo Electrónico"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field type="password" v-model="password" label="Contraseña"></v-text-field>
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
                                el Registro {{ adNombre }}
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
                usuarios:[],
                dialog: false,
                headers: [
                  { text: 'Opciones', value: 'opciones', sortable: false },
                  { text: 'Rol', value: 'rol' },
                  { text: 'Nombre', value: 'nombre' },
                  { text: 'Dirección', value: 'direccion', sortable: false},
                  { text: 'Teléfono', value: 'telefono', sortable: false },
                  { text: 'Correo Electrónico', value: 'email', sortable: false },
                  { text: 'Estado', value: 'estado' },
                
                ],
                search: '',
                editedIndex: -1,
                idusuario: '',
                idrol: '',
                rols:[
                ],
                nombre: '',
                direccion: '',
                telefono: '',
                email: '',
                password: '',
                actPassword:false,
                passwordAnt:'',
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adId: ''
            }    
        },
        computed: {
            formTitle () {
              return this.editedIndex === -1 ? 'Nuevo Usuario' : 'Actualizando Usuario'
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
              axios.get('api/Usuarios/Listar',configuracion).then(function(response){
                  //console.log(response);
                  me.usuarios=response.data;
              }).catch(function(error){
                  console.log(error);
              });
            },
            select(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var rolsArray=[];
              axios.get('api/Rols/Select',configuracion).then(function(response){
                  rolsArray=response.data;
                  rolsArray.map(function(x){
                    me.rols.push({text: x.nombre,value:x.idrol});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            editItem (item) {
              this.id=item.idusuario;
              this.idrol=item.idrol;
              this.nombre=item.nombre;
              this.direccion=item.direccion;
              this.telefono=item.telefono;
              this.email=item.email;
              this.password=item.password_hash;
              this.passwordAnt=item.password_hash;
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
              this.idusuario="";
              this.idrol="";
              this.nombre="";
              this.direccion="";
              this.telefono="";
              this.email="";
              this.password="";
              this.passwordAnt="";
              this.actPassword=false;
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
                  if (me.password!=me.passwordAnt){
                      me.actPassword=true;
                  }
                  axios.put('api/Usuarios/Actualizar', {
                    'idusuario': me.id,
                    'idrol': me.idrol,
                    'nombre' : me.nombre,
                    'direccion' : me.direccion,
                    'telefono': me.telefono,
                    'email': me.email,
                    'password': me.password,
                    'act_password':me.actPassword
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
                  axios.post('api/Usuarios/Crear', {
                    'idrol': me.idrol,
                    'nombre' : me.nombre,
                    'direccion' : me.direccion,
                    'telefono': me.telefono,
                    'email': me.email,
                    'password': me.password
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

              if(this.nombre.length<3 || this.nombre.length>100){
                this.validaMensaje.push("El nombre debe tener más de 3 caracteres y menos de 100 caracteres.");
              }
              if(!this.idrol){
                this.validaMensaje.push("Seleeciona un rol.");
              }
              if(!this.email){
                this.validaMensaje.push("Ingrese un Email.");
              }
              if(!this.password){
                this.validaMensaje.push("Ingrese el password del usuario.");
              }
              if(this.validaMensaje.length){
                this.valida=1;
              }
              return this.valida;
            },
            activarDesactivarMostrar(accion,item){
              this.adModal=1;
              this.adNombre=item.nombre;
              this.adId=item.idusuario;
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
              axios.put('api/Usuarios/Activar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adNombre="";
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
              axios.put('api/Usuarios/Desactivar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adNombre="";
                me.adId="";
                me.listar();
              }).catch(function(error){
                console.log(error);
              });
            }
        }
    }
</script>