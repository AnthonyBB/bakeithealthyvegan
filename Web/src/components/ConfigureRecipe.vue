<template>
  <v-container>    
    <v-layout text-xs-center wrap>
        <v-flex xs12 sm6 md3>
          <v-text-field
            label="Ingredient"
            placeholder="Select an ingredient"
          ></v-text-field>
        </v-flex>
        <v-spacer></v-spacer>
        <v-flex xs12 sm6 md3>
          <v-text-field
            label="Unit of Measure"
            placeholder="Cups, tablespoons, teaspons, etc"
          ></v-text-field>
        </v-flex>
        <v-spacer></v-spacer>
        <v-flex xs12 sm6 md3>
          <v-text-field
            label="Units"
            placeholder="How many?"
          ></v-text-field>
        </v-flex>
    </v-layout>
    <v-layout text-xs-center wrap>
      <v-flex xs12>
        <v-data-table :headers="headers" :items="ingredients" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.name }}</td>
            <td class="text-xs-center">{{ props.item.unitOfMeasure }}</td>
            <td class="text-xs-center">{{ props.item.units }}</td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
  data: () => ({
    headers: [
      { text: "Name", value: "name", align: "left" },
      { text: "Unit of Measure", value: "unitOfMeasure", align: "center" },
      { text: "Units", value: "units", align: "center" }
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
  })
};
</script>
<style>
  .hidden { display: none; }
</style>
