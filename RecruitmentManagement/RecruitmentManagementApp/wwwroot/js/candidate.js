
var dataItem;

$(function () {
    $("#btnOpenModal, #btnEdit, #btnDelete").removeClass("k-button k-button-icontext");

    $('#btnOpenModal').attr('data-toggle', 'modal');
    $('#btnOpenModal').attr('data-target', '#myModalCandidate');

    $("#btnEdit, #btnDelete").removeAttr("href");

    $("#btnEdit, #btnDelete").prop("hidden", true);

    var grid = $("#Candidates").data("kendoGrid");

    grid.tbody.on("click", ".k-checkbox", onChange);
});

function fillFields(rowData) {
    $("#txtId").val(rowData.Id);
    $("#txtName").val(rowData.Name);
    $("#txtPhoneNumber").val(rowData.PhoneNumber == null ? null : rowData.PhoneNumber.replaceAll("-", ""));
    $("#txtEmail").val(rowData.Email);
    $("#txtCurriculum").val(rowData.Curriculum);
    $("#txtGitHub").val(rowData.GitHub);
}

$("#myModalCandidate").on("hidden.bs.modal",
    function () {
        $("#txtId").val("");
        $("#txtName").val("");
        $("#txtPhoneNumber").val("");
        $("#txtEmail").val("");
        $("#txtCurriculum").val("");
        $("#txtGitHub").val("");

        removeErrorMessage("txtName", "lblNameError");
        removeErrorMessage("txtPhoneNumber", "lblPhoneNumberError");
        removeErrorMessage("txtEmail", "lblEmailError");
    });

function isValid() {
    document.body.style.cursor = "wait";

    var isValidPhoneNumber = true;
    var isValidEmail = true;
    var nameIsValid = buildError("txtName", "lblNameError");

    if ($("#txtPhoneNumber").val() != "" && $("#txtPhoneNumber").val().length != 10) {
        isValidPhoneNumber = false;

        $("#txtPhoneNumber").css("border-color", "red", "important");

        $("#lblPhoneNumberError").html("Campo debe tener 10 dígitos");
    }

    if ($("#txtEmail").val() != "" && $("#txtEmail").val().includes("@") == false) {
        isValidEmail = false;

        $("#txtEmail").css("border-color", "red", "important");

        $("#lblEmailError").html("Campo es inválido");
    }

    document.body.style.cursor = "default";

    return nameIsValid && isValidPhoneNumber && isValidEmail;
}

function upsertCandidate() {
    document.body.style.cursor = 'wait';

    if (!isValid()) {
        document.body.style.cursor = 'default';
        return false;
    }

    var id = $("#txtId").val();
    var name = $("#txtName").val();
    var phoneNumber = $("#txtPhoneNumber").val();
    var email = $("#txtEmail").val();
    var curriculum = $("#txtCurriculum").val();
    var gitHub = $("#txtGitHub").val();

    var request = {
        "Id": id == '' ? 0 : parseInt(id),
        "Name": name,
        "PhoneNumber": phoneNumber,
        "Email": email,
        "Curriculum": curriculum,
        "GitHub": gitHub
    };

    $.ajax({
        url: "Candidate/Upsert",
        data: { request: request },
        type: "POST",
        content: "application/json;",
        dataType: "json",
        success: function (result) {
            $('#myModalCandidate').modal('toggle');

            var candidate = result.data;

            if (result.isUpdate) {
                dataItem.set("Name", candidate.Name);
                dataItem.set("PhoneNumber", candidate.PhoneNumber);
                dataItem.set("Email", candidate.Email);
                dataItem.set("Curriculum", candidate.Curriculum);
                dataItem.set("GitHub", candidate.GitHub);
            } else {
                candidate.CreatedDate = new Date(candidate.CreatedDate);

                $("#Candidates").data("kendoGrid").dataSource.add(candidate);
            }

            $("#btnEdit, #btnDelete").prop("hidden", true);

            document.body.style.cursor = 'default';
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';
            alert(errorMessage.responseText);
        }
    });

    return true;
}

function onChange(e) {
    var row = $(e.target).closest("tr");

    if (row.hasClass("k-state-selected")) {
        setTimeout(function (e) {
            $("#btnEdit, #btnDelete").prop("hidden", true);

            $("#Candidates").data("kendoGrid").clearSelection();
        });
    } else {
        $("#btnEdit, #btnDelete").prop("hidden", false);

        $("#Candidates").data("kendoGrid").clearSelection();
    };
}

function editCandidate() {
    var allSelected = $("#Candidates tr.k-state-selected");

    if (allSelected.length <= 0) {
        return;
    }

    dataItem = allSelected.closest(".k-grid").data("kendoGrid").dataItem(allSelected);

    fillFields(dataItem);

    $("#myModalCandidate").modal();
}

function deleteCandidate() {
    var allSelected = $("#Candidates tr.k-state-selected");

    if (allSelected.length <= 0) {
        return;
    }

    dataItem = allSelected.closest(".k-grid").data("kendoGrid").dataItem(allSelected);

    $("#lblCandidateId").html(dataItem.Id);
    $("#txtDeleteName").val(dataItem.Name);   
    $("#txtDeletePhoneNumber").val(dataItem.PhoneNumber == null ? null : dataItem.PhoneNumber.replaceAll("-", ""));
    $("#txtDeleteEmail").val(dataItem.Email);

    $("#myModalDelete").modal();
}

$("#btnDeleteCandidate").on("click",
    function () {
        document.body.style.cursor = 'wait';

        var id = $("#lblCandidateId").html();

        $.ajax({
            url: "Candidate/Delete",
            data: { id: id },
            type: "DELETE",
            content: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#myModalDelete').modal('toggle');

                $("#Candidates").data("kendoGrid").dataSource.remove(dataItem);

                $("#btnEdit, #btnDelete").prop("hidden", true);

                document.body.style.cursor = 'default';
            },
            error: function (errorMessage) {
                document.body.style.cursor = 'default';

                alert(errorMessage.responseText);
            }
        });
    });

$("#myModalDelete").on("hidden.bs.modal",
    function () {
        $("#lblCandidateId").html("");
    });

$("#txtName").on("input",
    function () {
        removeErrorMessage("txtName", "lblNameError");
    });

$("#txtPhoneNumber").on("input",
    function () {
        removeErrorMessage("txtPhoneNumber", "lblPhoneNumberError");
    });

$("#txtEmail").on("input",
    function () {
        removeErrorMessage("txtEmail", "lblEmailError");
    });

function createdDateFilter(element) {
    element.kendoDatePicker({
        format: "dd/MM/yyyy" // set custom defined format
    });
}