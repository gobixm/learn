function ProductsPageViewModel(jsonData, category) {
    var self = this;
    self.Products = ko.observableArray([]);
    self.category = category;
    self.Paginator = ko.observable(new PaginatorViewModel(jsonData, function(page) {
        mainViewModel.LoadProducts(self.category, page);
    }));

    self.PageNumber = jsonData.PageNumber;
    
    $.each(jsonData.Items, function (i, productData) {
        var product = new ProductViewModel(productData);
        self.Products.push(product);
    });
}