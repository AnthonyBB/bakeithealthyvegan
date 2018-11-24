<template>
  <v-container>
    <v-form>
      <v-layout text-xs-center wrap>
          <v-flex xs12>
            <v-text-field
              v-model="name"
              label="Name"
              placeholder="The name of your recipe."
            ></v-text-field>
          </v-flex>
          <v-flex xs12 sm6 md3>
            <v-text-field
              v-model="description"
              label="Description"
              placeholder="Describe your recipe."
            ></v-text-field>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 sm6 md3>
            <v-text-field
              v-model="batchSize"
              label="Batch Size"
              placeholder="How many items does this recipe make?"
            ></v-text-field>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 sm6 md3>
            <v-text-field
              v-model="totalCost"
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
            <v-text-field
              v-model="ingredientUnitOfMeasure"
              label="Unit of Measure"
              placeholder="Cups, tablespoons, teaspons, etc"
            ></v-text-field>
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
            <v-btn @click="addIngredient()">Add Ingredient</v-btn>
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
                <v-icon large color="green darken-2">edit</v-icon>
              </v-btn>
              <v-btn fab small>
                <v-icon large color="red darken-2">delete_forever</v-icon>
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

export default {
  data: () => ({
    name: "name",
    description: "description",
    batchSize: 10,
    totalCost: 5.95,
    ingredientName: "Flour",
    ingredientUnitOfMeasure: "Cup",
    ingredientUnits: 5,
    headers: [
      { text: "Name", value: "name", align: "left" },
      { text: "Unit of Measure", value: "unitOfMeasure", align: "center" },
      { text: "Units", value: "units", align: "center" },
      { text: "Actions", align: "center", sortable: false }
    ],
    ingredients: [
      {
        id: 1,
        name: "Almond Flour",
        unitOfMeasure: "Cup",
        units: 2
      },
      {
        id: 2,
        name: "Sugar",
        unitOfMeasure: "Tablespoon",
        units: 4
      },
      {
        id: 3,
        name: "Vanilla",
        unitOfMeasure: "Teaspoon",
        units: 1
      }
    ]
  }),
  methods: {
    setRecipe: function() {
      alert("setRecipe");
      axios.put('https://devops-testing.azurewebsites.net/api/put_recipe',
      {
        name: this.name,
        description: this.description,
        batchSize: this.batchSize,
        totalCost: this.totalCost
      }).then((response) => {
        alert("Yuuuup!" + response.data);
      });
    },
    addIngredient: function() {
      alert("addIngredient");
    }
  },
};
</script>
<style>
  .hidden { display: none; }
  hr { margin: 20px 0; }
</style>
