<template>
  <v-container>
    <v-form v-model="valid">
      <v-layout text-xs-center wrap>
        <v-flex xs12>
          <v-text-field
            label="Name"
            placeholder="The name of the ingredient?"
            v-model="name"
            v-if="!isEditMode"
            :rules="nameRules"
            required
          ></v-text-field>
          <h2 v-if="isEditMode">{{ name }}</h2>
        <v-divider></v-divider>
        </v-flex>
        <v-flex xs5>
            <v-select
              :items="unitsOfMeasure"
              label="Unit of Measure"
              placeholder="Cups, tablespoons, teaspons, etc"
              v-model="unitOfMeasure"
              :rules="unitOfMeasureRules"
              required>
            </v-select>
        </v-flex>
        <v-flex xs2>
        </v-flex>
        <v-flex xs5>
          <v-text-field
            label="Units Per Pound"
            placeholder="How many units per pound?"
            v-model="unitsPerPound"
          ></v-text-field>
        </v-flex>
        <v-flex xs12>
        <v-divider></v-divider>
        </v-flex>
        <v-flex xs5>
          <v-text-field
            label="Cost Per Unit"
            placeholder="Cost per unit?"
            v-model="unitCost"
            @click="isPerPound = false"
            :outline="!isPerPound"
          ></v-text-field>
        </v-flex>
        <v-flex xs2>
          <h2 style="width: 100%; text-align: center;">Or</h2>
        </v-flex>
        <v-flex xs5>
          <v-text-field
            label="Cost Per Pound"
            placeholder="Cost per pound?"
            v-model="costPerPound"
            @click="isPerPound = true"
            :outline="isPerPound"
          ></v-text-field>
        </v-flex>
        <v-flex xs12>
          <v-btn :disabled="!valid" @click="setIngredient()" type="submit">Save Ingredient</v-btn>
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

export default {
  data: () => ({
    name: '',
    unitOfMeasure: '',
    unitsPerPound: 0,
    unitCost: 0.00,
    costPerPound: 0.00,
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
    isPerPound: false,
    valid: false,
    nameRules: [
      v => !!v || 'Name is required',
      v => v.length <= 100 || 'Name must be less than 100 characters'
    ],
    unitOfMeasureRules: [
      v => !!v || 'Unit of Measure is required',
    ],
    unitsOfMeasureRules: [
      v => !!v || 'Unit of Measure is required',
    ]
  }),
  methods: {
    setIngredient: function() {
      this.$validator.validate().then(result => {
        axios.put('https://devops-testing.azurewebsites.net/api/put_ingredient',
        {
          name: this.name,
          unitsPerPound: this.unitsPerPound,
          unitOfMeasure: this.unitOfMeasure,
          unitCost: this.isPerPound ? this.costPerPound / this.unitsPerPound : this.unitCost,
          costPerPound: this.isPerPound ? this.costPerPound : null
        }).then(() => {
          this.snackbar.text = "Ingredient Saved.";
          this.snackbar.open = true;
          this.isEditMode = true;
        });
      });
    },
    getIngredient: function(name) {
      axios.get('https://devops-testing.azurewebsites.net/api/get_ingredient?name=' + name).then((response) => {
        this.name = response.data.name,
        this.unitsPerPound = response.data.unitsPerPound,
        this.unitOfMeasure = response.data.unitOfMeasure,
        this.unitCost = response.data.isPerPound ? response.data.costPerPound / response.data.unitsPerPound : response.data.unitCost,
        this.costPerPound = response.data.isPerPound ? response.data.costPerPound : null
        this.isPerPound = response.data.costPerPound != null;
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
