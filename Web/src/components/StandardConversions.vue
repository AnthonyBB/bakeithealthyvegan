<template>
  <v-container> 
    <v-layout text-xs-center wrap>
      <v-flex xs12>
        <v-data-table :headers="headers" :items="conversions" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.name }}</td>
            <td class="text-xs-center">{{ props.item.cups }}</td>
            <td class="text-xs-center">{{ props.item.tablespoons }}</td>
            <td class="text-xs-center">{{ props.item.teaspoons }}</td>
            <td class="text-xs-center">{{ props.item.fluidOunces }}</td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

export default {
  data: () => ({
    headers: [
      { text: "Unit of Measure", value: "name", align: "left" },
      { text: "In Cups", value: "cups", align: "center", sortable: false },
      { text: "In Tablespoons", value: "tablespoons", align: "center", sortable: false },
      { text: "In Teaspoons", value: "teaspoons", align: "center", sortable: false },
      { text: "In Fluid Ounces", value: "fluidOunces", align: "center", sortable: false }
    ],
    conversions: []
  }),
  methods: {
    getAllConversions: function() {
      axios.get('https://devops-testing.azurewebsites.net/api/get_conversions').then((response) => {
        this.conversions = response.data;
      });
    }
  },
  created() {
    this.getAllConversions();
  }
};
</script>
