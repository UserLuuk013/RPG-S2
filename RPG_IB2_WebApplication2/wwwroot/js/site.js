// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowItemDetails(itemID) {
    $.ajax({
        url: "/Item/GetItemByID",
        data: { 'itemID': itemID },
        success: function (data) {
            $("#changespace").html(data);
        }
    });
};