function updateTextView(_obj) {
    var num = getNumber(_obj.val());
    if (num == 0) {
        _obj.val('0');
    } else {
        _obj.val(num.toLocaleString());
    }
}
function getNumber(_str) {
    var arr = _str.split('');
    var out = new Array();
    for (var cnt = 0; cnt < arr.length; cnt++) {
        if (isNaN(arr[cnt]) == false) {
            out.push(arr[cnt]);
        }
    }
    return Number(out.join(''));
}
$(document).ready(function () {
    $('.format-price').on('keyup', function () {
        updateTextView($(this));
    });

    $('.format-number-ziro').on('keyup', function () {
        
        var num = getNumber($(this).val());
        if (num == 0) {
            $(this).val('0');
        } else {
            $(this).val(num);
        }    });
});



function submitForm(e) {
    $('.format-price').each((index, input) => {
        const $input = $(input);
        $input.val($input.val().replace(/,/g, ''));
   
    });
    return true;
}