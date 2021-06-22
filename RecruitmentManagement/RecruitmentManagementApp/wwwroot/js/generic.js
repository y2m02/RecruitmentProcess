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
            event.keyCode == 46)) {

            event.preventDefault();
        }

        //if (window.$(this).val().indexOf(".") !== -1 && event.keyCode == 190)
        //    event.preventDefault();
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

function removeErrorMessage(field, label) {
    window.$("#" + field).css("borderColor", "");

    window.$("#" + label).html("");
}

function buildError(field, label) {
    var fieldId = "#" + field;

    if (window.$(fieldId).val() === "") {
       window.$(fieldId).css("border-color", "red", "important");

        window.$("#" + label).html("Campo requerido");
        return false;
    }

    window.$(fieldId).css("border-color", "");

    return true;
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

function formatDate(date) {
    var splitDate = (date.split("T")[0]).split("-");

    return  splitDate[2] + "/" + splitDate[1] + "/" + splitDate[0];
}