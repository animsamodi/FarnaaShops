$(document).ready(function() {


    //////////////////////////////////////////////// Brand ////////////////////////////////////////////////

    $(".brand .owl-carousel").owlCarousel({
        rtl: true,
        items: 12,
        margin: 15,
        nav: true,
        dots: false,
        navText: ["<i class='fal fa-chevron-right'></i>", "<i class='fal fa-chevron-left'></i>"],

        responsive: {
            0: {
                items: 3,

            },
            300: {
                items: 4,

            },
            400: {
                items: 6,

            },
            600: {
                items: 8,

            },
            1000: {
                items: 12,

            }
        }

    });

    /////////////////////////////////////////////// available - soon //////////////////////////////

    $(".available-soon .owl-carousel").owlCarousel({
        rtl: true,
        items: 7,
        nav: true,
        dots: false,
        navText: ["<i class='fal fa-chevron-right'></i>", "<i class='fal fa-chevron-left'></i>"],

        responsive: {
            0: {
                items: 2,

            },
            300: {
                items: 2,

            },
            600: {
                items: 4,

            },
            1000: {
                items: 7,

            }
        }

    });




});