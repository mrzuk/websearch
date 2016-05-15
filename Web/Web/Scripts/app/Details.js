function OnSumbitInterest(result) {
    var text=""
    if (result.userNotLogged) {
        $("#SuccessTitle").html();
        text = "You should be logged to express interest"
    }
    if (result.success) {
        text = "You have submitted your interest";
        $("#express-interes-form").remove();
    }

    $("#messageModalInfo").html(text);

    $("#infoModal").modal("show");

    

}