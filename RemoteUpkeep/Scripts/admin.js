function InitActionsModal() {

    $('.create-action').unbind();
    $('.create-action').click(function () {
        var url = "/Admin/Actions/CreatePartial";
        var detailsId = $(this).attr('data-id');
        $.get(url + '/' + detailsId, function (data) {
            $('#modal-content').html(data);

            $('#modal').modal('show');
            jQuery.validator.unobtrusive.parse('#frmModal');

            AddAsterisk();
            InitDatePicker();
        });
    });

    $('.edit-action').unbind();
    $('.edit-action').click(function () {
        var url = "/Admin/Actions/UpdatePartial";
        var id = $(this).attr('data-id');
        $.get(url + '/' + id, function (data) {
            $('#modal-content').html(data);

            $('#modal').modal('show');
            jQuery.validator.unobtrusive.parse('#frmModal');

            AddAsterisk();
            InitDatePicker();
        });
    });

    $('.delete-action').unbind();
    $('.delete-action').click(function () {
        if (confirm(resources.DeleteConfirm)) {
            var url = "/Admin/Actions/DeletePartial";
            var id = $(this).attr('data-id');
            $.get(url + '/' + id, function (data) {
                RefreshActions(data.detailsId);
            });
        }
    });
}

function RefreshActions(detailsId) {
    var url = "/Admin/Actions/GetListByDetails/" + detailsId;
    $.ajax({
        type: 'GET',
        dataType: 'Json',
        url: url,
        success: function (data) {

            $('table#actions_' + detailsId + ' TBODY').empty();

            $.each(data, function (idx, item) {
                var row = "<tr>";
                row += '<td>' + item.Description + '</td>';
                row += '<td>' + (new Date(parseInt(item.DueDate.substr(6)))).toLocaleString() + '</td>';
                row += '<td>' + (new Date(parseInt(item.ChangedDateTime.substr(6)))).toLocaleString() + '</td>';
                row += '<td>' + (item.AssignedUser ? item.AssignedUser.FullName : '') + '</td>';
                row += "<td align='right'><button type='button' class='btn btn-default edit-action' data-id='" + item.Id + "'>" + resources.Edit + "</button> <button type='button' class='btn btn-default delete-action' data-id='" + item.Id + "'>" + resources.Delete + "</button></td>";
                row += "</tr>";

                $('table#actions_' + detailsId + ' TBODY').append(row);
            });

            InitActionsModal();
        },
        processData: false,
        async: true
    });
}

function InitMessagesModal() {
    $('.create-message').unbind();
    $('.create-message').click(function () {
        var url = "/Admin/Messages/CreatePartial";
        var detailsId = $(this).attr('data-id');
        $.get(url + '/' + detailsId, function (data) {
            $('#modal-content').html(data);

            $('#modal').modal('show');
            jQuery.validator.unobtrusive.parse('#frmModal');

            AddAsterisk();
            InitDatePicker();
            InitReceiverLanguage();
        });
    });

    $('.view-message').unbind();
    $('.view-message').click(function () {
        var url = "/Admin/Messages/ViewPartial";
        var id = $(this).attr('data-id');
        $.get(url + '/' + id, function (data) {
            $('#modal-content').html(data);
            $('#modal').modal('show');
        });
    });
}

function InitReceiverLanguage()
{
    $('.receiver .user').change(function () {
        var selected = $(this).find('option:selected');
        var culture_id = selected.data('culture-id');
        if (culture_id) {
            $('.language select').val(culture_id);
        }
    });
}

function RefreshMessages(detailsId) {
    var url = "/Admin/Messages/GetListByDetails/" + detailsId;
    $.ajax({
        type: 'GET',
        dataType: 'Json',
        url: url,
        success: function (data) {

            $('table#messages_' + detailsId + ' TBODY').empty();

            $.each(data, function (idx, item) {
                var row = "<tr>";
                row += '<td>' + (item.Sender ? item.Sender.FullName : '') + '</td>';
                row += '<td>' + (item.Receiver ? item.Receiver.FullName : '') + '</td>';
                row += '<td>' + item.Subject + '</td>';
                row += '<td>' + item.Text + '</td>';
                row += '<td>' + (new Date(parseInt(item.Date.substr(6)))).toLocaleString() + '</td>';
                row += '<td>' + item.MessageTypeName + '</td>';
                row += "<td align='right'><button type='button' class='btn btn-default view-message' data-id='" + item.Id + "'>" + resources.View + "</button></td>";
                row += "</tr>";
                $('table#messages_' + detailsId + ' TBODY').append(row);
            });

            InitMessagesModal();
        },
        processData: false,
        async: true
    });
}

function InitTargetsModal() {
    $('.create-target').unbind();
    $('.create-target').click(function () {
        var url = "/Admin/Targets/CreatePartial";
        var orderId = $(this).attr('data-id');
        $.get(url + '/' + orderId, function (data) {
            $('#modal-content').html(data);

            $('#modal').modal('show');
            jQuery.validator.unobtrusive.parse('#frmModal');

            AddAsterisk();
            initMap();
            InitFileUpload("/Images/FileUpload", "FileIds");
            InitRegionChange();
        });
    });
}

function RefreshTargets(orderId) {
    var url = "/Admin/Targets/GetDetails/" + orderId;
    $.ajax({
        type: 'GET',
        dataType: 'Html',
        url: url,
        success: function (data) {
            $('#targetList').empty();
            $('#targetList').append(data);

            InitActionsModal();
            InitMessagesModal();
            initMap();
            InitDropzone();
        },
        processData: false,
        async: true
    });
}

function InitTranslationModal() {
    $('.translate').unbind();
    $('.translate').click(function () {
        var url = "/Admin/Translations/UpdatePartial";
        var params = $(this).attr('data-id').split('|');

        $.get(url + '/?id=' + params[2] + "&table=" + params[0] + "&field=" + params[1], function (data) {
            $('#modal-content').html(data);

            $('#modal').modal('show');
            jQuery.validator.unobtrusive.parse('#frmModal');

            AddAsterisk();
        });
    });
}

function RefreshLanguages(recordId, table, field) {

    alert('updated');

}

function AddAsterisk() {
    $('input[type=text]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && text.indexOf('*') === -1) {
                label.append('<span style="color:red"> *</span>');
            }
        }
    });
}

function InitDatePicker() {
    $('.datetimepicker').datetimepicker({
        format: resources.DateTimeFormat
    });
    $('.datepicker').datetimepicker({
        format: resources.DateFormat
    });

    $.validator.methods.date = function (value, element) {
        return this.optional(element)
            || moment(value, resources.DateFormat).format(resources.DateFormat) === value
            || moment(value, resources.DateTimeFormat).format(resources.DateTimeFormat) === value;
    }
}