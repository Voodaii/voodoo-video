

// Submit the new layout form.
$("#btn-create-layout").on("click", function() {
    $('#frm-new-layout').submit();
});

$("#btn-cancel-layout").on("click", function() {
    GetPreviousLayout();
});

function GetPreviousLayout() {
    const model = { "id": $("#current-layout-name").data("layoutid") };
    $.ajax({
        type: "GET",
        url: "/get-dashboard-layout",
        data: model,
        success: function(response) {
            $("#body-content").html(response);
        }
    });
}