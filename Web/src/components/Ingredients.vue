<template>
  <v-container> 
    <v-layout text-xs-center wrap>
        <v-flex xs12>
          <v-text-field
            label="Ingredient Name"
            placeholder="Ingredient name?"
          ></v-text-field>
        </v-flex>
    </v-layout>
    <v-layout text-xs-center wrap>
        <v-flex xs12>
          <v-text-field
            label="Cups Per Pound"
            placeholder="How many pounds?"
          ></v-text-field>
        </v-flex>
    </v-layout>
    <v-layout text-xs-center wrap>
      <v-flex xs12>
        <v-btn v-on:click="addConversion()">Add Ingredient</v-btn>
      </v-flex>
    </v-layout>
    <v-layout text-xs-left wrap>
      <v-flex xs12>
        <h4>* All measurements are per pound.</h4>
      </v-flex>
    </v-layout>
    <v-layout text-xs-center wrap>
      <v-flex xs12>
        <v-data-table :headers="headers" :items="conversions" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.ingredientName }}</td>
            <td class="text-xs-center">{{ props.item.cupsPerPound }}</td>
            <td class="text-xs-center">{{ props.item.ouncesPerPound }}</td>
            <td class="text-xs-center">{{ props.item.tablespoonsPerPound }}</td>
            <td class="text-xs-center">{{ props.item.teaspoonsPerPound }}</td>
            <td class="text-xs-center">{{ props.item.fluidOuncesPerPound }}</td>
            <td class="text-cs-center">
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
    headers: [
      { text: "Ingredient", value: "ingredientName", align: "left" },
      { text: "Cups", value: "cupsPerPound", align: "center" },
      { text: "Ounces", value: "ouncesPerPound", align: "center" },
      { text: "Tablespoons", value: "tablespoonsPerPound", align: "center" },
      { text: "Teaspoons", value: "teaspoonsPerPound", align: "center" },
      { text: "Fluid Ounces", value: "fluidOuncesPerPound", align: "center" },
      { text: "Actions", align: "center", sortable: false }
    ],
    conversions: [ ]
    }),
    methods: {
      getAllWeightToVolumeConversions: function() {
        axios.get('https://devops-testing.azurewebsites.net/api/get_weight_to_volume_conversions').then((response) => {
          if(response.data)
          {
            let conversionList = [];
            response.data.forEach(function(weightToVolumeConversion) {
              let conversion = {
                "ingredientName": weightToVolumeConversion.ingredientName,
                "cupsPerPound": weightToVolumeConversion.cupsPerPound,
                "ouncesPerPound": "1",
                "tablespoonsPerPound": "2",
                "teaspoonsPerPound": "3",
                "fluidOuncesPerPound": "4"
              }
              conversionList.push(conversion);
            });
            this.conversions = conversionList;
          }
        });
      },
      addConversion: function() {
        alert("addConversion");
      }
    },
    created() {
      this.getAllWeightToVolumeConversions();
    }
};
</script>
