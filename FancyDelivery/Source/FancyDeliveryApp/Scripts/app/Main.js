function MainViewModel() {
    var self = this;
    self.Categories = ko.observableArray([]);
    self.ProductsPageViewModel = ko.observable();

    self.LoadCategories = function () {
        $.getJSON('/api/category/all', function (data) {
            $.each(data, function (i, category) {
                var cat = ko.mapping.fromJS(category);
                $.extend(cat, CategoryViewModel);
                self.Categories.push(cat);
            });
        });
    }

    self.LoadProducts = function (category, page, pagesize) {
        if (page === undefined) {
            page = 1;
        }

        if (pagesize === undefined) {
            pagesize = 20;
        }
            
        $.getJSON('/api/product/bycategory?categoryId=' + category + '&pageNumber=' + page + '&pageSize='+pagesize, function (data) {
            self.ProductsPageViewModel(new ProductsPageViewModel(data, category));
        });
    }
}

var mainViewModel;

$(document).ready(function () {
    mainViewModel = new MainViewModel();
    ko.applyBindings(mainViewModel);
    mainViewModel.LoadCategories();
});