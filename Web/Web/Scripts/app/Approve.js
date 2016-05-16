﻿
function ShowEditModal(){
document.addEventListener('DOMContentLoaded', function () {
    var link = document.getElementsByClassName('editButton');

    $(".editButton").each(function () {

        this.addEventListener('click', function () {
            var html = $("#" + this.getAttribute('modal-id')).html();

            $("#modal-projectlist-content").html(html);
            jQuery.validator.unobtrusive.parse($("#modal-projectlist-content"));
            $("#infoProjectListModal").modal("show")
        });
    })
    // onClick's logic below:

});
}

function ApproveBiding() {
    document.addEventListener('DOMContentLoaded', function () {

        $(".approve-button").each(function () {

            this.addEventListener('click', function () {
                var html = $("#" + this.getAttribute('approve-form-id')).parent().html();

                $("#modal-projectlist-content").html(html);
                jQuery.validator.unobtrusive.parse($("#modal-projectlist-content"));
                $("#infoProjectListModal").modal("show")
            });
        })
        // onClick's logic below:

    });
}