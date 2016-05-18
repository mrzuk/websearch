function OnSumbitInterest(result) {
    var text = "";
   
    if (result.success) {
        text = "You have submitted your interest";
        $("#express-interes-form").remove();
        $("#messageModalInfo").html(text);
        $("#infoModal").modal("show");
    }
   
}

function ShowExpressInterestModal() {
    document.addEventListener('DOMContentLoaded', function () {

        $("#express_interest-button").each(function () {

            this.addEventListener('click', function () {
                var html = $("#express-interest-form").html();

                $("#modal-projectlist-content").html(html);
                jQuery.validator.unobtrusive.parse($("#modal-projectlist-content"));
                $("#infoProjectListModal").modal("show")
            });
        })
        // onClick's logic below:

    });
}