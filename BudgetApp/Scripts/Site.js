$(document).ready(function () {

    //datatables
    $('.data-table').DataTable()
    //   responsive: true
    ;

    //datepicker
    $('.datepicker').datepicker();


    //trigger first-visit modal with role information
    //window.onunload = function () {
    //    alert('Bye.');
    //}



    //disable/enable budget and category dropdowns on transaction view
    $('body').on('change', '#budgetBool', function () {
        if ($(this).is(':checked')) {
            $('#category').show('slow');
            $('.budget-item').prop('disabled', true).hide('slow');
        }
        if (!$(this).is(':checked')) {
            $('#category').hide("slow");
            $('.budget-item').prop('disabled', false).show('slow');
        }
    });

    //colors
    $('.balance').each(function (i) {
        var content = parseInt($(this).text().replace('$', ''), 10);
        var balance = parseInt(content, 10);
        if (balance <= 0) {
            $(this).removeClass("text-succ").addClass("text-dang");
        }
        else {
            $(this).removeClass("text-dang").addClass("text-succ");
        }
    });


    //section scroll
    $('.section-scroll').on('click', function (e) {
        var target = this.hash;
        var $target = $(target);

        $('html, body').stop().animate({
            'scrollTop': $target.offset().top
        }, 900, 'swing');

        e.preventDefault();
    });

    //TogglePrettyCheckbox('#ckbtn-ck', '#ckbtn', '#ck-text', 'Spending Limit', 'Expected Income', true, '#amt-editor', "Enter goal spending limit amount", "Enter expected income amount")
    //TogglePrettyCheckbox('#allowbtn-ck', '#allowbtn', '#allow-text', 'Only I can Edit', 'Anyone Can Edit', false, null, null, null)
    //TogglePrettyCheckbox('#incomebtn-ck', '#incomebtn', '#type-text', 'Expense', 'Income', false, null, null, null)

    //function TogglePrettyCheckbox(checkbox, button, textElement, newText1, newText0, changeEditor, editor, editorText1, editorText0) {

    //    $('#editView').on('click', button, function () {
    //        if ($(checkbox).is(':checked')) {
    //            $(button).prop('checked', false).prop('value', false);
    //            $(button).removeClass('.btn-teal').addClass('.btn-danger');
    //            $(textElement).text(newText1);
    //            changeEditor ? $(editor).attr("placeholder", editorText1) : "";
    //        }
    //        else {
    //            $(checkbox).prop('checked', true).prop('value', true);
    //            $(button).removeClass('.btn-danger').addClass('.btn-teal');
    //            $(textElement).text(newText0);
    //            changeEditor ? $(editor).attr("placeholder", editorText0) : "";
    //        }
    //    })
    //};

    //$('#editView').on('click', '#ckbtn', function () {
    //    if ($('#ckbtn-ck').is(':checked')) {
    //        $('#ckbtn-ck').prop('checked', false).prop('value', false);
    //        $('#ckbtn').removeClass('btn-teal').addClass('btn-danger');
    //        $('#ck-text').text('Spending Limit');
    //        $('#amt-editor').attr("placeholder", "Enter goal spending limit amount");
    //    }
    //    else {
    //        $('#ckbtn-ck').prop('checked', true).prop('value', true);
    //        $('#ckbtn').removeClass('btn-danger').addClass('btn-teal');
    //        $('#ck-text').text('Expected Income');
    //        $('#amt-editor').attr("placeholder", "Enter expected income amount");
    //    }
    //});

    //$('#editView').on('click', '#allowbtn', function () {
    //        if (!$('#allowbtn-ck').is(':checked')) {
    //            $('#allowbtn-ck').prop('checked', true);
    //            $('#allowbtn').removeClass('btn-danger').addClass('btn-teal');
    //            $('#allow-text').text('Anyone Can Edit');
    //        }
    //        else {
    //            $('#allowbtn-ck').prop('checked', false);
    //            $('#allowbtn').removeClass('btn-teal').addClass('btn-danger');
    //            $('#allow-text').text('Only I Can Edit');
    //        }
    //    });

   // $('#editView').on('click', '#incomebtn', function () {
   //     if ($('#incomebtn-ck').is(':checked')) {
   //         $('#incomebtn-ck').prop('checked', false).prop('value', false);
            //$('#incomebtn').removeClass('btn-teal').addClass('btn-danger');
            //$('#income-text').text('Expense');
       //}
       //else {
       //    $('#incomebtn-ck').prop('checked', true).prop('value', true);
            //$('#incomebtn').removeClass('btn-danger').addClass('btn-teal');
            //$('#income-text').text('Income');
    //    }
    //});

    //partial views handling
    function AssignPartialViewHandler(divContain, divRender, target, controllerName, actionName, hasDataTag) {
        var loadUrl = "/" + controllerName + "/" + actionName;

        $(divContain).on('click', target, function () {
            $(divRender).load(loadUrl + (hasDataTag ? ('/' + $(this).data('id')) : ""));
        })
    }

    AssignPartialViewHandler('#editView', '#editView', '.cancel-acct', 'BankAccounts', '_Create', false);
    AssignPartialViewHandler('#tableAcct', '#editView', '.editAcct', 'BankAccounts', '_Edit', true);
    AssignPartialViewHandler('#tableAcct', '#editView', '.deleteAcct', 'BankAccounts', '_Delete', true);
    AssignPartialViewHandler('#editView', '#editView', '.cancel-budg', 'BudgetItems', '_Create', false);
    AssignPartialViewHandler('#tableBudg', '#editView', '.editBudg', 'BudgetItems', '_Edit', true);
    AssignPartialViewHandler('#tableBudg', '#editView', '.deleteBudg', 'BudgetItems', '_Delete', true)
    AssignPartialViewHandler('#tableBudg', '#viewTrans', '.viewBudgTrans', 'BudgetItems', '_Transactions', true);
    AssignPartialViewHandler('#accountsRender', '#editView', '.editTrans', 'Transactions', '_Edit', true);
    AssignPartialViewHandler('#accountsRender', '#editView', '.deleteTrans', 'Transactions', '_Delete', true);
    AssignPartialViewHandler('#catsRender', '#editView', '.editCat', 'Categories', '_Edit', true);
    AssignPartialViewHandler('#catsRender', '#editView', '.deleteCat', 'Categories', '_Delete', true);
    AssignPartialViewHandler('#invitedUsers', "#inviteUserPartial", '.invite', 'InvitedUsers', '_Create', false);

    //.cancel-cat - remove partial
    //.cancel-invite - remove partial

    //manually change checkbox values (in partial views)
    function ManualCheckbox(target) {
        $('body').on('click', target, function () {
            if ($(this).is(':checked')) {
                $(this).val(true);

            }
            if (!$(this).is(':checked')) {
                $(this).val(false);
            }
        });
    }

    ManualCheckbox("#incomeCk");
    ManualCheckbox("#adminCk");

    $('.delete').click(function () {
        $('#rescindId').val($(this).data('id'));
    });

    $('.expel').click(function () {
        $('#expelId').val($(this).data('id'));
    })

    $('.leave').click(function () {
        $('#leaveId').val($(this).data('id'));
    })
});