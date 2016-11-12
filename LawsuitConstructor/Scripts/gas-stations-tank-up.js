
jQuery(document).ready(function () {
    var Choise = 0, Max, Current, Result, GasType;
    // prepare
    jQuery("#Button").hide();
    jQuery("#MessageBox").text("Value can't be negative or more than maximum capasity.");
    // listeners
    jQuery("#GasType").on("change paste keyup blur", Submit);
    jQuery("#Value").on("change paste keyup blur", Submit);
    // submit handler
    function Submit (){
        Choise = parseInt(jQuery("#Value").val());
        GasType = parseInt(jQuery("#GasType").val());
        if (GasType === 92)
            Max = parseInt(jQuery("#Max92").text());
        else if (GasType === 95)
            Max = parseInt(jQuery("#Max95").text());
        else if (GasType === 98)
            Max = parseInt(jQuery("#Max98").text());
        if (GasType === 92)
            Current = parseInt(jQuery("#Current92").text());
        else if (GasType === 95)
            Current = parseInt(jQuery("#Current95").text());
        else if (GasType === 98)
            Current = parseInt(jQuery("#Current98").text());
        Result = Max - (Choise + Current);
        if (Choise == 0)
            jQuery("#MessageBox").text("Value can't be negative or more than maximum capasity.");
        else if (Choise < 0)
            jQuery("#MessageBox").text("Value can't be negative.");
        else if (Result < 0) {
            jQuery("#MessageBox").text("Value is too high.");
            jQuery("#Button").hide();
        }
        else {
            jQuery("#MessageBox").text("You've selected " + Choise + " reserve is: " + Result);
            jQuery("#Button").show();
        }
    }
});