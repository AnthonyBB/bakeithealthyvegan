<template>
  <v-container>
    <v-form>
      <v-layout text-xs-center wrap>
        <v-flex xs12>
          <v-text-field
            label="Name"
            placeholder="The name of the ingredient?"
            v-model="ingredient.name"
             v-if="!isEditMode"
          ></v-text-field>
          <h2 v-if="isEditMode">{{ ingredient.name }}</h2>
        <v-divider></v-divider>
        </v-flex>
        <v-flex xs5>
            <v-select
              :items="unitsOfMeasure"
              label="Unit of Measure"
              placeholder="Cups, tablespoons, teaspons, etc"
              v-model="ingredient.unitOfMeasure">
            </v-select>
        </v-flex>
        <v-flex xs2>
        </v-flex>
        <v-flex xs5>
          <v-text-field
            label="Units Per Pound"
            placeholder="How many units per pound?"
            v-model="ingredient.unitsPerPound"
          ></v-text-field>
        </v-flex>
        <v-flex xs12>
        </v-flex>
        <v-flex xs12>
          <h2 style="width: 100%; text-align: center;">or</h2>
        </v-flex>
        <v-flex xs5>
          <v-text-field
            label="Cost Per Unit"
            placeholder="Cost per unit?"
            v-model="ingredient.unitCost"
            @click="isPerPound = false"
            :outline="!isPerPound"
          ></v-text-field>
        </v-flex>
        <v-flex xs2>
        </v-flex>
        <v-flex xs5>
          <v-text-field
            label="Cost Per Pound"
            placeholder="Cost per pound?"
            v-model="ingredient.costPerPound"
            @click="isPerPound = true"
            :outline="isPerPound"
          ></v-text-field>
        </v-flex>
        <v-flex xs12>
          <v-btn @click="setIngredient()">Save Ingredient</v-btn>
        </v-flex>
      </v-layout>
    </v-form>
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
    ingredient: {
      name: null,
      unitOfMeasure: null,
      unitsPerPound: 0,
      unitCost: 0.00,
      costPerPound: 0.00
     },
    unitsOfMeasure: [],
    snackbar: {
      open: false,
      y: 'top',
      x: null,
      mode: '',
      timeout: 5000,
      text: ''
    },
    isEditMode: false,
    isPerPound: false
  }),
  methods: {
    setIngredient: function() {
      axios.put('https://devops-testing.azurewebsites.net/api/put_ingredient',
      {
        name: this.ingredient.name,
        unitsPerPound: this.ingredient.unitsPerPound,
        unitOfMeasure: this.ingredient.unitOfMeasure,
        unitCost: this.IsPerPound ? null : this.ingredient.unitCost,
        costPerPound: this.IsPerPound ? this.ingredient.costPerPound : null
      }).then(() => {
        this.snackbar.text = "Ingredient Saved.";
        this.snackbar.open = true;
        this.isEditMode = true;
      });;
    },
    getIngredient: function(name) {
      axios.get('https://devops-testing.azurewebsites.net/api/get_ingredient?name=' + name).then((response) => {
        this.ingredient = response.data;
      })
    },
    getAllConversionNames: function() {
      axios.get('https://devops-testing.azurewebsites.net/api/get_conversions').then((response) => {
        this.unitsOfMeasure = response.data.map(x => x.name);
      });
    }
  },
  created() {
    this.isEditMode = this.$route.params.name != null;
    if(this.isEditMode)
    {
      this.getIngredient(this.$route.params.name);
    }
    this.getAllConversionNames();
  }
};
</script>
<style>
  .hidden { display: none; }
  hr { margin: 20px 0; }
  h2 { text-align: left; }
</style>
