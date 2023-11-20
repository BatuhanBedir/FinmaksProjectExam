const baseUrl = "https://localhost:7045/api/"

function GoToHomeIndex() {
    window.location.href = "/Home/Index";
}
function Login() {
    let formData = new FormData();
    formData.append('userName', $('#username').val());
    formData.append('password', $('#password').val());

    var myUrl = baseUrl + "Auth";

    $.ajax({
        url: myUrl,
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            sessionStorage.setItem("token", response);
            alert("Welcome");
            GetLoggedInUserRole();
        },
        error: function (xhr) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}

function GetLoggedInUserRole() {
    var myUrl = baseUrl + 'Auth';
    $.ajax({
        url: myUrl,
        type: "GET",
        headers: {
            Authorization: 'Bearer ' + sessionStorage.getItem("token")
        },
        success: function (response) {
            sessionStorage.setItem("role", response);
            GoToHomeIndex();
        },
        error: function (xhr) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}