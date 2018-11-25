<template>
  <v-container>
    <v-form>
      <v-layout text-xs-center wrap>
          <v-flex xs12>
            <v-text-field 
              v-if="this.$route.params.name == null"
              v-model="recipe.name"
              label="Name"
              placeholder="The name of your recipe."
            ></v-text-field>
          </v-flex>
          <v-flex xs12 sm6 md3>
            <v-text-field
              v-model="recipe.description"
              label="Description"
              placeholder="Describe your recipe."
            ></v-text-field>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 sm6 md3>
            <v-text-field
              v-model="recipe.batchSize"
              label="Batch Size"
              placeholder="How many items does this recipe make?"
            ></v-text-field>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 sm6 md3>
            <v-text-field
              v-model="recipe.totalCost"
              label="Total Cost"
              placeholder="What is the total cost of this batch?"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-btn @click="setRecipe()">Save Recipe</v-btn>
          </v-flex>
      </v-layout>
    </v-form>
    <v-divider></v-divider>
    <v-form>
      <v-layout text-xs-center wrap>
          <v-flex xs12 sm6 md3>
            <v-text-field
              v-model="ingredientName"
              label="Ingredient"
              placeholder="Select an ingredient"
            ></v-text-field>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 sm6 md3>
            <v-select
              :items="unitsOfMeasure"
              label="Standard"
              placeholder="Cups, tablespoons, teaspons, etc"
            ></v-select>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 sm6 md3>
            <v-text-field
              v-model="ingredientUnits"
              label="Units"
              placeholder="How many?"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-btn @click="addIngredient(ingredientName, ingredientUnitOfMeasure, ingredientUnits)">Add Ingredient</v-btn>
          </v-flex>
      </v-layout>
    </v-form>
    <v-layout text-xs-center wrap>
      <v-flex xs12>
        <v-data-table :headers="headers" :items="ingredients" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.name }}</td>
            <td class="text-xs-center">{{ props.item.unitOfMeasure }}</td>
            <td class="text-xs-center">{{ props.item.units }}</td>
            <td class="text-cs-center">
              <v-btn fab small>
                <v-icon large color="red darken-2" v-on:click="deleteIngredient(props.item.id)">delete_forever</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import axios from 'axios'

import UtilityService from '../services/utilityService'

export default {
  data: () => ({
    headers: [
      { text: "Name", value: "name", align: "left" },
      { text: "Unit of Measure", value: "unitOfMeasure", align: "center" },
      { text: "Units", value: "units", align: "center" },
      { text: "Actions", align: "center", sortable: false }
    ],
    recipe: {
      name: null,
      description: null,
      batchSize: 0,
      totalCost: 0.00
     },
    ingredients: [],
    unitsOfMeasure: [],
    ingredientName: "Flour",
    ingredientUnitOfMeasure: "Cup",
    ingredientUnits: "5"
  }),
  methods: {
    setRecipe: function() {
      axios.put('https://devops-testing.azurewebsites.net/api/put_recipe',
      {
        name: this.recipe.name,
        description: this.recipe.description,
        batchSize: this.recipe.batchSize,
        totalCost: this.recipe.totalCost,
        ingredients: this.ingredients
      });
    },
    getRecipe: function(name) {
      axios.get('https://devops-testing.azurewebsites.net/api/get_recipe?name=' + name).then((response) => {
        this.recipe = response.data;
        this.ingredients = this.recipe.ingredients;
        if(this.ingredients == null)
        {
          this.ingredients = [];
        }
      })
    },
    addIngredient: function(name, unitOfMeasure, units) {
      const ingredient = {
        id: UtilityService.generateId(),
        name: name,
        unitOfMeasure: unitOfMeasure,
        units: units
      };
      if(!this.ingredients)
      {
        this.ingredients = [];
      }
      this.ingredients.push(ingredient);
      this.setRecipe();
    },
    deleteIngredient: function(id) {
      this.ingredients = this.ingredients.filter(function(value){
        return value.id !== id;
      });
      this.setRecipe();
    },
    getAllConversions: function() {
      axios.get('https://devops-testing.azurewebsites.net/api/get_conversions').then((response) => {
        this.unitsOfMeasure = response.data.map(x => x.name);
      });
    }
  },
  created() {
    this.getRecipe(this.$route.params.name);
    this.getAllConversions();
  }
};
</script>
<style>
  .hidden { display: none; }
  hr { margin: 20px 0; }
</style>
