$(document).ready(function () {

});

var ConfirmSign = function (CauseId) {
    $("#hiddenCauseId").val(CauseId);
    $("#myModal").modal('show');
}

var Sign = function () {
    var causeId = $("#hiddenCauseId").val();
    //console.log(causeId);

    $.ajax({
        type: "POST",
        url: "/Cause/Sign",
        data: { id: causeId },
        success: function (sign) {
            $("#myModal").modal('hide');
            location.reload();
        }
    });
}