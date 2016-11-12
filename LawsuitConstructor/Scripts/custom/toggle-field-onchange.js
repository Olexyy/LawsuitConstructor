$(document).ready(function () {
    var triggerCase = '4';
    var trigger = '#trigger';
    var toggle = '.toggle';
    function doToggle() {
        if ($(trigger).val() == triggerCase)
            $(toggle).hide()
        else
            $(toggle).show();
    }
    $(trigger).on('change', function () {
        doToggle();
    });
    doToggle();
});