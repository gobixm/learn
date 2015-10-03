var CategoryViewModel = new function() {
    this.select = function () {
        mainViewModel.LoadProducts(this.Id());
    }
};