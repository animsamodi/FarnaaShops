function headerSearch() {
    var searchinput = $('.js-input_search');

    searchinput.on('keyup', function () {

        var q = searchinput.val();
        if (q.length <= 0) {
            $('.js-search_result-category').empty()
            $('.js-search_result-suggest').empty()
            $('.c-search_result-footer').show()
            $('.js-search_result-category').hide()
            $('.js-search_result-suggest').hide()
            return;
        }
        $('.js-search_result-category').empty()
        $('.js-search_result-suggest').empty()
        $('.c-search_result-footer').hide()
        $('.js-search_result-category').show()
        $('.js-search_result-suggest').show()
        $.ajax({
            type: 'GET',
            url: 'HeaderSearch',
            data: { q: q },
            success: function (data) {
                if (data.status) {
                    console.log('test')
                    if (data.res.length >= 3) {
                        for (var i = 0; i < 3; i++) {
                            $('.js-search_result-category').append(
                                '<li><a>'
                                + ' <span class="c-header_search_result-item  c-header_search_result-icon-search">'
                                + data.res[i].word + ' در دسته '
                                + '</span>'
                                + '<span class="c-header_search_result-item-category">'
                                + data.res[i].categoryName
                                + '</span>'
                                + '</a></li>')
                        }
                    }
                    else {

                        for (var i = 0; i < data.res.length; i++) {
                            $('.js-search_result-category').append(
                                '<li><a>'
                                + ' <span class="c-header_search_result-item  c-header_search_result-icon-search">'
                                + data.res[i].word + ' در دسته '
                                + '</span>'
                                + '<span class="c-header_search_result-item-category">'
                                + data.res[i].categoryName
                                + '</span>'
                                + '</a></li>')
                        }
                    }

                    if (data.suggest.length >= 5) {
                        for (var i = 0; i < 5; i++) {
                            $('.js-search_result-suggest').append(
                                '<li><a>'
                                + ' <span class="c-header_search_result-item ">'
                                + data.res[i].word
                                + '</span>'
                                + '</a></li>')
                        }
                    }
                    else {
                        for (var i = 0; i < data.suggest.length; i++) {
                            $('.js-search_result-suggest').append(
                                '<li><a>'
                                + ' <span class="c-header_search_result-item">'
                                + data.res[i].word
                                + '</span>'
                                + '</a></li>')
                        }
                    }
                }
            }
        })
    })

}

