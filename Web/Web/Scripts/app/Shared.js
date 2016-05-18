function reloadPage() {
    location.reload();
}

function onBeforeSubmit(parentElement) {
    var parent = parentElement
    var suitableLevelAnyChecked = false;
    $(parent).find("[name='SuitableLevels']").each(function (index) {
        if (this.checked) {
            suitableLevelAnyChecked = true;
        }
    });

    var subjectChecked = false;
    $(parent).find("input[name=SuitableSubjects]").each(function (index) {
        if (this.checked) {
            subjectChecked = true;
        }
    });

    var causeChecked = false;
    $(parent).find("[name='Causes']").each(function (index) {
        if (this.checked) {
            causeChecked = true;
        }
    });

    if (!suitableLevelAnyChecked) {
        $(parent).find("[data-valmsg-for=SuitableLevels]").toggleClass('text-danger field-validation-error').html("Level is required");
    }
    else
        $(parent).find("[data-valmsg-for=SuitableLevels]").toggleClass('text-danger field-validation-error').html("");

    if (!subjectChecked) {
        $(parent).find("[data-valmsg-for=SuitableSubjects]").toggleClass('text-danger field-validation-error').html("Subject is required");
    }
    else
        $(parent).find("[data-valmsg-for=SuitableSubjects]").toggleClass('text-danger field-validation-error').html("");


    if (!causeChecked) {
        $("[data-valmsg-for=Causes]").toggleClass('text-danger field-validation-error').html("Cause is required");
    }
    else
        $(parent).find("[data-valmsg-for=Causes]").toggleClass('text-danger field-validation-error').html("");

    if (!suitableLevelAnyChecked || !causeChecked || !subjectChecked) {
        return false;
    }
    else
        return true;
}


$('.modal').each(function () {
    $(this).on('show.bs.modal', function () {
        $(this).css({
            width: 'auto', 
            height: 'auto', 
            'max-height': '100%'
        });
    });
});
