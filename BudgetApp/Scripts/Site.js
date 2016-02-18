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

    //action booleans
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

    $('#ckbtn-type').change(function () {
        if ($(this).is(':checked')) {
            $(this).removeClass("text-danger").addClass("text-success");
            $('#ck-text').text('Expected Income');
            $('#amt-editor').attr("placeholder", "Enter expected income amount");
        }
        if (!$(this).is(':checked')) {
            $(this).removeClass("text-success").addClass("text-danger");
            $('#ck-text').text('Goal Spending Limit');
            $('#amt-editor').attr("placeholder", "Enter goal spending limit amount");

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