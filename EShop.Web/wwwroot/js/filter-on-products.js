var search_url =decodeURIComponent( window.location.href);
// constants
const minPrice = 'min_price'
    , maxPrice = 'max_price',
    brand = 'brand',
    seri = 'seri',
    catId = 'catId',
    search='q',
    availableStock = 'availablestock',
    discounted = 'discounted',
    page = 'page';

function getFilterValueByTitle (filterTitle) {
    var results = new RegExp('[\?&]' + filterTitle + '=([^&#]*)')
        .exec(window.location.search);

    return (results !== null) ? results[1] || 0 : false;
}

function init_search_category_page(max_price_default) {

    var min_price_filter = 0;
    var max_price_filter = max_price_default;

    var read_filters_url = window.location.href.slice(window.location.href.indexOf('?') + 1);
    var filters = read_filters_url.split('&');
    try {
        if (filters.length > 0) {
            //debugger;
            for (var i = 0; i < filters.length + 1; i++) {
                var filterItem = filters[i].split("=");

                // has filter
                if (filterItem.length > 1) {
                    var filterTitle = filterItem[0];
                    var filterValue = filterItem[1];
                    // check empty or null filter
                   if(filterValue!=='' && filterTitle!=='')
                       // check for d none filters section
                       if (filterTitle === "sort")
                       {
                        $(".nav-item-sort").removeClass("sort-active");
                        $(".nav-item-sort[data-type='sort'][data-id='" + parseInt(filterValue) + "']").addClass("sort-active");
                       } 
                    else{
                        $('.rangeSlider-desktop').parents(".js-box_filter").siblings(".js-box_toggleable").removeClass('is-hidden')
                        $('.rangeSlider-desktop').parents(".js-box_filter").slideDown();

                        show_selected_filters_box()
                        if (filterTitle === "seri") {
                            var checkbox = $(".js-filter_checkbox[data-type='seri'][data-id='" + parseInt(filterValue) + "']");
                            checkbox.prop('checked', true);
                            $('.js-filters').append(add_filter_item_badge({
                                text: `سری ${checkbox.data('fa')}`,
                                type: filterTitle,
                                id: filterValue
                            }))
                        } 
                        else if (filterTitle === "brand") {
                            var checkbox = $(".js-filter_checkbox[data-type='brand'][data-id='" + parseInt(filterValue) + "']");
                            checkbox.prop('checked', true);
                            $('.js-filters').append(add_filter_item_badge({
                                text: `برند ${checkbox.data('fa')}`,
                                type: filterTitle,
                                id: filterValue
                            }))
                        } 
                        else if (filterTitle === minPrice &&filterValue!=="0"){
                        
                            min_price_filter = filterValue;
                            $('.js-filters').append(add_filter_item_badge({
                                text: `از ${ToFadigit(currency(min_price_filter))} تومان`,
                                type: minPrice,
                                id: filterValue
                            }))
                        }
                        else if (filterTitle === maxPrice) {
                            max_price_filter = filterValue;
                           
                            $('.js-filters').append(add_filter_item_badge({
                                text: `تا ${ToFadigit(currency(max_price_filter))} تومان`,
                                type: maxPrice,
                                id: filterValue
                            }))
                        } 
                        else if (filterTitle ===availableStock) {
                            if (filterValue === "true") {
                                $(".available-stock-only").prop('checked', true);
                                $('.js-filters').append(add_filter_item_badge({
                                    text: `فقط کالا های موجود`,
                                    type: filterTitle,
                                    id: filterValue
                                }))
                            }                            else
                                $(".available-stock-only").prop('checked', false);
                        }
                        else if (filterTitle === discounted) {
                            if (filterValue === "true") {
                                $(".available-discounted-only").prop('checked', true);
                                $('.js-filters').append(add_filter_item_badge({
                                    text: `فقط کالا های تخفیف دار`,
                                    type: filterTitle,
                                    id: filterValue
                                }))
                            }                            else
                                $(".available-discounted-only").prop('checked', false);
                        }
                        else if (filterTitle === catId) {

                            var checkbox = $(".js-filter_checkbox[data-type='catId'][data-id='" + parseInt(filterValue) + "']");
                            checkbox.prop('checked', true);
                            $('.js-filters').append(add_filter_item_badge({
                                text: `دسته بندی ${checkbox.data('fa')}`,
                                type: catId,
                                id: filterValue
                            }))
                        }
                        else if (filterTitle === search ) 
                                {
                            $('.js-filters').append(add_filter_item_badge({
                                text: `جستجو ${decodeURIComponent(filterValue)}`,
                                type: search,
                                id: filterValue
                            }))}
                    }
                }
            }
            if (max_price_filter > min_price_filter) {

                $('.js-slider_range-from').attr("data-value", min_price_filter)
                $('.js-slider_range-to').attr("data-value", max_price_filter)
            }
        }
        else
            $('.js-filter_box').remove();
    }
    catch { }

    render_slider(min_price_filter, max_price_filter);
    on_sort_change_handler();
    on_change_page_handler();
    on_filter_checkbox_change_handler();
}

function show_selected_filters_box(){
    $('.js-filter_box').removeClass('d-none');
}

function on_sort_change_handler() {
    $(document).on('click', '.nav-item-sort', function () {

        var value = $(this).data("id");
        if (!search_url.includes(`sort=`)) {
            // add_filter_to_path('availablestock', 'true')
            add_filter_to_path('sort', value)
        }
        else {
            if (value === search_url.split('sort=')[1].substring(0, 1)) {
                remove_filter_from_path('sort')
            }
            else
                update_filter_in_path('sort', value)

        }
        $(".nav-item-sort").removeClass("sort-active");

        var input = $(".nav-item-sort[data-type='sort'][data-id='" + parseInt(value) + "']");
        input.addClass("sort-active");

        reload_on_filter()
    })
}

function on_change_page_handler(){
    $(document).on('click', '.js-product-pager', function () {
        if (!search_url.includes(`page=`)) 
            add_filter_to_path(page, $(this).data("id"));
        else
            update_filter_in_path(page,$(this).data("id"))
            
        $(".js-product-pager").removeClass("active");
        $(this).addClass("active");
       
        reload_on_filter();
    })
}
function on_filter_checkbox_change_handler() {

    $(document).on('change', '.available-stock-only', function (e) {
        $("#FilterModal").modal('hide');
        console.log(e)
        if (e.target.checked) {
            remove_filter_from_path(page);
            add_filter_to_path(availableStock, 'true')
        }
        else
            remove_filter_from_path(availableStock)

        reload_on_filter()
    })
    $(document).on('change', '.available-discounted-only', function (e) {
        $("#FilterModal").modal('hide');
        console.log(e)
        if (e.target.checked) {
            remove_filter_from_path(page);
            add_filter_to_path(discounted, 'true')
        }
        else
            remove_filter_from_path(discounted)

        reload_on_filter()
    })

    $(document).on('click', '.js-filter_checkbox', function () {

        $("#FilterModal").modal('hide');
        var value = $(this).data("id");
        console.log($(this).data('type'))
        switch ($(this).data('type')) {
            case 'seri':
                remove_filter_from_path(page);
                if ($(this)[0].checked && !search_url.includes(`seri=${value}`))
                    add_filter_to_path(seri, value)
                else
                    remove_filter_from_path(seri)
                break;
            case 'brand':
                remove_filter_from_path(page);
                if ($(this)[0].checked && !search_url.includes(`brand=${value}`))
                    add_filter_to_path(brand, value)
                else
                    remove_filter_from_path(brand)
                break;
            case 'catId':
                remove_filter_from_path(page);
                if ($(this)[0].checked && !search_url.includes(`catId=${value}`))
                    add_filter_to_path(catId, value)
                else
                    remove_filter_from_path(catId)
                break;
            default:
                break;
        }

        reload_on_filter()
    })

}

$(document).on('click', '.js-list_filter-remove', function () {
    var value = $(this).data('id');
    var type = $(this).data('type');
    remove_filter_from_path(type)
    reload_on_filter()
})
// selector checkbox on text side bar
$(document).on("click",".row-option-filter", function (e) {

    $(this).find('input').click()
});

function render_slider(minprice, maxprice)
{
    try {

        var rangeSliders = [
            document.querySelector('.rangeSlider-desktop'),
            document.querySelector('.rangeSlider-mobile')
        ]
        var input_froms = document.querySelectorAll('.js-slider_range-from');
        var input_tos = document.querySelectorAll('.js-slider_range-to');

        for (var i = 0; i < rangeSliders.length + 1; i++) {
            noUiSlider.create(rangeSliders[i], {
                start: [minprice, maxprice],
                connect: true,
                direction: 'rtl',
                range: {
                    'min': 0,
                    'max': parseInt(input_tos[0].dataset.range)
                }
            });
            rangeSliders[i].noUiSlider.on('update', function (values, handle) {

                var value = values[handle];
                var round_value = Math.round(value);

                if (handle) {
                    for (var i = 0; i < input_tos.length; i++) {
                        input_tos[i].value = currency(ToFadigit(round_value));
                        input_tos[i].setAttribute('data-value', round_value);
                    }
                } else {
                    for (var i = 0; i < input_froms.length; i++) {
                        input_froms[i].value = currency(ToFadigit(round_value));
                        input_froms[i].setAttribute('data-value', round_value);
                    }
                }
            });

            rangeSliders[i].noUiSlider.on('change', function (values, handle) {

                var value = values[handle];
                var round_value = Math.round(value);

                if (handle) {
                   if(parseInt(input_tos[0].dataset.range)!==round_value) {
                       if (search_url.includes(maxPrice))
                           update_filter_in_path(maxPrice, round_value)
                       else
                           add_filter_to_path(maxPrice, round_value)
                   }else
                       remove_filter_from_path(maxPrice)
                }
                else {
                   if(round_value!==0) {
                       if (search_url.includes(minPrice))
                           update_filter_in_path(minPrice, round_value)
                       else
                           add_filter_to_path(minPrice, round_value)
                   }
                   else
                       remove_filter_from_path(minPrice)

                }
                
                reload_on_filter()
            });
        }

    }
    catch (error) { }

};


function add_filter_item_badge(values) {
    var template = '<li>'
        + '<div class="c-list_filter-lable">'
        + '<button class="c-list_filter-remove js-list_filter-remove" data-type="{{type}}" data-id="{{id}}"></button>'
        + '<span>{{text}}</span>'
        + '</div>'
        + '</li>';

    var keys = Object.keys(values);
    for (var i = 0; i < keys.length; i++) {
        template = template.replace(new RegExp('{{ *' + keys[i] + ' *}}'), values[keys[i]]);
    }
    return template
}



function remove_filter_from_path(filter_name) {
    var filter_value=getFilterValueByTitle(filter_name);
    search_url =
        search_url.replace(`?${filter_name}=${filter_value}`, '?')
            .replace(`&${filter_name}=${filter_value}`, '')
            .toString();
}

function update_filter_in_path(filter_name, filter_value) {

    remove_filter_from_path(filter_name);
    add_filter_to_path(filter_name,filter_value)
}
function add_filter_to_path(filter_name, filter_value) {
    // validation 
    if(filter_name!==null && filter_name!=='') {
        if (search_url.includes('?'))
            search_url += `&${filter_name}=${filter_value}`
        else
            search_url += `?${filter_name}=${filter_value}`
    
}
}

function reload_on_filter() {
    // load from first page
remove_filter_from_path(page);
    if (search_url.charAt(search_url.length - 1) == '&') {
        search_url = search_url.substring(0, search_url.length - 1);
    }
    else if (search_url.charAt(search_url.length - 1) == '?') {
        search_url = search_url.substring(0, search_url.length - 1);
    }
    search_url = search_url.replace('?&', '?');
    window.open(search_url, '_self');
}
