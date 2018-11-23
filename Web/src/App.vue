<template>
  <v-app>
    <v-navigation-drawer app permanent>
      <v-list>
        <v-list-tile :to="{path: '/home'}">
          <v-list-tile-action>
            <v-icon>home</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Home</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{path: '/recipes'}">
          <v-list-tile-action>
            <v-icon>library_books</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Recipes</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{path: '/configure-recipe'}">
          <v-list-tile-action>
            <v-icon>edit</v-icon>
          </v-list-tile-action>
          <v-list-tile-content> 
            <v-list-tile-title>Configure Recipe</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{path: '/standard-conversions'}">
          <v-list-tile-action>
            <v-icon>replay</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Standard Conversions</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="{path: '/ingredient-conversions'}">
          <v-list-tile-action>
            <v-icon>replay_10</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Ingredient Specific Conversions</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar app>
      <v-toolbar-title class="headline text-uppercase">
        <span>Bake It Healthy Vegan</span>
        <span class="font-weight-light"> Live Health, Live Peace</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>
    <v-content>
        <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
export default {
  name: "App",
  components: { }
};
</script>
<style>
nav a {
  margin: 0 5px;
}
</style>