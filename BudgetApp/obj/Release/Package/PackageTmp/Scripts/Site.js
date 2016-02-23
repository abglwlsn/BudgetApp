$(document).ready(function () {

    //partial views handling
    function AssignPartialViewHandler(divContain, target, controllerName, actionName, hasDataTag) {
        var loadUrl = "/" + controllerName + "/" + actionName;

        $(divContain).on('click', target, function () {
            $('#editView').load(loadUrl + (hasDataTag ? ('/' + $(this).data('id')) : ""));
        })
    }

    AssignPartialViewHandler('#editView', '.cancel-acct', 'BankAccounts', '_Create', false);
    AssignPartialViewHandler('#tableAcct', '#editAcct', 'BankAccounts', '_Edit', true);
    AssignPartialViewHandler('#tableAcct', '#deleteAcct', 'BankAccounts', '_Delete', true);
    AssignPartialViewHandler('#editView', '.cancel-budg', 'BudgetItems', '_Create', false);
    AssignPartialViewHandler('#tableBudg', '#editBudg', 'BudgetItems', '_Edit', true);
    AssignPartialViewHandler('#tableBudg', '#deleteBudg', 'BudgetItems', '_Delete', true)
    AssignPartialViewHandler('#viewTrans', '#viewBudgTrans', 'BudgetItems', '_Transactions', true);
    AssignPartialViewHandler('#editView', '.cancel-trans', 'Transactions', '_Create', false);
    AssignPartialViewHandler('#tableTrans', '#editTrans', 'Transactions', '_Edit', true);
    AssignPartialViewHandler('#tableTrans', '#deleteTrans', 'Transactions', '_Delete', true);
    AssignPartialViewHandler('#editView', '#editCat', 'Categories', '_Edit', true);
    AssignPartialViewHandler('#editView', '#deleteCat', 'Categories', '_Delete', true);

    //$('#editView').on('click', '#cancelTrans', function () {
    //    $('#editView').load('/Transactions/_Create');
    //})

    //$('#editTrans').click(function () {
    //    $('#editView').load('/Transactions/_Edit/' + $(this).data('id'));
    //})

    //datatables
    $('.data-table').DataTable(
    //   responsive: true
    );

    //datepicker
    $('.datepicker').datepicker();

    //form post verify
    $('#leave').click(verifySubmit);
    function verifySubmit() {
        $('#verify').show("slow");
    }

    //action booleans
    //$('#incomeBool').change(function () {
    //    if ($(this).is(':checked')) {
    //        $('#type').text('Income');
    //    }
    //    if (!$(this).is(':checked')) {
    //        $('#type').text('Expense');
    //    }
    //});

    $('#editView').on('change', '#budgetBool', function () {
        if ($(this).is(':checked')) {
            $('#category').show('slow');
            $('.budget-item').prop('disabled', true).hide('slow');
        }
        if (!$(this).is(':checked')) {
            $('#category').hide("slow");
            $('.budget-item').prop('disabled', false).show('slow');
        }
    });

    //TogglePrettyCheckbox('#ckbtn-ck', '#ckbtn', '#ck-text', 'Spending Limit', 'Expected Income', true, '#amt-editor', "Enter goal spending limit amount", "Enter expected income amount")
    //TogglePrettyCheckbox('#allowbtn-ck', '#allowbtn', '#allow-text', 'Only I can Edit', 'Anyone Can Edit', false, null, null, null)
    //TogglePrettyCheckbox('#typebtn-ck', '#typebtn', '#type-text', 'Expense', 'Income', false, null, null, null)

    //function TogglePrettyCheckbox(checkbox, button, textElement, newText1, newText0, changeEditor, editor, editorText1, editorText0) {

    //    $('#editView').on('click', button, function () {
    //        if ($(checkbox).is(':checked')) {
    //            $(button).prop('checked', false).prop('value', false);
    //            $(button).removeClass('.btn-success').addClass('.btn-danger');
    //            $(textElement).text(newText1);
    //            changeEditor ? $(editor).attr("placeholder", editorText1) : "";
    //        }
    //        else {
    //            $(checkbox).prop('checked', true).prop('value', true);
    //            $(button).removeClass('.btn-danger').addClass('.btn-success');
    //            $(textElement).text(newText0);
    //            changeEditor ? $(editor).attr("placeholder", editorText0) : "";
    //        }
    //    })
    //};

    //$('#editView').on('click', '#ckbtn', function () {
    //    if ($('#ckbtn-ck').is(':checked')) {
    //        $('#ckbtn-ck').prop('checked', false).prop('value', false);
    //        $('#ckbtn').removeClass('btn-success').addClass('btn-danger');
    //        $('#ck-text').text('Spending Limit');
    //        $('#amt-editor').attr("placeholder", "Enter goal spending limit amount");
    //    }
    //    else {
    //        $('#ckbtn-ck').prop('checked', true).prop('value', true);
    //        $('#ckbtn').removeClass('btn-danger').addClass('btn-success');
    //        $('#ck-text').text('Expected Income');
    //        $('#amt-editor').attr("placeholder", "Enter expected income amount");
    //    }
    //});

    //$('#editView').on('click', '#allowbtn', function () {
    //        if (!$('#allowbtn-ck').is(':checked')) {
    //            $('#allowbtn-ck').prop('checked', true);
    //            $('#allowbtn').removeClass('btn-danger').addClass('btn-success');
    //            $('#allow-text').text('Anyone Can Edit');
    //        }
    //        else {
    //            $('#allowbtn-ck').prop('checked', false);
    //            $('#allowbtn').removeClass('btn-success').addClass('btn-danger');
    //            $('#allow-text').text('Only I Can Edit');
    //        }
    //    });

    //$('#editView').on('click', '#typebtn', function () {
    //    if ($('#typebtn-ck').is(':checked')) {
    //        $('#typebtn-ck').prop('checked', false).prop('value', false);
    //        $('#typebtn').removeClass('btn-success').addClass('btn-danger');
    //        $('#type-text').text('Expense');
    //    }
    //    else {
    //        $('#typebtn-ck').prop('checked', true).prop('value', true);
    //        $('#typebtn').removeClass('btn-danger').addClass('btn-success');
    //        $('#type-text').text('Income');
    //    }
    //});


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