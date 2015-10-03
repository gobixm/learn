function ProductsPageViewModel(jsonData, category) {
    var self = this;
    self.Products = ko.observableArray([]);
    self.category = category;
    self.Paginator = ko.observable(jsonData, function(page) {
        alert(page);
    });
    
    $.each(jsonData.Items, function (i, productData) {
        var product = new ProductViewModel(productData);
        self.Products.push(product);
    });
}