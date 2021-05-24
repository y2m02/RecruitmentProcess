$(function () {
    window.$("input").attr("autocomplete", "off");
    window.$("p").remove();

    window.$('.numericField').bind('paste', function (e) {
        e.preventDefault();
    });

    window.$("input[class*='numericField']").keydown(function (event) {

        if (event.shiftKey == true) {
            event.preventDefault();
        }

        if (!((event.keyCode >= 48 && event.keyCode <= 57) ||
            (event.keyCode >= 96 && event.keyCode <= 105) ||
            event.keyCode == 8 ||
            event.keyCode == 9 ||
            event.keyCode == 37 ||
            event.keyCode == 39 ||
            event.keyCode == 46 ||
            event.keyCode == 190)) {

            event.preventDefault();
        }

        if (window.$(this).val().indexOf(".") !== -1 && event.keyCode == 190)
            event.preventDefault();
    });

    window.$(".numericField").focus(function () {
        var $this = window.$(this);
        $this.select();
        $this.mouseup(function () {
            $this.unbind("mouseup");
            return false;
        });
    });
});

function clearErrorMessage(parameters) {
    for (var i = 0; i < parameters.length; i++) {
        if (parameters[i].key == "dtpDate") {
            window.$(".k-picker-wrap").css("borderColor", "");
        } else {
            window.$("#" + parameters[i].key).css("borderColor", "");
        }
        window.$("#" + parameters[i].value).html("");
    }
}

function removeErrorMessage(id, messageId) {
    if (id == "dtpDate") {
        window.$(".k-picker-wrap").css("borderColor", "");
    } else {
        window.$("#" + id).css("borderColor", "");
    }

    window.$("#" + messageId).html("");
}

function buildError(field, label) {
    var fieldId = "#" + field;

    if (window.$(fieldId).val() === "") {
        if (field == "dtpDate") {
            window.$(".k-picker-wrap").css("border-color", "red", "important");
        } else {
            window.$(fieldId).css("border-color", "red", "important");
        }


        window.$("#" + label).html("Campo requerido");
        return false;
    }

    window.$(fieldId).css("border-color", "");

    return true;
}


function GetDropDownListData(elementId, id, controllerName) {
    window.$.ajax({
        url: "/" + controllerName + "/GetAllForDropDownList",
        data: { id: id },
        type: "GET",
        content: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            fillDropDownList(elementId, result);

            if (id > 0) {
                window.$("#" + elementId).val(id);
            }
        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}

function fillDropDownList(elementId, result) {
    window.$("#" + elementId).children("option:not(:first)").remove();

    window.$.each(result,
        function (key, data) {
            var option = new Option();

            window.$(option).val(data.ItemId);
            window.$(option).html(data.Description);
            window.$("#" + elementId).append(option);
        });
}

function appendNewOption(elementId, id, description) {
    var option = new Option();

    window.$(option).val(id);
    window.$(option).html(description);
    window.$("#" + elementId).append(option);
}

function redirectToIndex(e, gridName) {
    if ((e.type == "create" || e.type == "update" || e.type == "destroy") && !e.response.modelState) {
        RefreshGrid(gridName);
    }
}

function RefreshGrid(gridName) {
    var grid = window.$('#' + gridName).data("kendoGrid");

    grid.dataSource.read();
    grid.refresh();
}

function deleteRecord(controllerName, griName) {
    document.body.style.cursor = 'wait';

    var id = window.$("#lblRecordId").html();

    window.$.ajax({
        url: "/" + controllerName + "/Delete",
        data: { id: id },
        type: "DELETE",
        content: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            window.$('#myModalDelete').modal('toggle');

            RefreshGrid(griName);

            document.body.style.cursor = 'default';
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';

            alert(errorMessage.responseText);
        }
    });
}

function deleteVisible(dataItem) {
    return dataItem.Used == false;
}