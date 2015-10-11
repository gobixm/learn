function CartViewModel(jsonData) {
    var self = this;
    self.LineItems = ko.observableArray([]);
    self.Address = ko.observable();

    if (jsonData != null && jsonData.LineItems != null) {
        $.each(jsonData.LineItems, function (i, lineItemData) {
            var lineItem = new LineItemViewModel(lineItemData);
            self.LineItems.push(lineItem);
        });
    }

    self.confirm = function () {
        $('#shipModal').modal();
    };

    self.ship = function () {
        $('#shipModal').modal('hide');
        $.ajax({
            url: '/api/cart/ship',
            type: "POST",
            data: JSON.stringify(self.Address()),
            contentType: "application/json"
        }).done(function () {
            mainViewModel.LoadCart();
        }).fail(function () {
            alert('fail');
        });
    };
}