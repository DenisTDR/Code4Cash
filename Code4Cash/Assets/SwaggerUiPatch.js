var init = function () {
    var form = $("#api_selector");

    var input_field = form.find("#input_baseUrl");
    input_field
      .attr("placeholder", "Authorization Token")
      .val("");
    input_field.on("keyup", function () {
        form.find("#explore").click();
    });

    form.find("#input_apiKey").parent().detach();
    form.find("#explore").html("Set").click(function (e) {

        $("[id^=mAuthorization0]").val(input_field.val());

        return false;
    });
}

init();
setTimeout(function () {
    init();
}, 200);