$(document).ready(function () {
    var lawsuitId = $('#lawsuit').attr('lawsuitId');
    var listDivId = '#listDiv';
    var selectedDivId = '#selectedDiv';
    var listId = '#list'; // not needed
    var collapsibleClass = '.collapsible2'
    var selectedId = '#selected';
    var UrlAll = "/LawsuitBlock/All";
    var UrlSelected = "/LawsuitBlock/Selected";
    var UrlAction = "/LawsuitBlock/Action";
    //loadAll();
    setCollapsibleEvents();
    loadSelected();
    //Function to set events for Drag-Drop for li items of selected lists
    function setCollapsibleEvents() {
        var lists = $(collapsibleClass);
        //Set Drag on Each 'li' in the list
        lists.each(function (index, element) {
            var list = $(element).find('li');
            $.each(list, function (idx, val) {
                $(this).on('dragstart', function (evt) {
                    evt.originalEvent.dataTransfer.setData('Text', evt.target.textContent);
                    evt.originalEvent.dataTransfer.setData('ID', evt.target.id);
                    evt.originalEvent.dataTransfer.setData('Location', 'list');
                });
            });
        });
    }
    function setSelectedEvents() {
        var selected = $(selectedDivId + ' li');
        //Set Drag on Each 'li' in the selected
        $.each(selected, function (idx, val) {
            $(this).on('dragstart', function (evt) {
                evt.originalEvent.dataTransfer.setData('Text', evt.target.textContent);
                evt.originalEvent.dataTransfer.setData('ID', evt.target.id);
                evt.originalEvent.dataTransfer.setData('Location', 'selected');
            });
        });
    }
    //Set the Drop on the <div>
    $(selectedDivId).on('drop', function (evt) {
        evt.preventDefault();
        var listItems = $(selectedId);
        var location = evt.originalEvent.dataTransfer.getData("Location");
        if (location == "list") {
            var id = evt.originalEvent.dataTransfer.getData("ID");
            if (listItems.find('#' + id).length === 0) {
                //var text = evt.originalEvent.dataTransfer.getData("Text");
                //var li = "<li draggable='true' id=" + id + ">" + text + "</li>";
                //listItems.append(li);
                //listItems.find('#' + id).on('dragstart', function (evt) {
                //evt.originalEvent.dataTransfer.setData("Text", evt.target.textContent);
                //evt.originalEvent.dataTransfer.setData("ID", evt.target.id);
                //evt.originalEvent.dataTransfer.setData("Location", "selected");
                //});
                action(0, lawsuitId, id, 0);
                listItems.empty();
            }
        }
        else {
            var sourceId = evt.originalEvent.dataTransfer.getData("ID");
            var targetId = evt.target.id;
            if (targetId !== null && sourceId !== null && targetId != sourceId) {
                action(2, lawsuitId, sourceId, targetId); // using not by name
                listItems.empty();
            }
        }
    });
    $(listDivId).on('drop', function (evt) {
        evt.preventDefault();
        var location = evt.originalEvent.dataTransfer.getData("Location");
        if (location == "selected") {
            var id = evt.originalEvent.dataTransfer.getData("ID", evt.target.id);
            var listItems = $(selectedId);
            //listItems.find('#' + id).remove();
            action(1, lawsuitId, id, 0);
            listItems.empty();
        }
    });
    //The dragover
    $(selectedDivId).on('dragover', function (evt) {
        evt.preventDefault();
    });
    //The dragover
    $(listDivId).on('dragover', function (evt) {
        evt.preventDefault();
    });
    //The dragover
    $('.toggle').each(function (id, el) {
        $(el).on('dragover', function (evt) {
            evt.preventDefault();
        });
    });
    ///Function to load products using call to WEB API
    function loadAll() {
        var items = '';
        $.ajax({
            type: 'POST',
            url: UrlAll,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
        }).done(function (resp) {
            if (resp.length > 0) {
                $.each(resp, function (idx, val) {
                    items += '<li draggable="true" id=' + val.BlockId + '>' + val.BlockName + '</li>';
                });
                $(listId).html(items);
                setListEvents();
            }
            else {
                $(listId).html('<p > Список пустий </p>');
            }
            loadSelected();
        }).error(function (err) {
            alert('Error! ' + err.status);
        });
    }

    function loadSelected() {
        var items = '';
        $.ajax({
            type: 'POST',
            url: UrlSelected,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: '{"lawsuitId": ' + lawsuitId + '}',
        }).done(function (resp) {
            if (resp.length > 0) {
                $.each(resp, function (idx, val) {
                    items += '<li draggable="true" id=' + val.BlockId + '>' + val.BlockName + '</li>';
                });
                $(selectedId).html(items);
                setSelectedEvents();
            }
            else {
                $(selectedId).html('<p > Список пустий </p>');
            }
        }).error(function (err) {
            alert('Error! ' + err.status);
        });
    }

    function action(action, lawsuitId, blockId, targetBlockId) {
        $.ajax({
            url: UrlAction,
            type: 'POST',
            data: '{ Type:' + action + ', LawsuitId:' + lawsuitId + ', BlockId:' + blockId + ', TargetBlockId:' + targetBlockId + '}',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
        }).error(function (err) {
            alert('Error! ' + err.status);
        }).done(function (resp) { loadSelected(); });
    }
});