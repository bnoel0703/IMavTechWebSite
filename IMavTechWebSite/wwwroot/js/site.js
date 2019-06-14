// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#contactModalSendButton").on("click", (evt) => {
    evt.preventDefault();
    $.post("", $("form").serialize(), () => {
        hideContactmodal();
        $("#contactModal").on("hidden.bs.modal", (evt) => {
            disableContactModalButton();
            displayAlert();
        });
    });
});

function hideContactmodal() {
    $("#contactModal").modal("hide");
}

function disableContactModalButton() {
    $("#contactButton").attr("title", "This is to prevent spam");
    $("#contactButton").prop("disabled", true);
}

function displayAlert() {
    $("#emailAlert").removeClass("hide");
    $("#emailAlert").addClass("show");
}
