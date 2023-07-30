
function discount_timer() {
    var delta;
    var day;
    var hour;
    var minutes;
    var seconds;
    $('.js-counter').each(function (index) {
        var x = $(this);
        var container = document.getElementsByClassName('js-discount_container').item(index);
        var price = container.querySelector('.js-discount_price');
        var price_finish = container.querySelector('.js-discount_price-finish');
        var counter = container.querySelector('.js-discount_counter');
        var counter_finish = container.querySelector('.js-discount_counter-finish');

        var now_date = new Date(x.data('nowdate'));
        var discount_date = new Date(x.data('discountdate'));
        setInterval(function () {
            if (discount_date > now_date) {
                now_date.setSeconds(now_date.getSeconds() + 1);
                delta = (discount_date - now_date) / 1000;
                day = Math.floor(delta / 86400);

                hour = Math.floor(delta / 3600);
                delta -= hour * 3600;

                minutes = Math.floor(delta / 60);
                delta -= minutes * 60;
                seconds = Math.floor(delta);
                if (hour < 10)
                    hour = "0" + hour;
                if (minutes < 10)
                    minutes = "0" + minutes;
                if (seconds < 10)
                    seconds = "0" + seconds;
                x.html('<span>' + hour + '</span>:<span>' + minutes + '</span>:<span>' + seconds + '</span><i class="mdi mdi-timer img-timer-promotion mr-1"></i></span>');
                x.persiaNumber();

            }
            else {
                price.classList.add('not-active');
                counter.classList.add('not-active');

                price_finish.classList.add('is-active');
                counter_finish.classList.add('is-active');
            }
        }, 1000);

    });

 }

discount_timer();