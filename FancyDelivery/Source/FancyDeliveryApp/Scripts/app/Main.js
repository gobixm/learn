function MainViewModel() {
    var self = this;
    self.Categories = ko.observableArray([]);
    self.LoadCategories = function () {
        $.getJSON('/api/category/all', function (data) {
            $.each(data, function (i, category) {
                var cat = ko.mapping.fromJS(category);
                $.extend(cat, CategoryViewModel);
                self.Categories.push(cat);
            });
        });
    }
}

var mainViewModel = new MainViewModel();
ko.applyBindings(mainViewModel);

$(document).ready(function () {
    mainViewModel.LoadCategories();
});