// Make sure to call Vue.use(Vuex) first if using a module system
import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export const store  = new Vuex.Store({
    state: {
        count: 0
    },
    getters: {},
    mutations: {
      increment (state) {
        state.count++
      }
    },
    actions: {}
});