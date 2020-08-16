<template>
    <v-layout aling-start>
        <v-flex>
            <v-data-table
            :headers="headers"
            :items="vpss"
            :search="search"
            class="elevation-1"
            >
            <template v-slot:top>
                <v-toolbar flat color="white">
                    <v-btn @click="crearPDF()"><v-icon>print</v-icon></v-btn>
                    <v-toolbar-title>Virtual Servers</v-toolbar-title>
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
                                    <v-select v-model="idclient" :items="vmclients" label="Cliente"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="vmname" label="VM Name"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="vm_uuid" label="VM UUID"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="vcpus" label="VCPU's"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="ram" label="RAM"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="hdisk" label="Disco Duro"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="bandw" label="Ancho de Banda"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-select v-model="idnw" :items="networkbonds" label="Network Bond"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-select v-model="idos" :items="osfamilys" label="OS Family"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-select v-model="idversion" :items="osversions" label="OS Version"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-select v-model="idsql" :items="sqlfamilys" label="SQL Family"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-select v-model="idsqlversion" :items="sqlversions" label="SQL Version"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="internal_ip" label="Internal IP"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="external_ip" label="External IP"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-select v-model="idusuario" :items="usuarios" label="Create By"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="dnsname" label="DNS"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-select v-model="idvmtype" :items="vmtypes" label="VM Type"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-select v-model="idpool" :items="pools" label="Pool"></v-select>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="notes" label="Notes"></v-text-field>
                                  </v-flex>
                                  <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="rmtaccesssal" label="RMT Access SAL"></v-text-field>
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
                                el Registro {{ adVMName }}
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
    import jsPDF from 'jspdf'
    import autoTABLE from 'jspdf-autotable'
    export default {
        data(){
            return{
                vpss:[],
                dialog: false,
                headers: [
                  { text: 'Opciones', value: 'opciones', sortable: false },
                  { text: 'VM Client', value: 'vmclient' },
                  { text: 'VM Name', value: 'vmname' },
                  { text: 'VM_UUID', value: 'vm_uuid', sortable: false},
                  { text: 'CPUS', value: 'vcpus', sortable: false },
                  { text: 'RAM', value: 'ram', sortable: false },
                  { text: 'Disco Duro', value: 'hdisk' },
                  { text: 'Ancho de Banda', value: 'bandw' },
                  { text: 'Network Bond', value: 'networkbond', sortable: false},
                  { text: 'OS Family', value: 'osfamily', sortable: false },
                  { text: 'OS Version', value: 'osversion', sortable: false },
                  { text: 'SQL Family', value: 'sqlfamily' },
                  { text: 'SQL Version', value: 'sqlversion' },
                  { text: 'Internal IP', value: 'internal_ip', sortable: false},
                  { text: 'External IP', value: 'external_ip', sortable: false },
                  { text: 'CreateOn', value: 'createon'},
                  { text: 'Usuario', value: 'usuario' },
                  { text: 'DNS', value: 'dnsname' },
                  { text: 'VM Type', value: 'vmtype', sortable: false},
                  { text: 'Pool', value: 'pools', sortable: false },
                  { text: 'Notes', value: 'notes', sortable: false },
                  { text: 'RMT Access SAL', value: 'rmtaccesssal', sortable: false },
                  { text: 'Estado', value: 'estado' },
                
                ],
                search: '',
                editedIndex: -1,
                idvps: '',
                idclient: '',
                vmclients:[
                ],
                vmname: '',
                vm_uuid: '',
                vcpus: '',
                ram: '',
                hdisk: '',
                bandw: '',
                idnw:'',
                networkbonds:[
                ],
                idos:'',
                osfamilys:[
                ],
                idversion:'',
                osversions:[
                ],
                idsql:'',
                sqlfamilys:[
                ],
                idsqlversion:'',
                sqlversions:[
                ],
                internal_ip: '',
                external_ip:'',
                idusuario:'',
                usuarios:[
                ],
                dnsname:'',
                idvmtype:'',
                vmtypes:[
                ],
                idpool:'',
                pools:[
                ],
                notes:'',
                rmtaccesssal:'',
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adVMName: '',
                adId: ''
            }    
        },
        computed: {
            formTitle () {
              return this.editedIndex === -1 ? 'Nuevo VPS' : 'Actualizando VPS'
            },
        },
        watch: {
            dialog (val) {
            val || this.close()
            },
        },
        created () {
            this.listar();
            this.selectvmclient();
            this.selectnw();
            this.selectosfamily();
            this.selectosversion();
            this.selectsqlfamily();
            this.selectsqlversion();
            this.selectusuario();
            this.selectvmtype();
            this.selectpool();
        },
        methods:{
            crearPDF(){
              var columns = [
                    {title: "VM Client", dataKey: "vmclient"},
                    {title: "VM Name", dataKey: "vmname"}, 
                    {title: "VM_UUID", dataKey: "vm_uuid"}, 
                    {title: "OS Family", dataKey: "osfamily"},
                    {title: "Internal IP", dataKey: "internal_ip"},
                    {title: "External IP", dataKey: "external_ip"},
                    {title: "Create On", dataKey: "createon"},
                    {title: "Usuario", dataKey: "usuario"},
                    {title: "VM Type", dataKey: "vmtype"},
                    {title: "Estado", dataKey: "estado"},

                ];
                var rows = [];

                this.vpss.map(function(x){
                    rows.push({vmclient:x.vmclient,vmname:x.vmname,vm_uuid:x.vm_uuid,osfamily:x.osfamily,internal_ip:x.internal_ip,external_ip:x.external_ip,createon:x.createon,usuario:x.usuario,vmtype:x.vmtype,estado:x.estado});
                });

                // Only pt supported (not mm or in)
                var doc = new jsPDF('l', 'pt');
                doc.autoTable(columns, rows, {
                    margin: {top: 60},
                    addPageContent: function(data) {
                        doc.text("Listado de Servidores Virtuales", 40, 30);
                    }
                });
                doc.save('ListVPS.pdf');
            },
            listar(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              axios.get('api/VPSs/Listar',configuracion).then(function(response){
                  //console.log(response);
                  me.vpss=response.data;
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectvmclient(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var vmclientsArray=[];
              axios.get('api/VMClients/Select',configuracion).then(function(response){
                  vmclientsArray=response.data;
                  vmclientsArray.map(function(x){
                    me.vmclients.push({text: x.clientname,value:x.idclient});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectnw(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var networkbondsArray=[];
              axios.get('api/NetworkBonds/Select',configuracion).then(function(response){
                  networkbondsArray=response.data;
                  networkbondsArray.map(function(x){
                    me.networkbonds.push({text: x.nwbond,value:x.idnw});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectosfamily(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var osfamilysArray=[];
              axios.get('api/OSFamilys/Select',configuracion).then(function(response){
                  osfamilysArray=response.data;
                  osfamilysArray.map(function(x){
                    me.osfamilys.push({text: x.osfamilyname,value:x.idos});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectosversion(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var osversionsArray=[];
              axios.get('api/OSVersions/Select',configuracion).then(function(response){
                  osversionsArray=response.data;
                  osversionsArray.map(function(x){
                    me.osversions.push({text: x.osversion,value:x.idversion});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectsqlfamily(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var sqlfamilysArray=[];
              axios.get('api/SQLFamilys/Select',configuracion).then(function(response){
                  sqlfamilysArray=response.data;
                  sqlfamilysArray.map(function(x){
                    me.sqlfamilys.push({text: x.mssql,value:x.idsql});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectsqlversion(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var sqlversionsArray=[];
              axios.get('api/SQLVersions/Select',configuracion).then(function(response){
                  sqlversionsArray=response.data;
                  sqlversionsArray.map(function(x){
                    me.sqlversions.push({text: x.mssqlversion,value:x.idsqlversion});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectusuario(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var usuariosArray=[];
              axios.get('api/Usuarios/Select',configuracion).then(function(response){
                  usuariosArray=response.data;
                  usuariosArray.map(function(x){
                    me.usuarios.push({text: x.nombre,value:x.idusuario});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectvmtype(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var vmtypesArray=[];
              axios.get('api/VMTypes/Select',configuracion).then(function(response){
                  vmtypesArray=response.data;
                  vmtypesArray.map(function(x){
                    me.vmtypes.push({text: x.vmtypename,value:x.idvmtype});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            selectpool(){
              let me = this;
              let header={"Authorization" : "Bearer " + this.$store.state.token};
              let configuracion= {headers : header}
              var poolsArray=[];
              axios.get('api/Poolss/Select',configuracion).then(function(response){
                  poolsArray=response.data;
                  poolsArray.map(function(x){
                    me.pools.push({text: x.poolname,value:x.idpool});
                  });
              }).catch(function(error){
                  console.log(error);
              });
            },
            editItem (item) {
              this.id=item.idvps;
              this.idclient=item.idclient;
              this.vmname=item.vmname;
              this.vm_uuid=item.vm_uuid;
              this.vcpus=item.vcpus;
              this.ram=item.ram;
              this.hdisk=item.hdisk;
              this.bandw=item.bandw;
              this.idnw=item.idnw;
              this.idos=item.idos;
              this.idversion=item.idversion;
              this.idsql=item.idsql;
              this.idsqlversion=item.idsqlversion;
              this.internal_ip=item.internal_ip;
              this.external_ip=item.external_ip;
              this.idusuario=item.idusuario;
              this.dnsname=item.dnsname;
              this.idvmtype=item.idvmtype;
              this.idpool=item.idpool;
              this.notes=item.notes;
              this.rmtaccesssal=item.rmtaccesssal;
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
              this.idvps="";
              this.idclient="";
              this.vmname="";
              this.vm_uuid="";
              this.vcpus="";
              this.ram="";
              this.hdisk="";
              this.bandw="";
              this.idnw="";
              this.idos="";
              this.idversion="";
              this.idsql="";
              this.idsqlversion="";
              this.internal_ip="";
              this.external_ip="";
              this.createon="";
              this.idusuario="";
              this.dnsname="";
              this.idvmtype="";
              this.idpool="";
              this.notes="";
              this.rmtaccesssal="";
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
                  axios.put('api/VPSs/Actualizar', {
                    'idvps': me.id,
                    'idclient': me.idclient,
                    'vmname' : me.vmname,
                    'vm_uuid' : me.vm_uuid,
                    'vcpus': me.vcpus,
                    'ram': me.ram,
                    'hdisk': me.hdisk,
                    'bandw':me.bandw,
                    'idnw' : me.idnw,
                    'idos' : me.idos,
                    'idversion': me.idversion,
                    'idsql': me.idsql,
                    'idsqlversion': me.idsqlversion,
                    'internal_ip':me.internal_ip,
                    'external_ip': me.external_ip,
                    'idusuario' : me.idusuario,
                    'dnsname' : me.dnsname,
                    'idvmtype': me.idvmtype,
                    'idpool': me.idpool,
                    'notes': me.notes,
                    'rmtaccesssal':me.rmtaccesssal
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
                  axios.post('api/VPSs/Crear', {
                    'idclient': me.idclient,
                    'vmname' : me.vmname,
                    'vm_uuid' : me.vm_uuid,
                    'vcpus': me.vcpus,
                    'ram': me.ram,
                    'hdisk': me.hdisk,
                    'bandw':me.bandw,
                    'idnw' : me.idnw,
                    'idos' : me.idos,
                    'idversion': me.idversion,
                    'idsql': me.idsql,
                    'idsqlversion': me.idsqlversion,
                    'internal_ip':me.internal_ip,
                    'external_ip': me.external_ip,
                    'idusuario' : me.idusuario,
                    'dnsname' : me.dnsname,
                    'idvmtype': me.idvmtype,
                    'idpool': me.idpool,
                    'notes': me.notes,
                    'rmtaccesssal':me.rmtaccesssal
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

              if(this.vmname.length<2 || this.vmname.length>100){
                this.validaMensaje.push("El nombre debe tener más de 3 caracteres y menos de 100 caracteres.");
              }
              if(!this.idclient){
                this.validaMensaje.push("Seleeciona un rol.");
              }
              if(!this.idnw){
                this.validaMensaje.push("Ingrese un NB.");
              }
              if(!this.idusuario){
                this.validaMensaje.push("Ingrese Creador.");
              }
              if(this.validaMensaje.length){
                this.valida=1;
              }
              return this.valida;
            },
            activarDesactivarMostrar(accion,item){
              this.adModal=1;
              this.adVMName=item.vmname;
              this.adId=item.idvps;
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
              axios.put('api/VPSs/Activar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adVMName="";
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
              axios.put('api/VPSs/Desactivar/'+this.adId, { },configuracion).then(function(response){
                me.adModal=0;
                me.adAccion=0;
                me.adVMName="";
                me.adId="";
                me.listar();
              }).catch(function(error){
                console.log(error);
              });
            }
        }
    }
</script>