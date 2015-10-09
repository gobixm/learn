function ProductViewModel(productData) {
    var self = this;
    self.Name = ko.observable(productData.Name);
    self.Id = ko.observable(productData.Id);
    self.Price = ko.observable(productData.Price);
    self.ImageName = ko.observable(productData.ImageName);
    self.add_to_cart = function(product) {
        mainViewModel.AddProductToCart(product);
    };
}