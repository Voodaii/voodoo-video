let body = $("body");

$(document).ready(function () {
    //! Sidebar is completely gone at <= 768px;
    
    //!! User Profile dropdown list.
    $("#drop2").on("click", function () {
        $("#submenu2").slideToggle();
        $("#profile-chev").toggleClass("rup rdn");
    });

    //!! Dashboard layouts dropdown list.
    $("#drop1").on("click", function () {
        $("#submenu1").slideToggle();
        $("#dash-chev").toggleClass("rup rdn");
    });

    //!! Collapse/Expand icon
    $('#collapse-icon').addClass('fa-angle-double-left');

    //!! Collapse click
    $('[data-toggle=sidebar-collapse]').click(function () {
        $('#collapse-icon').toggleClass('fa-angle-double-left fa-angle-double-right');
        $('.fa-chevron-down').toggleClass("d-none");
        $(".sidebar-title.l2").toggleClass("font-norm font-sm");
        $("#msg-badge").toggleClass("msg-badge-r msg-badge-l");
        $(".sidebar-label").toggleClass("sidebar-label-shown sidebar-label-hidden");
        $(".sidebar, .sidebar>ul, .sidebar>ul>li").toggleClass("sidebar-expanded sidebar-collapsed");
        $(".sidebar-sub-label").toggleClass("sidebar-label-shown sidebar-label-hidden");
    });

    //!! Get the existing dashboard layouts.
    GetDashboardLayouts();

    //!! Get the new layout form.
    $(".new-layout").on("click", function () {
        $.ajax({
            method: "GET",
            url: "/new-layout-form",
            success: function (response) {
                $("#body-content").html(response);
            }
        });
    });

    //!! Get the dashboard layout list.
    function GetDashboardLayouts() {
        $.ajax({
            method: "GET",
            url: "/get-dashboard-layouts",
            success: function (response) {
                $("#dashboard-layouts").html(response);
            }
        });
    }
    
    //!! Load the selected layout.
    body.on("click", ".btn-layout", function () {        
        //Grab the buttons so we can access them
        //when the ajax call completes.
        const btn = $(this);
        const id = btn.data("id");
        const span = btn.find(".fa");  
        
        //Add loading indicator.
        span.removeClass("fa-grip-vertical").addClass("fa-spinner fa-pulse");
        btn.prop("disabled", true);
        
        //Create the ajax data model.
        console.log(`layout selected: ${id}`);
        const model = { "id": id };
        //Ajax call to retrieve selected layout.
        $.ajax({
            type: "GET",
            url: "/get-dashboard-layout",
            data: model,
            success: function(response) {
                //Load the dashboard layout into the page.
                $("#body-content").html(response);
                //Set the layout id on the header.
                $("#current-layout-name").data("layoutid", id);
            },
            error: function() {
                alert("There was an error retrieving the layout.");
            },
            complete: function() {
                span.removeClass("fa-spinner fa-pulse")
                    .addClass("fa-grip-vertical");
                btn.prop("disabled", false);
            }
        });
    });
    
    body.on("click", "#btn-cameras", function () {
        $.ajax('/Camera/CameraManager',   // request url
            {
                success: function (data, status, xhr) {// success callback function
                    $("#body-content").html(data);
                    $("#current-layout-name").html("Camera Manager");
                    //window.history.pushState(null, "Camera Manager", "/Camera/CameraManager");
                }
            });
    });
});
