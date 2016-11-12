$(document).ready(function () {
    $('ul.collapsible1').hide();
    $('ul.collapsible2').hide();
    $('ul.toggle1 li').on('click', function () {
        $(this).find('ul.collapsible1').each(function () { $(this).toggle(); });
    });
    $('ul.toggle2 li').on('click', function (e) {
        $(this).parent().stop();
        $(this).find('ul.collapsible2').each(function () { $(this).toggle(); });
        e.stopPropagation();
    });
});