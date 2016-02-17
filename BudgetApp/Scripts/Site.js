$(document).ready(function () {

    //datatables
    $('.data-table').DataTable();

    //datepicker
    $('.datepicker').datepicker();

    //form post verify
    $('#leave').click(verifySubmit);
    function verifySubmit() {
        $('#verify').show("slow");
    }

    //transaction booleans
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
            $('.budget-item').prop('disabled', true)
        }
        if (!$(this).is(':checked')) {
            $('#category').hide("slow");
            $('.budget-item').prop('disabled', false)
        }
    })

    //partial views handling
    $('.editAcct').click(function () {
        $('#editView').load('/BankAccounts/_Edit/' + $(this).data('id'));
    });

    $('.deleteAcct').click(function () {
        $('#editView').load('/BankAccounts/_Delete/' + $(this).data('id'));
    })

    $('.editBudg').click(function () {
        $('#editView').load('/BudgetItems/_Edit/' + $(this).data('id'));
    });

    $('.deleteBudg').click(function () {
        $('#editView').load('/BudgetItems/_Delete/' + $(this).data('id'));
    })

    $('.viewAcctTrans').click(function () {
        $('.transactions').load('/Transactions/_View/' + $(this).data('id'));
    })

    $('.viewTrans').click(function () {
        $('.transactions').load('/Transactions/_ViewPartial');
    })

    //colors
    if ($('.balance') < 0) {
        $('.balance').removeclass("text-success").addclass("text-danger");
    }
    else {
        $('.baance').removeclass("text-danger").addclass("text-success");
    }

});