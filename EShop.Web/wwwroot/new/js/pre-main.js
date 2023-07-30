
//////////////////////////////////////////////// Popular Products ////////////////////////////////////////////////

setTimeout(() => {
 
    $(".popular-products .owl-carousel").owlCarousel({
        rtl: true,
        loop: false,
        items: 6,
        nav: true,
        dots: false,
        navText: ["<i class='fal fa-chevron-right'></i>", "<i class='fal fa-chevron-left'></i>"],

        responsive: {
            0: {
                items: 2,

            },
            500: {
                items: 2,

            },
            940: {
                items: 3,

            },
            1000: {
                items: 4,

            },
            1200: {
                items: 5,

            }
        }

    });
    $(".owl-carousel-preload").removeClass("owl-carousel-preload")
    $(".item-product-preload").removeClass("item-product-preload")

}, 6000);