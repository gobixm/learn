var CategoryViewModel = new function() {
    this.select = function () {
        $.getJSON('/api/product/bycategory?categoryId='+this.Id(), function(data) {
            alert(JSON.stringify(data));
        })
    }
};