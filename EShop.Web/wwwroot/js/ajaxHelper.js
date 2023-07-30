const UrlApi = 'https://api.dmacourse.com/api';

function loading() {
    Swal.fire({
        title: "منتظر بمانید",
        imageUrl: "../front/gif/alert_loading.gif",
        text: "در حال ارسال اطلاعات...",
        showConfirmButton: false,
        width: 400,

    });
}
function Post(Data, Url, Success, _Error) {
    $.ajax({
        url: UrlApi + Url,
        type: 'POST',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (xhr) {
            var Token = getCookie("Token");
            if (Token != null)
                xhr.setRequestHeader('Authorization', 'Bearer ' + Token);
        },
        data: Data,
        success: function (data) {

            Success(data);
        },
        error: function (response) {

            _Error(response.responseJSON);

        },
    });

}
function Get(Url, Success, _Error) {
    $.ajax({
        url: UrlApi + Url,
        type: 'GET',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (xhr) {
            var Token = getCookie("Token");
            if (Token != null)
                xhr.setRequestHeader('Authorization', 'Bearer ' + Token);
        },

        success: function (data) {

            Success(data);
        },
        error: function (response) {

            _Error(response.responseJSON);

        },
    });
}