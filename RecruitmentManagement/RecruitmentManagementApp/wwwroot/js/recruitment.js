$("#Recruitments").delegate(".editButton",
    "click",
    function(e) {
        e.preventDefault();

        var grid = window.$("#Recruitments").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        fillFields(rowData);

        window.$("#myModalRecruitment").modal();
    });

function fillFields(rowData) {
    window.$("#txtId").val(rowData.Id);
    window.$("#cbxStatus").val(rowData.Status);
    window.$("#txtNote").val(rowData.Note);
}

$("#myModalRecruitment").on("hidden.bs.modal",
    function() {
        window.$("#txtId").val("");
        window.$("#cbxStatus").val("");
        window.$("#txtNote").val("");

        removeErrorMessage("cbxStatus","lblStatusError");
    });

function isValid() {
    document.body.style.cursor = "wait";

    var name = buildError("cbxStatus", "lblStatusError");

    document.body.style.cursor = "default";

    return name;
}

function updateRecruitment() {
    document.body.style.cursor = 'wait';

    if (!isValid()) {
        document.body.style.cursor = 'default';
        return false;
    }

    var id = window.$("#txtId").val();
    var status = parseInt(window.$("#cbxStatus").val());
    var note = window.$("#txtNote").val();

    var request = {
        "Id": id == '' ? 0 : parseInt(id),
        "Status": status,
        "Note": note,
    };

    window.$.ajax({
        url: "Recruitment/Update",
        data: { request: request },
        type: "PUT",
        content: "application/json;",
        dataType: "json",
        success: function (result) {
            window.$('#myModalRecruitment').modal('toggle');
            RefreshGrid('Recruitments');
            document.body.style.cursor = 'default';
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';
            alert(errorMessage.responseText);
        }
    });

    return true;
}

window.$("#cbxStatus").on("change",
    function() {
        removeErrorMessage("cbxStatus","lblStatusError");
    });
