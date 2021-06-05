
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
    window.$("#txtId").val(rowData.Id);
    window.$("#txtName").val(rowData.Name);
    window.$("#txtCurriculum").val(rowData.Curriculum);
    window.$("#txtGitHub").val(rowData.GitHub);
}

$("#myModalCandidate").on("hidden.bs.modal",
    function () {
        window.$("#txtId").val("");
        window.$("#txtName").val("");
        window.$("#txtCurriculum").val("");
        window.$("#txtGitHub").val("");

        removeErrorMessage("txtName", "lblNameError");
    });

function isValid() {
    document.body.style.cursor = "wait";

    var name = buildError("txtName", "lblNameError");

    document.body.style.cursor = "default";

    return name;
}

function upsertCandidate() {
    document.body.style.cursor = 'wait';

    if (!isValid()) {
        document.body.style.cursor = 'default';
        return false;
    }

    var id = window.$("#txtId").val();
    var name = window.$("#txtName").val();
    var curriculum = window.$("#txtCurriculum").val();
    var gitHub = window.$("#txtGitHub").val();

    var request = {
        "Id": id == '' ? 0 : parseInt(id),
        "Name": name,
        "Curriculum": curriculum,
        "GitHub": gitHub
    };

    window.$.ajax({
        url: "Candidate/Upsert",
        data: { request: request },
        type: "POST",
        content: "application/json;",
        dataType: "json",
        success: function (result) {
            window.$('#myModalCandidate').modal('toggle');

            RefreshGrid('Candidates');
            
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

    var dataItem = allSelected.closest(".k-grid").data("kendoGrid").dataItem(allSelected);

    fillFields(dataItem);

    window.$("#myModalCandidate").modal();
}

function deleteCandidate() {
    var allSelected = $("#Candidates tr.k-state-selected");

    if (allSelected.length <= 0) {
        return;
    }

    var dataItem = allSelected.closest(".k-grid").data("kendoGrid").dataItem(allSelected);

    window.$("#lblCandidateId").html(dataItem.Id);
    window.$("#txtDeleteName").val(dataItem.Name);
    window.$("#txtDeleteCurriculum").val(dataItem.Curriculum);
    window.$("#txtDeleteGitHub").val(dataItem.GitHub);

    window.$("#myModalDelete").modal();
}

$("#btnDeleteCandidate").on("click",
    function () {
        document.body.style.cursor = 'wait';

        var id = window.$("#lblCandidateId").html();

        window.$.ajax({
            url: "Candidate/Delete",
            data: { id: id },
            type: "DELETE",
            content: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                window.$('#myModalDelete').modal('toggle');

                RefreshGrid('Candidates');

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
        window.$("#lblCandidateId").html("");
    });

$("#txtName").on("input",
    function () {
        removeErrorMessage("txtName", "lblNameError");
    });