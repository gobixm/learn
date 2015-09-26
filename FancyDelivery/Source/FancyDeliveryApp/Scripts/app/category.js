$(document).ready(function () {
    $.getJSON('/default/api/category/categories')
        .done(function (data) {
            $('#categories').html(JSON.stringify(data, null, 2));
        });
        });
