<template>
  <v-container>
    <v-layout text-xs-center wrap>
      <v-flex xs12>
        <v-data-table :headers="headers" :items="recipes" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.name }}</td>
            <td class="text-xs-center">{{ props.item.description }}</td>
            <td class="text-xs-center">{{ props.item.batchSize }}</td>
            <td class="text-xs-center">{{ props.item.totalCost }}</td>
            <td class="text-xs-center">{{ props.item.totalCost / props.item.batchSize }}</td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <v-btn color="increment">Increment</v-btn>
        <v-btn color="decrement">Decrement</v-btn>
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
      { text: "Name", value: "name", align: "left" },
      { text: "Description", value: "description", align: "center", sortable: false },
      { text: "Batch Size", value: "batchSize", align: "center" },
      { text: "Total Cost", value: "totalCost", align: "center" },
      { text: "Individual Cost", align: "center", sortable: false }
    ],
    recipes: [
    ]
  }),
  methods: {
    getAllRecipes: function() {
      axios.get('https://devops-testing.azurewebsites.net/api/get_recipes').then((response) => {
        this.recipes = response.data;
      });
    }
  },
  created() {
    this.getAllRecipes();
  }
};
</script>
<style>
</style>