(function ($) {
    $.fn.focusTextToEnd = function () {
        this.focus();
        var $thisVal = this.val();
        this.val('').val($thisVal);
        return this;
    }
}(jQuery));
(function ($) {
    $.fn.formNavigation = function () {
        $(this).each(function () {
            $(this).find('input').on('keyup', function (e) {
                switch (e.which) {
                case 37:
                        $(this).closest('td').next().find('input').focusTextToEnd(); break;
                case 39:
                        $(this).closest('td').prev().find('input').focusTextToEnd(); break;
                case 40:
                        $(this).closest('tr').next().children().eq($(this).closest('td').index()).find('input').focusTextToEnd(); break;
                case 38:
                        $(this).closest('tr').prev().children().eq($(this).closest('td').index()).find('input').focusTextToEnd(); break;
                }
            });
        });
    };
})(jQuery);



 
$(document).ready(function () {
    $('.formNavigation').formNavigation();
    $('.formNavigation :input:enabled:visible:first').focusTextToEnd();

});