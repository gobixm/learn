function LineItemViewModel(lineItemData) {
    var self = this;
    self.Name = ko.observable(lineItemData.Product.Name);
    self.Price = ko.observable(lineItemData.Price);
    self.Quantity = ko.observable(lineItemData.Quantity);
    self.TotalPrice = ko.observable(self.Price() * self.Quantity());
};