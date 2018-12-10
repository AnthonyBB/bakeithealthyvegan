<template>
  <v-container>
    <v-layout text-xs-center wrap>
      <v-flex xs12 v-if="isLoadingRecipes">
        <v-progress-circular :size="50" color="primary" indeterminate></v-progress-circular>
      </v-flex>
      <v-flex xs12 v-if="!isLoadingRecipes">
        <v-data-table :headers="headers" :items="recipes" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.name }}</td>
            <td class="text-xs-left">{{ props.item.description }}</td>
            <td class="text-xs-center">{{ props.item.batchSize }}</td>
            <td class="text-xs-center">{{ props.item.totalCost.toFixed(2) }}</td>
            <td
              class="text-xs-center"
            >{{ (props.item.totalCost / props.item.batchSize).toFixed(2) }}</td>
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
import axios from "axios";

export default {
  data: () => ({
    headers: [
      { text: "Name", value: "name", align: "left" },
      {
        text: "Description",
        value: "description",
        align: "center",
        sortable: false
      },
      { text: "Batch Size", value: "batchSize", align: "center" },
      { text: "Total Cost", value: "totalCost", align: "center" },
      { text: "Individual Cost", align: "center", sortable: false },
      { text: "Actions", align: "center", sortable: false }
    ],
    recipes: [],
    isLoadingRecipes: false,
    ingredients: []
  }),
  methods: {
    getAllRecipes: function() {
      var that = this;
      this.isLoadingRecipes = true;
      axios
        .get("https://devops-testing.azurewebsites.net/api/get_recipes")
        .then(response => {
          that.recipes = response.data;
          that.getAllIngredients();
          that.isLoadingRecipes = false;
        })
        .catch(function() {
          that.isLoadingRecipes = false;
        });
    },
    deleteRecipe: function(name) {
      axios
        .delete("https://devops-testing.azurewebsites.net/api/delete_recipe", {
          data: { name: name }
        })
        .then(() => {
          this.recipes = this.recipes.filter(function(value) {
            return value.name !== name;
          });
        });
    },
    editRecipe: function(name) {
      this.$router.push({ path: `/configure-recipe/${name}` });
    },
    addRecipe: function() {
      this.$router.push({ path: `/configure-recipe` });
    },
    getTotalCost: function(recipeIngredients) {
      //let totalCost = 0.0;
      var that = this;
      let matchingIngredients = [];
      if (recipeIngredients) {
        for(let recipeIngredient of recipeIngredients) {
          if (that.ingredients) {
            for(let ingredient of that.ingredients) {
              if (
                recipeIngredient.name.toLowerCase() ===
                ingredient.name.toLowerCase()
              ) {
                let match = {
                  unitCost: ingredient.unitCost,
                  units: recipeIngredient.units,
                  convertedUnits: that.convertToRecipeIngredientUnitOfMeasure(ingredient.unitOfMeasure, recipeIngredient.unitOfMeasure, that.conversions, recipeIngredient.units)
                };
                matchingIngredients.push(match);
                break;
              }
            }
          }
        }
      }
      var recipeCost = 0;
      matchingIngredients.forEach(function(match) {
        recipeCost += match.unitCost * match.convertedUnits;
      });
      return recipeCost;
    },
    getAllIngredients: function() {
      var that = this;
      axios
        .get("https://devops-testing.azurewebsites.net/api/get_ingredients")
        .then(response => {
          this.ingredients = response.data;
          that.getAllConversions();
        });
    },
    getAllConversions: function() {
      var that = this;
      axios.get('https://devops-testing.azurewebsites.net/api/get_conversions').then((response) => {
        this.conversions = response.data;
        for(let recipe of that.recipes) {
            recipe.totalCost = that.getTotalCost(recipe.ingredients);
        }
      });
    },
    convertToRecipeIngredientUnitOfMeasure: function(ingredientUnitOfMeasure, recipeIngredientUnitOfMeasure, standardConversions, amount)
    {
      for(let standardConversion of standardConversions) {
          if(recipeIngredientUnitOfMeasure.toLowerCase() == standardConversion.name.toLowerCase())
          {
            let ratio = standardConversion[`${ingredientUnitOfMeasure}`];
            let conversion = eval(ratio) * amount;
            return conversion;
          }
        }
          return null;
    }
  },
  created() {
    this.getAllRecipes();
  }
};
</script>
<style>
</style>
