var PaginatorViewModel = function(pageData, select) {
    var self = this;
    self.SelectedPage = ko.observable(pageData.PageNumber);
    self.Pages = ko.observableArray([]);
    self.select = function(data) {
        select(data.page);
    };
    var i;
    for (i = 0; i < pageData.PageCount; i += 1) {
        self.Pages.push({
            page: i + 1,
            selected: self.SelectedPage() == i + 1
        });
    }
}