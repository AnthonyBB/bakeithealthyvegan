<template>
  <v-container> 
     <v-layout text-xs-center wrap>
      <v-flex xs12 v-if="isLoadingIngredients">
        <v-progress-circular :size="50" color="primary" indeterminate>
        </v-progress-circular>
      </v-flex>
      <v-flex xs12 v-if="!isLoadingIngredients">
        <v-data-table :headers="headers" :items="ingredients" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.name }}</td>
            <td class="text-xs-center">{{ props.item.unitOfMeasure }}</td>
            <td class="text-xs-center">{{ props.item.unitsPerPound }}</td>
            <td class="text-xs-center">{{ props.item.unitCost }}</td>
            <td class="text-cs-center">
            <v-btn fab small v-on:click="editIngredient(props.item.name)">
              <v-icon large color="green darken-2">edit</v-icon>
            </v-btn>
              <v-btn fab small v-on:click="deleteIngredientConfirm(props.item.name)">
                <v-icon large color="red darken-2">delete_forever</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <v-btn v-on:click="addIngredient()">Add Ingredient</v-btn>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <div class="text-xs-center">
          <v-dialog
            v-model="dialog"
            width="500"
          >

            <v-card>
              <v-card-title
                class="headline grey lighten-2"
                primary-title
              >
                Confirm Ingredient Deletion
              </v-card-title>

              <v-card-text>
                Are you sure you want to delete ingredient {{ deleteIngredientName }}?
              </v-card-text>

              <v-divider></v-divider>

              <v-card-actions>
                <v-btn
                  color="secondary"
                  flat
                  @click="dialog = false"
                >
                  Cancel
                </v-btn>
                <v-spacer></v-spacer>
                <v-btn
                  color="primary"
                  flat
                  @click="deleteIngredient()"
                >
                  Confirm Deletion
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </div>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import axios from 'axios'

export default {
  data: () => ({
    headers: [
      { text: "Ingredient", value: "name", align: "left" },
      { text: "Unit Of Measure", value: "unitOfMeasure", align: "center" },
      { text: "Units Per Pound", value: "unitsPerPound", align: "center" },
      { text: "Unit Cost", value: "unitCost", align: "center" },
      { text: "Actions", align: "center", sortable: false }
    ],
    ingredients: [ ],
    isLoadingIngredients: false,
    name: "Flour",
    unitOfMeasure: "Cup",
    unitsPerPound: 0.00,
    unitCost: 0.00,
    unitsOfMeasure: [],
    dialog: false,
    deleteIngredientName: null
    }),
    methods: {
      getAllIngredients: function() {
        this.isLoadingIngredients = true;
        axios.get('https://devops-testing.azurewebsites.net/api/get_ingredients').then((response) => {
          this.ingredients = response.data;
          this.isLoadingIngredients = false;
        })
        .catch(function () {
            this.isLoadingIngredients = false;
        });
      },
      addIngredient: function() {
        this.$router.push({ path: `/configure-ingredient/`});
      },
      deleteIngredientConfirm: function(name) {
        this.deleteIngredientName = name;
        this.dialog = true;
      },
      deleteIngredient: function() {
        const name = this.deleteIngredientName;
        this.deleteIngredientName = null;
        this.dialog = false;
        axios.delete('https://devops-testing.azurewebsites.net/api/delete_ingredient', {data: {name: name}}).then(() => {
          this.ingredients = this.ingredients.filter(function(value){
            return value.name !== name;
          });
        })
      },
      editIngredient: function(name) {
        this.$router.push({ path: `/configure-ingredient/${name}`});
      }
    },
    created() {
      this.getAllIngredients();
    }
};
</script>
