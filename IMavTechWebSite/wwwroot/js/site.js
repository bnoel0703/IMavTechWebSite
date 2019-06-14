// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#contactModalSendButton").on("click", (evt) => {
    evt.preventDefault();
    $.post("", $("form").serialize(), () => {
        $("#contactModal").modal("hide");
        $("#contactModal").on("hidden.bs.modal", (evt) => {
            $("#contactButton").attr("title", "This is to prevent spam");
            $("#contactButton").prop("disabled", true);
            $("#emailAlert").removeClass("hide");
            $("#emailAlert").addClass("show");
        });
    });
});
