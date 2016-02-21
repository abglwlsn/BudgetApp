$(document).ready(function () {

    //partial views handling
    //$('#tableAcct').on('click', '#deleteAcct', function () {
    //    $('#editView').load('/BankAccounts/_Delete/' + $(this).data('id'));
    //})

    //function AssignPartialViewHandler(divContain, target, controllerName, actionName, hasDataTag)
    //{
    //    var fullLoadName = "/" + controllerName + "/" + actionName;
    //
    //    if (hasDataTag) {
    //
    //        fullLoadName += "/" + $(this).data('id');
    //    }
    //
    //    $(divContain).on('click', target, function (){
    //        $('#editView').load(fullLoadName);
    //    })
    //}

    function AssignPartialViewHandler(divContain, target, controllerName, actionName, hasDataTag) {
        var fullLoadName = "/" + controllerName + "/" + actionName;

        $(divContain).on('click', target, function () {
            $('#editView').load(fullLoadName + (hasDataTag ? ('/' + $(this).data('id')) : ""));
        })
    }

    AssignPartialViewHandler('#editView', '.cancel', 'BankAccounts', '_Create', false);
    AssignPartialViewHandler('#tableAcct', '#editAcct', 'BankAccounts', '_Edit', true);
    GhettoAssignPartialViewHandler('#tableAcct', '#deleteAcct', 'BankAccounts', '_Delete', true);




    //AssignNewEventHandler('createAcct', '_Create', false);
    //AssignNewEventHandler('cancelAcct', '_Create', false);
    //AssignNewEventHandler('deleteAcct', '_Delete', true);
    //AssignNewEventHandler('editAcct', '_Edit', true);

    $('#editView').on('click', '#editBudg', function () {
        $('#editView').load('/BudgetItems/_Edit/' + $(this).data('id'));
    })

    $('#editView').on('click', '#deleteBudg', function () {
        $('#editView').load('/BudgetItems/_Delete/' + $(this).data('id'));
    })

    $('#editView').on('click', '#createBudg', function () {
        $('editView').load('/BudgetItems/_Create');
    })

    $('#editView').on('click', '#cancelBudg', function () {
        $('#editView').load('/BudgetItems/_Create');
    })

    $('#editView').on('click', '#editTrans', function () {
        $('#editView').load('/Transactions/_Edit/' + $(this).data('id'));
    })

    $('#editView').on('click', '#deleteTrans', function () {
        $('#editView').load('/Transactions/_Delete/' + $(this).data('id'));
    })

    $('#editView').on('click', '#createTrans', function () {
        $('#editView').load('/Transactions/_Create');
    })

    $('#editView').on('click', '#cancelTrans', function () {
        $('#editView').load('/Transactions/_Create');
    })

    //$('#editTrans').click(function () {
    //    $('#editView').load('/Transactions/_Edit/' + $(this).data('id'));
    //})

    //datatables
    $('.data-table').DataTable({
        responsive: true
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