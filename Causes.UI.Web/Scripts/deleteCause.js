$(document).ready(function () {
    
});

var ConfirmDelete = function (CauseId) {
    $("#hiddenCauseId").val(CauseId);
    $("#myModal").modal('show');
}

var Delete = function () {
    var causeId = $("#hiddenCauseId").val();
    //console.log(causeId);

    $.ajax({
        type: "POST",
        url: "/Admin/Delete",
        data: { id: causeId },
        success: function (del) {
            $("#myModal").modal('hide');
            location.reload();
        }
    });
}