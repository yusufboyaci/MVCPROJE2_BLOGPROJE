$(document).ready(function () {
    $("#resim").hide();
    $("#upload-photo-label").click(function (event) {
        event.preventDefault();
        $("#resim").fadeIn(1000);
    });   
});