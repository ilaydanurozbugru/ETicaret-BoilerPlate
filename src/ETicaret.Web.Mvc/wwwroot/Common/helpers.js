var app = app || {};
(function () {

    app.resetFilter = function (form) {
        $("textarea.datatable-filter, select.datatable-filter, input.datatable-filter").each(function () {

            $(this).val("")
        });
        $('input.datatable-filter[type="checkbox"]').each(function () {
            $(this).attr("checked", !1)
        });

    };  

   

    app.htmlUtils = {
        htmlEncodeText: function (value) {
            return $("<div/>").text(value).html();
        },

        htmlDecodeText: function (value) {
            return $("<div/>").html(value).text();
        },

        htmlEncodeJson: function (jsonObject) {
            return JSON.parse(app.htmlUtils.htmlEncodeText(JSON.stringify(jsonObject)));
        },

        htmlDecodeJson: function (jsonObject) {
            return JSON.parse(app.htmlUtils.htmlDecodeText(JSON.stringify(jsonObject)));
        }
    };   

   
   
     
})();