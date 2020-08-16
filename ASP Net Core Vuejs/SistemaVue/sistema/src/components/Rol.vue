<template>
    <v-layout aling-start>
        <v-flex>
            <v-data-table
            :headers="headers"
            :items="rols"
            :search="search"
            class="elevation-1"
            >
            <template v-slot:top>
                <v-toolbar flat color="white">
                    <v-toolbar-title>Roles</v-toolbar-title>
                    <v-divider
                        class="mx-4"
                        inset
                        vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>
                </v-toolbar>
            </template>

            <template v-slot:item.estado="{ item }">
                <v-card-text v-if="item.estado" class="blue--text">Activo</v-card-text>
                <v-card-text v-if="!item.estado" class="red--text">Inactivo</v-card-text>
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
                rols:[],
                dialog: false,
                headers: [
                  { text: 'Rol', value: 'nombre' },
                  { text: 'Descripción', value: 'descripcion' },
                  { text: 'Estado', value: 'estado' },
                
                ],
                search: ''
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
              axios.get('api/Rols/Listar',configuracion).then(function(response){
                  //console.log(response);
                  me.rols=response.data;
              }).catch(function(error){
                  console.log(error);
              });
            }
        }
    }
</script>