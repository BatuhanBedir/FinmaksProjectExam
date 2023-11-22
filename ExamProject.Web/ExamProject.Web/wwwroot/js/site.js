const baseUrl = "https://localhost:7045/api/"


$(document).ready(function () {
    if (window.location == "https://localhost:7050/Exam/Index") {
        ExamList();
    }
    if (sessionStorage.getItem("token") != null) {
        $('#logoutButton').show();
        $('#loginButton').hide();
        $('#examButton').show();
    } else {
        $('#logoutButton').hide();
        $('#examButton').hide();
        $('#loginButton').show();
    }
});

function GoToExamIndex() {
    window.location.href = "/Exam/Index";
}
function GoToAccountLogin() {
    window.location.href = "/Account/Login"
}
function Login() {

    let username = $('#username').val();
    let password = $('#password').val();

    if (!username || !password) {
        $('#loginFail').text('Username and password are required.').css('color', 'red');
        return;
    }
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
            sessionStorage.setItem("token", response.data);
            GetLoggedInUserRole();
        },
        error: function (xhr) {
            try {
                var errorResponse = JSON.parse(xhr.responseText);
                if (errorResponse && errorResponse.errors) {
                    $('#loginFail').text(errorResponse.errors).css('color', 'red');
                } else {
                    $('#loginFail').text('An unknown error occurred.').css('color', 'red');
                }
            } catch (error) {
                $('#loginFail').text('An error occurred while processing the response.').css('color', 'red');
            }
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
            sessionStorage.setItem("role", response.data);
            GoToExamIndex();
        },
        error: function (xhr) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}
function GetArticleList() {

    var role = sessionStorage.getItem("role")
    if (role === "admin") {

        var myUrl = baseUrl + "Exam";
        $.ajax({
            url: myUrl,
            type: "GET",
            headers: {
                "Authorization": 'Bearer ' + sessionStorage.getItem("token")
            },
            success: function (response) {
                GetArticleListPartial(response.data)
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        });
    } else {
        GoToAccountLogin();
    }

}
function GetArticleListPartial(response) {
    var role = sessionStorage.getItem("role")
    if (role === "admin") {

        var myUrl = "/Exam/GetArticleListPartial";
        $.ajax({
            url: myUrl,
            type: "POST",
            headers: {
                "Authorization": 'Bearer ' + sessionStorage.getItem("token")
            },
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(response),
            success: function (result) {
                $("#letMeSee").html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        });
    }
    else {
        GoToAccountLogin();
    }
}
function GetSelectedArticleContent() {
    var content = document.getElementById("articleTitle").value;

    var select = document.getElementById("articleTitle");
    var selectedValue = select.options[select.selectedIndex].value;

    var values = selectedValue.split('|');
    var content = values[1];

    if (content === '') {
        document.getElementById("articleContentDiv").innerHTML = "";
        return;
    }
    var role = sessionStorage.getItem("role");
    if (role === "admin") {
        var myUrl = "/Exam/GetArticlePartial";
        $.ajax({
            url: myUrl,
            type: "POST",
            headers: {
                "Authorization": 'Bearer ' + sessionStorage.getItem("token")
            },
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ content: content }),
            success: function (result) {
                $("#articleContentDiv").html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        });
    }
}
function CreateExam() {
    var role = sessionStorage.getItem("role")
    if (role === "admin") {

        var formDataArray = [];

        const validAnswers = ['A', 'B', 'C', 'D'];
        for (var i = 1; i < 5; i++) {
            var question = $('#question' + i).val();
            var option1 = $('#option' + i + '_1').val();
            var option2 = $('#option' + i + '_2').val();
            var option3 = $('#option' + i + '_3').val();
            var option4 = $('#option' + i + '_4').val();
            var correctAnswer = $('#correct-answer' + i).val().toUpperCase();

            if (!validAnswers.includes(correctAnswer)) {
                alert(`Please enter a valid answer (A,B,C or D) for Question ${i}`)
                return;
            }

            var correctAnswerValue = '';

            switch (correctAnswer) {
                case 'A':
                    correctAnswerValue = option1;
                    break;
                case 'B':
                    correctAnswerValue = option2;
                    break;
                case 'C':
                    correctAnswerValue = option3;
                    break;
                case 'D':
                    correctAnswerValue = option4;
                    break;
                default:
                    break;
            }
            var questionDto = {
                question: question,
                option1: option1,
                option2: option2,
                option3: option3,
                option4: option4,
                correctAnswer: correctAnswerValue
            };

            formDataArray.push(questionDto);
        }

        var select = document.getElementById("articleTitle");
        var selectedValue = select.options[select.selectedIndex].value;

        var values = selectedValue.split('|');
        var articleTitle = values[0];

        //var articleTitle = $("#title").text();
        var articleContent = $("#content").text();
        var examDto = {
            questions: formDataArray,
            title: articleTitle,
            content: articleContent
        };

        var myUrl = baseUrl + "Exam";

        $.ajax({
            url: myUrl,
            type: 'POST',
            data: JSON.stringify(examDto),
            contentType: 'application/json',
            success: function (data) {
                ExamList();
            },
            error: function (xhr) {
                try {
                    var errorResponse = JSON.parse(xhr.responseText);
                    if (errorResponse && errorResponse.errors) {
                        $('#fail').text(errorResponse.errors).css('color', 'red');
                    } else {
                        $('#fail').text(xhr.error).css('color', 'red');
                    }
                } catch (error) {
                    $('#fail').text('An error occurred while processing the response.').css('color', 'red');
                }
            }
        });
    } else {
        GoToAccountLogin();
    }
}
function ExamList() {
    var role = sessionStorage.getItem("role")
    var myUrl = baseUrl + "Exam/GetAllExam";
    if (role != null) {
        $.ajax({
            url: myUrl,
            type: "GET",
            headers: {
                "Authorization": 'Bearer ' + sessionStorage.getItem("token")
            },
            success: function (response) {
                GetExamListPartial(response.data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        });
    } else {
        GoToAccountLogin();
    }

}
function GetExamListPartial(exams) {
    var myUrl = "/Exam/GetExamListPartial";
    var role = sessionStorage.getItem("role")

    var requestData = {
        exams: exams,
        role: role
    };
    $.ajax({
        url: myUrl,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(requestData),
        success: function (response) {
            $("#letMeSee").html(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}
function DeleteExam(id) {
    var myUrl = baseUrl + "Exam/" + id;
    $.ajax({
        url: myUrl,
        type: "DELETE",
        success: function (data, textSatus, xhr) {
            if (xhr.status == 200) {
                ExamList();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }

    });
}
function TakeTheExam(id) {
    var myUrl = baseUrl + "Exam/" + id;
    $.ajax({
        url: myUrl,
        type: "GET",
        headers: {
            "Authorization": 'Bearer ' + sessionStorage.getItem("token")
        },
        success: function (response) {
            UserExamList(response.data);
        }, error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }

    });
}
function UserExamList(exams) {
    var myUrl = "/Exam/GetUserExamListPartial";

    $.ajax({
        url: myUrl,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(exams),
        success: function (response) {
            $("#letMeSee").html(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }

    });
}
function Logout() {
    var myUrl = "/Account/Logout";

    $.ajax({
        url: myUrl,
        type: "POST",
        headers: {
            "Authorization": 'Bearer ' + sessionStorage.getItem("token")
        },
        success: function () {
            sessionStorage.removeItem("token");
            sessionStorage.removeItem("role");
            GoToAccountLogin();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    })
}