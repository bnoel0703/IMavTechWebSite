// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//TODO: Start AJAX - Write first version in Javascript. Write final version in JQuery

//var xhr = new XMLHttpRequest();
//document.getElementById("contactModalSendButton").onclick = function () {
//    var userName = document.getElementById("ajaxPostTestBox").value;
//    makeRequest('TextFile.txt', userName);
//};

////document.getElementById("contactModalSendButton").addEventListener('click', makeRequest);
//function makeRequest(url, userName) {
//    xhr.onreadystatechange = alertContents;
//    xhr.open('POST', url);
//    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
//    xhr.send('userName=' + encodeURIComponent(userName));
//}

//function alertContents() {
//    if (xhr.readyState === XMLHttpRequest.DONE) {
//        if (xhr.status === 200) {
//            var response = JSON.parse(xhr.responseText);
//            alert(response.computedString);
//        }
//        else {
//            alert('There was a problem with the request');
//        }
//    }
//}

$('#contactModalSendButton').on('click', (evt) => {
    evt.preventDefault();
    $.post('api/Email', $('form').serialize(), () => {
        $('#contactModalSendButton').attr("data-dismiss", "modal");
        setTimeout(() => { alert("Posted with Jquery"); }, 1000);
    });
});
//TODO: Trigger absolute dismissable Alert slide down informing user of a successful email. Will slide up and hide after 10 seconds
//TODO: Add some animations either through raw JS or JQuery