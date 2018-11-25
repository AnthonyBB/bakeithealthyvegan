import Vue from 'vue'
import Router from 'vue-router'
import Home from "./components/Home";
import Recipes from "./components/Recipes";
import ConfigureRecipe from "./components/ConfigureRecipe";
import StandardConversions from "./components/StandardConversions";
import IngredientConversions from "./components/IngredientConversions";
import Ingredients from "./components/Ingredients";

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/home',
      name: 'Home',
      component: Home
    },
    {
      path: '/recipes',
      name: 'Recipes',
      component: Recipes
    },
    {
      path: '/configure-recipe/:name?',
      name: 'Configure Recipe',
      component: ConfigureRecipe
    },
    {
      path: '/standard-conversions',
      name: 'Standard Conversions',
      component: StandardConversions
    },
    {
      path: '/ingredient-conversions',
      name: 'Ingredient Conversions',
      component: IngredientConversions
    },
    {
      path: '/ingredients',
      name: 'Ingredients',
      component: Ingredients
    }
  ]
})