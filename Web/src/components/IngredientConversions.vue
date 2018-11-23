<template>
  <v-container> 
    <v-layout text-xs-center wrap>
        <v-flex xs12>
          <v-text-field
            label="Ingredient"
            placeholder="Select ingredient."
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
        <v-data-table :headers="headers" :items="conversions" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td class="text-xs-left">{{ props.item.ingredientName }}</td>
            <td class="text-xs-center">{{ props.item.cupsPerPound }}</td>
            <td class="text-xs-center">{{ props.item.ouncesPerPound }}</td>
            <td class="text-xs-center">{{ props.item.tablespoonsPerPound }}</td>
            <td class="text-xs-center">{{ props.item.teaspoonsPerPound }}</td>
            <td class="text-xs-center">{{ props.item.fluidOuncesPerPound }}</td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

export default {
  data: () => ({
    headers: [
      { text: "Ingredient", value: "ingredientName", align: "left" },
      { text: "Cups Per Pound", value: "cupsPerPound", align: "center" },
      { text: "Ounces Per Pound", value: "ouncesPerPound", align: "center" },
      { text: "Tablespoons Per Pound", value: "tablespoonsPerPound", align: "center" },
      { text: "Teaspoons Per Pound", value: "teaspoonsPerPound", align: "center" },
      { text: "Fluid Ounces Per Pound", value: "fluidOuncesPerPound", align: "center" },
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
      }
    },
    created() {
      this.getAllWeightToVolumeConversions();
    }
};
</script>
