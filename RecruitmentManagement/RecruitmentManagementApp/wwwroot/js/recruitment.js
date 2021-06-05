$(function () {
    $("#btnEdit").removeClass("k-button k-button-icontext");

    $("#btnEdit").removeAttr("href");

    $("#btnEdit").prop("hidden", true);

    var grid = $("#Recruitments").data("kendoGrid");

    grid.tbody.on("click", ".k-checkbox", onChange);
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
            
            $("#btnEdit").prop("hidden", true);

            document.body.style.cursor = 'default';
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';
            alert(errorMessage.responseText);
        }
    });

    return true;
}

function editRecruitment() {
    var allSelected = $("#Recruitments tr.k-state-selected");

    if (allSelected.length <= 0) {
        return;
    }

    var dataItem = allSelected.closest(".k-grid").data("kendoGrid").dataItem(allSelected);

    fillFields(dataItem);

    window.$("#myModalRecruitment").modal();
}

function onChange(e) {
    var row = $(e.target).closest("tr");

    if (row.hasClass("k-state-selected")) {
        setTimeout(function (e) {
            $("#btnEdit").prop("hidden", true);

            $("#Recruitments").data("kendoGrid").clearSelection();
        });
    } else {
        $("#btnEdit").prop("hidden", false);

        $("#Recruitments").data("kendoGrid").clearSelection();
    };
}

window.$("#cbxStatus").on("change",
    function() {
        removeErrorMessage("cbxStatus","lblStatusError");
    });


//$("#Recruitments").delegate(".editButton",
//    "click",
//    function(e) {
//        e.preventDefault();

//        var grid = window.$("#Recruitments").data("kendoGrid");
//        var rowData = grid.dataItem(window.$(this).closest("tr"));

//        fillFields(rowData);

//        window.$("#myModalRecruitment").modal();
//    });