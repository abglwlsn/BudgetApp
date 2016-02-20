$(document).ready(function () {

    //datatables
    $('.data-table').DataTable({
        responsive: true,
        columns: [{
            responsivePriority: 1, targets: 0
        }]
    });

    //datepicker
    $('.datepicker').datepicker();

    //form post verify
    $('#leave').click(verifySubmit);
    function verifySubmit() {
        $('#verify').show("slow");
    }

    //action booleans
    $('#typeBool').change(function () {
        if ($(this).is(':checked')) {
            $('#type').text('Income');
        }
        if (!$(this).is(':checked')) {
            $('#type').text('Expense');
        }
    });

    $('#budgetBool').change(function () {
        if ($(this).is(':checked')) {
            $('#category').show("slow");
            $('.budget-item').prop('disabled', true)
        }
        if (!$(this).is(':checked')) {
            $('#category').hide("slow");
            $('.budget-item').prop('disabled', false)
        }
    });

    $('#ckbtn').click(function () {
        if ($('#ckbtn-ck').is(':checked')) {
            $('#ckbtn-ck').prop('checked', false).prop('value', false);
            $('#ckbtn').removeClass('btn-success').addClass('btn-danger');
            $('#ck-text').text('Spending Limit');
            $('#amt-editor').attr("placeholder", "Enter goal spending limit amount");
        }
        else {
            $('#ckbtn-ck').prop('checked', true).prop('value', true);
            $('#ckbtn').removeClass('btn-danger').addClass('btn-success');
            $('#ck-text').text('Expected Income');
            $('#amt-editor').attr("placeholder", "Enter expected income amount");
        }
    });

    $('#allowbtn').click(function () {
        if (!$('#allowbtn-ck').is(':checked')) {
            $('#allowbtn-ck').prop('checked', true);
            $('#allowbtn').removeClass('btn-danger').addClass('btn-success');
            $('#allow-text').text('Anyone Can Edit');
        }
        else {
            $('#allowbtn-ck').prop('checked', false);
            $('#allowbtn').removeClass('btn-success').addClass('btn-danger');
            $('#allow-text').text('Only I Can Edit');
        }
    });

    $('#typebtn').click(function () {
        if ($('#typebtn-ck').is(':checked')) {
            $('#typebtn-ck').prop('checked', false).prop('value', false);
            $('#typebtn').removeClass('btn-success').addClass('btn-danger');
            $('#type-text').text('Expense');
        }
        else {
            $('#typebtn-ck').prop('checked', true).prop('value', true);
            $('#typebtn').removeClass('btn-danger').addClass('btn-success');
            $('#type-text').text('Income');
        }
    });

    //partial views handling
    $('.editAcct').click(function () {
        $('#editView').load('/BankAccounts/_Edit/' + $(this).data('id'));
    });

    $('.deleteAcct').click(function () {
        $('#editView').load('/BankAccounts/_Delete/' + $(this).data('id'));
    });

    $('.editBudg').click(function () {
        $('#editView').load('/BudgetItems/_Edit/' + $(this).data('id'));
    });

    $('#createBudg').click(function () {
        $('editView').load('/BudgetItems/_Create');
    })

    $('.deleteBudg').click(function () {
        $('#editView').load('/BudgetItems/_Delete/' + $(this).data('id'));
    });

    $('.createTrans').click(function () {
        $('#editView').load('/Transactions/_Create');
    })

    $('.editTrans').click(function () {
        $('#editView').load('/Transactions/_Edit/' + $(this).data('id'));
    })

    $('.deleteTrans').click(function () {
        $('#editView').load('/Transactions/_Delete/' +
            $(this).data('id'));
    })

    //$('.cancel').click(function () {
    //    $('#editView').load('/BankAccounts/_Create/');
    //})

    //$('#cancelEditBudget').click(function () {
    //    $('#editView').load('/BudgetItems/_Create');
    //})

    //$('body').on("click", "#cancelEditBudget", function () {
    //    $('#editView').load('/BudgetItems/_Create');
    //})


    //colors
    $('.balance').each(function (i) {
        var content = parseInt($(this).text().replace('$', ''), 10);
        var balance = parseInt(content, 10);
        if (balance <= 0)
        {
            $(this).removeClass("text-succ").addClass("text-dang");
        }
        else {
            $(this).removeClass("text-dang").addClass("text-succ");
        }
    });

});