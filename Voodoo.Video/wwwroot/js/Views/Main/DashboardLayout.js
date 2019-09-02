let hiddenLayout = $("#layout-name-hidden");
let tileCount = parseInt(hiddenLayout.data("qty"));

if (tileCount > 0) {
    //! Initialize the grid stack.        
    $('.grid-stack').gridstack({"animate": true, float: true, minHeight: 2});

//! Enable clean looking scroll bars.
    $('.scrollbar-inner').scrollbar();
}

  
//! Set the layout title with the name of the current layout.

$("#current-layout-name").html(hiddenLayout.val()).data("layoutid", hiddenLayout.data("id"));

//$ Get the new layout form.
$(".new-layout").on(
    "click",
    function () {
        $.ajax({
            type: "GET",
            url: "/new-layout-form",
            success: function (response, status, xhr) {
                $("#body-content").html(response);
            }
        });
    });
