export default {
  getAll() {
      this.$http.get('https://devops-testing.azurewebsites.net/api/get_recipes')
        .then(response => {
            return response.data.sources;
        });
  }
}