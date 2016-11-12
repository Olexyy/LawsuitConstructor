$(document).ready(function () {
    var trigger = '#trigger';
    var toggle = '.toggle';
    $(toggle).hide();
    $(trigger).on('change', function () {
        if ($(trigger).val() == '0')
            $(toggle).hide();
        else
            $(toggle).show();
    });
});