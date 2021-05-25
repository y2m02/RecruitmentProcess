$("#Candidates").delegate(".editButton",
    "click",
    function(e) {
        e.preventDefault();

        var grid = window.$("#Candidates").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        fillFields(rowData);

        window.$("#myModalCandidate").modal();
    });

function fillFields(rowData) {
    window.$("#txtId").val(rowData.Id);
    window.$("#txtName").val(rowData.Name);
    window.$("#txtCurriculum").val(rowData.Curriculum);
    window.$("#txtGitHub").val(rowData.GitHub);
}

$("#myModalCandidate").on("hidden.bs.modal",
    function() {
        window.$("#txtId").val("");
        window.$("#txtName").val("");
        window.$("#txtCurriculum").val("");
        window.$("#txtGitHub").val("");

        clearErrorMessage([
            {
                'field': "txtName",
                'label': "lblNameError"
            },
        ]);
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
            document.body.style.cursor = 'default';
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';
            alert(errorMessage.responseText);
        }
    });

    return true;
}

window.$("#txtName").on("input",
    function() {
        removeErrorMessage("txtName", "lblNameError");
    });
