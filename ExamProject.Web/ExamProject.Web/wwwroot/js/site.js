const baseUrl = "https://localhost:7045/api/"


$(document).ready(function () {
    if (window.location == "https://localhost:7050/Exam/Index") {
        GetArticleList();
    }
});
function GoToHomeIndex() {
    window.location.href = "/Exam/Index";
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
            sessionStorage.setItem("token", response.data);
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
            sessionStorage.setItem("role", response.data);
            GoToHomeIndex();
        },
        error: function (xhr) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}

function GetArticleList() {
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
}

function GetArticleListPartial(response) {
    var myUrl = "/Exam/GetArticleListPartial";
    $.ajax({
        url: myUrl,
        type: "POST",
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


function GetArticleContent(title, content) {
    var myUrl = "/Exam/GetArticlePartial";
    $.ajax({
        url: myUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ title: title, content: content }),
        success: function (result) {
            $("#letMeSee").html(result);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}


function CreateExam() {
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
    var articleTitle = $("#title").text();
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
            console.log(data);
        },
        error: function (error) {
            console.log(error);
        }
    });
}