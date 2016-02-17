$(document).ready(function () {

    //form post verify
    $('#leave').click(verifySubmit);
    function verifySubmit() {
        $('#verify').show("slow");
    }

    //transaction creation and edit
    $('#typeBool').change(function () {
        if ($(this).is(':checked')) {
            $('#type').text('Income');
        }
        if (!$(this).is(':checked')) {
            $('#type').text('Expense');
        }
    })

    $('#budgetBool').change(function () {
        if ($(this).is(':checked')) {
            $('#category').show("slow");
            $('.budget-item').disabled = true;
        }
        if (!$(this).is(':checked')) {
            $('#category').hide("slow");
            $('.budget-item').disabled = false;
        }
    })

    //datepicker
    $('.datepicker').datepicker();
});