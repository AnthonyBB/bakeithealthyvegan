<template>
  <v-container>
    <v-layout text-xs-center wrap>
      <v-flex xs12>
        <v-data-table :headers="headers" :items="recipes" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.name }}</td>
            <td class="text-xs-left">{{ props.item.description }}</td>
            <td class="text-xs-center">{{ props.item.batchSize }}</td>
            <td class="text-xs-center">{{ props.item.totalCost }}</td>
            <td class="text-xs-center">{{ props.item.totalCost / props.item.batchSize }}</td>
            <td class="text-cs-center">
              <v-btn fab small v-on:click="editRecipe(props.item.name)">
                <v-icon large color="green darken-2">edit</v-icon>
              </v-btn>
              <v-btn fab small v-on:click="deleteRecipe(props.item.name)">
                <v-icon large color="red darken-2">delete_forever</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <v-btn v-on:click="addRecipe()">Add Recipe</v-btn>
      </v-flex>
    </v-layout>

  </v-container>
</template>

<script>
import axios from 'axios'

export default {
  data: () => ({
    headers: [
      { text: "Name", value: "name", align: "left" },
      { text: "Description", value: "description", align: "center", sortable: false },
      { text: "Batch Size", value: "batchSize", align: "center" },
      { text: "Total Cost", value: "totalCost", align: "center" },
      { text: "Individual Cost", align: "center", sortable: false },
      { text: "Actions", align: "center", sortable: false }
    ],
    recipes: [
    ]
  }),
  methods: {
    getAllRecipes: function() {
      axios.get('https://devops-testing.azurewebsites.net/api/get_recipes').then((response) => {
        this.recipes = response.data;
      });
    },
    deleteRecipe: function(name) {
      axios.delete('https://devops-testing.azurewebsites.net/api/delete_recipe', {data: {name: name}}).then(() => {
        this.recipes = this.recipes.filter(function(value){
          return value.name !== name;
        });
      })
    },
    editRecipe: function(name) {
      this.$router.push({ path: `/configure-recipe/${name}`});
    },
    addRecipe: function(name) {
      this.$router.push({ path: `/configure-recipe`});
    }
  },
  created() {
    this.getAllRecipes();
  }
};
</script>
<style>
</style>
