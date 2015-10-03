function ProductViewModel(productData) {
    var self = this;
    self.Name = ko.observable(productData.Name);
    self.Id = ko.observable(productData.Id);
}