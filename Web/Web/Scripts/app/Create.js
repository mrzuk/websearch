function onBeforeSubmit() {
    var suitableLevelAnyChecked = false;
    $("[name='SuitableLevels']").each(function (index) {
        if (this.checked) {
            suitableLevelAnyChecked = true;
        }
    });

    var subjectChecked = false;
    $("input[name=SuitableSubjects]").each(function (index) {
        if (this.checked) {
            subjectChecked = true;
        }
    });

    var causeChecked = false;
    $("[name='Causes']").each(function (index) {
        if (this.checked) {
            causeChecked = true;
        }
    });

    if (!suitableLevelAnyChecked) {
        $("[data-valmsg-for=SuitableLevels]").toggleClass('text-danger field-validation-error').html("Level is required");
    }
    else
        $("[data-valmsg-for=SuitableLevels]").toggleClass('text-danger field-validation-error').html("");

    if (!subjectChecked) {
        $("[data-valmsg-for=SuitableSubjects]").toggleClass('text-danger field-validation-error').html("Subject is required");
    }
    else
        $("[data-valmsg-for=SuitableSubjects]").toggleClass('text-danger field-validation-error').html("");


    if (!causeChecked) {
        $("[data-valmsg-for=Causes]").toggleClass('text-danger field-validation-error').html("Cause is required");
    }
    else
        $("[data-valmsg-for=Causes]").toggleClass('text-danger field-validation-error').html("");

    if (!suitableLevelAnyChecked || !causeChecked || !subjectChecked) {
        return false;
    }
    else
        return true;
}