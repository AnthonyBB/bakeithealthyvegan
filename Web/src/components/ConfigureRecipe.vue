<template>
  <v-container>
    <v-form>
      <v-layout text-xs-center wrap>        
        <v-flex xs12>
          <v-text-field
            label="Name"
            placeholder="The name of the recipe?"
            v-model="recipe.name"
             v-if="!isEditMode"
          ></v-text-field>
          <h2 v-if="isEditMode">{{ recipe.name }}</h2>
        </v-flex>
        <v-flex xs12 sm5>
          <v-text-field
            v-model="recipe.description"
            label="Description"
            placeholder="Describe your recipe."
          ></v-text-field>
        </v-flex>
        <v-spacer></v-spacer>
        <v-flex xs12 sm5>
          <v-text-field
            v-model="recipe.batchSize"
            label="Batch Size"
            placeholder="How many items does this recipe make?"
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
            <v-select
              :items="ingredientNames"
              v-model="ingredientName"
              label="Ingredient Name"
              placeholder="Flour, Sugar, etc."
            ></v-select>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 sm6 md3>
            <v-select
              :items="unitsOfMeasure"
              v-model="ingredientUnitOfMeasure"
              label="Unit of Measure"
              placeholder="Cups, tablespoons, teaspons, etc"
              item-text="name"
              item-value="name"
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
    <v-snackbar
      v-model="snackbar.open"
      :bottom="snackbar.y === 'bottom'"
      :left="snackbar.x === 'left'"
      :multi-line="snackbar.mode === 'multi-line'"
      :right="snackbar.x === 'right'"
      :timeout="snackbar.timeout"
      :top="snackbar.y === 'top'"
      :vertical="snackbar.mode === 'vertical'"
    >
      {{ snackbar.text }}
      <v-btn
        color="pink"
        flat
        @click="snackbar.open = false"
      >
        Close
      </v-btn>
    </v-snackbar>
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
    ingredientNames: [],
    ingredientName: null,
    ingredientUnitOfMeasure: null,
    ingredientUnits: null,
    snackbar: {
      open: false,
      y: 'top',
      x: null,
      mode: '',
      timeout: 5000,
      text: ''
    },
    isEditMode: false,
    nameRules: [
      v => !!v || 'Name is required'
    ],
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
      }).then(() => {
        this.snackbar.text = "Recipe Saved.";
        this.snackbar.open = true;
        this.isEditMode = true;
      });
    },
    getRecipe: function(name) {
      var promise = axios.get('https://devops-testing.azurewebsites.net/api/get_recipe?name=' + name);
      promise.then((response) => {
        this.recipe = response.data;
        this.ingredients = this.recipe.ingredients;
        if(this.ingredients == null)
        {
          this.ingredients = [];
        }
        
        this.getAllConversionNames();
      });
      return promise;
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
    getAllConversionNames: function() {
      var promise = axios.get('https://devops-testing.azurewebsites.net/api/get_conversions');
      promise.then((response) => {
        this.unitsOfMeasure = response.data;
        this.getAllIngredientNames();
      });
      return promise;
    },  
    getAllIngredientNames: function() {
      var promise = axios.get('https://devops-testing.azurewebsites.net/api/get_ingredients');
      promise.then((response) => {
        this.ingredientNames = response.data.map(x => x.name);
      });
      return promise;
    }
  },
  created() {
    this.isEditMode = this.$route.params.name != null;
    if(this.isEditMode)
    {
    this.getRecipe(this.$route.params.name);
    }
  }
};
</script>
<style>
  .hidden { display: none; }
  hr { margin: 20px 0; }
</style>
