import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Cliente from '../components/Cliente.vue'
import VMClient from '../components/VMClient.vue'
import NetworkBond from '../components/NetworkBond.vue'
import OSFamily from '../components/OSFamily.vue'
import OSVersion from '../components/OSVersion.vue'
import SQLFamily from '../components/SQLFamily.vue'
import SQLVersion from '../components/SQLVersion.vue'
import VMType from '../components/VMType.vue'
import Pools from '../components/Pools.vue'
import Rol from '../components/Rol.vue'
import Usuario from '../components/Usuario.vue'
import Login from '../components/Login.vue'
import store from '../store/index'
import VPS from '../components/VPS.vue'
import Notifivps from '../components/Notifivps.vue'
import ConfigMail from '../components/ConfigMail.vue'

Vue.use(Router)

var router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta:{
        administrador:true,
        soporte:true,
        gerente:true
      }
    },
    {
      path: '/clientes',
      name: 'clientes',
      component: Cliente,
      meta:{
        administrador:true,
        soporte:true,
        gerente:true
      }
    },
    {
      path: '/vmclients',
      name: 'vmclients',
      component: VMClient,
      meta:{
        administrador:true,
        soporte:true,
        gerente:true
      }
    },
    {
      path: '/networkbonds',
      name: 'networkbonds',
      component: NetworkBond,
      meta:{
        administrador:true,
        soporte:true
      }
    },
    {
      path: '/osfamilys',
      name: 'osfamilys',
      component: OSFamily,
      meta :{
        administrador:true,
        soporte:true
      }
    },
    {
      path: '/osversions',
      name: 'osversions',
      component: OSVersion,
      meta :{
        administrador:true,
        soporte:true
      }
    },
    {
      path: '/sqlfamilys',
      name: 'sqlfamilys',
      component: SQLFamily,
      meta :{
        administrador:true,
        soporte:true
      }
    },
    {
      path: '/sqlversions',
      name: 'sqlversions',
      component: SQLVersion,
      meta :{
        administrador:true,
        soporte:true
      }
    },
    {
      path: '/vmtypes',
      name: 'vmtypes',
      component: VMType,
      meta :{
        administrador:true,
        soporte:true
      }
    },
    {
      path: '/poolss',
      name: 'poolss',
      component: Pools,
      meta :{
        administrador:true,
        soporte:true
      }
    },
    {
      path: '/rols',
      name: 'rols',
      component: Rol,
      meta :{
        administrador:true
      }
    },
    {
      path: '/usuarios',
      name: 'usuarios',
      component: Usuario,
      meta:{
        administrador:true
      }
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
      meta:{
        libre:true
      }
    },
    {
      path: '/vpss',
      name: 'vpss',
      component: VPS,
      meta :{
        administrador:true
      }
    },
    {
      path: '/notifivps',
      name: 'notifivps',
      component: Notifivps,
      meta:{
        administrador:true,
        soporte:true,
        gerente:true
      }
    },
    {
      path: '/configmails',
      name: 'configmails',
      component: ConfigMail,
      meta:{
        administrador:true,
        gerente:true
      }
    }
  ]
})

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.libre)){
    next()
  } else if (store.state.usuario && store.state.usuario.rol== 'Administrador'){
    if (to.matched.some(record => record.meta.administrador)){
      next()
    }
  }else if (store.state.usuario && store.state.usuario.rol== 'Soporte'){
    if (to.matched.some(record => record.meta.soporte)){
      next()
    }
  }else if (store.state.usuario && store.state.usuario.rol== 'Gerente'){
    if (to.matched.some(record => record.meta.gerente)){
      next()
    }
  } else{
    next({
      name: 'login'
    })
  }
})

export default router