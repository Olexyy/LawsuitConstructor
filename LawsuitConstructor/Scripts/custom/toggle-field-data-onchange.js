$(document).ready(function () {
    var trigger = '.trigger';
    var control = '.control';
    var toggle = '.toggle';
    function doToggle(e) {
        if ($(e).find(control)[0].checked == true)
            $(e).find(toggle).each(function () { $(this).show() });
        else
            $(e).find(toggle).each(function () { $(this).hide() });
    }
    $(trigger).each(function () {
        doToggle(this);
    });
    $(trigger).on('click', function () {
        doToggle(this);
    });
});