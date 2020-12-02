$(document).ready(function () {


    $('.addnewsbtn').click(function () {
        $('.addnewsblock').show();
    });
    $('.addarticlebtn').click(function () {
        $('.addarticleblock').show();
    });

    $('.closenewsblock').click(function () {
        $('.addnewsblock').hide();
    });

    $('.closearticleblock').click(function () {
        $('.addarticleblock').hide();
    });
    //request for registration of user
    $('#registration').submit(function (e) {
        let user = $('input[name="user"]:checked').val();
        e.preventDefault();
        if (user === 'employer') {
            let employer = {
                FirstName: $('#FirstName').val(),
                LastName: $('#LastName').val(),
                Email: $('#Email').val(),
                Password: $('#Password').val(),
                CompanyId: $('#CompanyName').val()
            };
            let isValid = false;
            for (let key in employer) {
                if (employer[key] === '') isValid = true;
            }
            if (isValid === true) $('#regerror').html("Fill all fields");
            else {
                $.ajax({
                    type: "POST",
                    url: "/users/employers/auth/register",
                    data: JSON.stringify(employer),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        window.location.reload();
                    },
                    error: function (response) {
                        $('#regerror').html(response.responseText);
                    }
                });
            }
        } else {
            let student = {
                FirstName: $('#FirstName').val(),
                LastName: $('#LastName').val(),
                Email: $('#Email').val(),
                Password: $('#Password').val()
            };
            let isValid = false;
            for (let key in student) {
                if (student[key] === '') isValid = true;
            }
            if (isValid === true) $('#regerror').html("Fill all fields");
            else {
                $.ajax({
                    type: "POST",
                    url: "/users/students/auth/register",
                    data: JSON.stringify(student),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        window.location.reload();
                    },
                    error: function (response) {
                        $('#regerror').html(response.responseText);
                    }
                });
            }
        }
    });



    //request for user authentification
    $('#authorisation').submit(function (e) {
        let user = {
            Email: $('#AuthEmail').val(),
            Password: $('#AuthPassword').val()
        };
        let authurl = "";
        let role = $('input[name="authuser"]:checked').val();
        if (role === 'employer') {
            authurl = "/users/employers/auth/login";
        } else {
            authurl = "/users/students/auth/login";
        }
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: authurl,
            data: JSON.stringify(user),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.location.reload();
            },
            error: function (response) {
                $('#autherror').html(response.responseText);
            }
        });
    });


    $('.changeemailform').hide();
    $('.changeemailbtn').click(function () {
        $('.changeemailform').show();
    });
    $('.changecompanyform').hide();
    $('.changecompanybtn').click(function () {
        $.get(
            '/companies'
        ).done(function (data) {
            for (let i = 0; i < data.length; i++)
            $('.newcompany').append('<option value="' + data[i].name + '">' + data[i].name + '</option>');
        });
        $('.changecompanyform').show();
    });
    $('.changepasswordform').hide();
    $('.changepasswordbtn').click(function () {
        $('.changepasswordform').show();
        $('.newpasswordblock').hide();
    });
});
function substr(index, str) {
    var s = "";
    for (let i = index; i < str.length; i++) s += str[i];
    return s;
}