#pragma checksum "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41e7c70027f6086a50e00628e64cb3a1b32c26fe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Profile), @"mvc.1.0.razor-page", @"/Views/Home/Profile.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"41e7c70027f6086a50e00628e64cb3a1b32c26fe", @"/Views/Home/Profile.cshtml")]
    public class Views_Home_Profile : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js""></script>
<link rel=""stylesheet"" href=""https://pro.fontawesome.com/releases/v5.10.0/css/all.css""
      integrity=""sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p"" crossorigin=""anonymous"" />
    <style>
        .user {
            text-align: center;
            display: flex;
            flex-direction: column;
            justify-content: center;
            padding: 20px 0;
            background-color: rgba(0, 0, 0, 0.8);
            color: white;
            font-family: Bahnschrift, Arial, Helvetica, sans-serif;
        }

            .user ul {
                display: flex;
                justify-content: center;
            }

        .user-data {
            margin: 20px 0;
            font-size: 22px;
        }

        .change-data {
            display: block;
            list-style-type: none;
            font-size: 20px;
            margin: 30px 20px;
        }
");
            WriteLiteral(@"
        button.changedata {
            padding: 10px 20px;
            background-color: dodgerblue;
            color: white;
            font-weight: bold;
            border: none;
        }

            button.changedata:hover {
                cursor: pointer;
                background-color: mediumspringgreen;
                color: black;
            }

        #profileimg {
            max-width: 100px;
            border: 2px solid white;
            padding: 20px;
            border-radius: 200px;
        }

        .employerads {
            text-align: left !important;
            font-family: Bahnschrift, Arial, Helvetica, sans-serif;
            color: white;
            padding: 20px;
        }

        .signs i {
            font-size: 30px;
        }

        .ads {
            background-color: rgba(0, 0, 0, 0.8);
            padding: 20px;
        }

        .empty {
            height: 50px;
        }
        .deleteads i {
            color: whi");
            WriteLiteral(@"te;
        }

        .deleteads button {
            background: none;
            border: none;
            cursor: pointer;
        }

        .deleteads i:hover {
            color: crimson;
        }

        .ads a {
            font-size: 20px;
            color: mediumspringgreen;
        }

            .ads a:hover {
                color: dodgerblue;
            }

        .ads-title {
            font-style: italic;
        }
    </style>
");
#nullable restore
#line 103 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
  

    int? id = @HttpContextAccessor.HttpContext.Session.GetInt32("Id");
    string url = (@HttpContextAccessor.HttpContext.Session.Keys.Contains("Company")) ? "/users/employers/id=" : "/users/students/id=";
    url += id;
    

#line default
#line hidden
#nullable disable
            WriteLiteral("<script>\r\n        let user = null;\r\n        $.ajax({\r\n            method: \"GET\",\r\n            headers: {\r\n                \'Authorization\': \"Bearer ");
#nullable restore
#line 113 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
                                    Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n            },\r\n            url: \"");
#nullable restore
#line 115 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
             Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            success: function (data) {
                user = data;
                $('.names').append(user.firstName + "" "" + user.lastName);
                $('.email').append(user.email);
                if (user.hasOwnProperty('companyName')) {
                    $('.changedataul').append('<li class=""change-data"" id=""company-field""><button type=""button"" id=""changecompany"" class=""changedata"">'
                        + ""Изменить компанию"" + '</button></li>');
                    $('#addadsbtn').html('Добавить обьявление');
                    let ads = user.advertisements;
                    for (let i = 0; i < ads.length; i++) {
                        $('.employerads').append('<div class=""ads""><div class=""signs""><form class=""deleteads""><button type=""submit""><i class=""fas fa-trash-alt""></i></button></form>' +
                            '</div><h2 class=""ads-title"">' + ads[i].title + '</h2><a href=""/home/advertisements/id=' + ads[i].advertisementId + '"">Learn more...</a></div>'
          ");
            WriteLiteral(@"                  + '<div class=""empty""></div>');
                    }
                    $('.deleteads').each(function (id) {
                        $(this).submit(function () {
                            $.ajax({
                                type: ""DELETE"",
                                headers: {
                                    'Authorization': 'Bearer ");
#nullable restore
#line 135 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                                },
                                async: false,
                                url: '/advertisements/id=' + ads[id].advertisementId + '/delete',
                                error: function (response) {
                                    alert(JSON.stringify(response));
                                }
                            });
                        });
                    });

                } else {
                    $('#addadsbtn').hide();
                    $('.changedataul').append('<li class=""change-data""><button type=""button"" id=""addresume"" class=""changedata"">'
                        + ""Добавить резюме"" + '</button></li>');
                }
                $('#changepass-form').hide();
                $('#changeemail-form').hide();
                $('button#changepass').click(function () {
                    let userdto = {
                        FirstName: user.firstName,
                        LastName: user.lastName,
   ");
            WriteLiteral(@"                     Email: user.email,
                        CompanyName: user.companyName,
                        Password: """"
                    };
                    $('#changepass-form').show();
                    $('#changepass-form').submit(function (e) {
                        e.preventDefault();
                        userdto.Password = $('input[name=""changepass""]').val();
                        $.ajax({
                            method: ""PUT"",
                            headers: {
                                'Authorization': ""Bearer ");
#nullable restore
#line 168 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
                                                    Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                            },\r\n                            async: false,\r\n                            url: \"");
#nullable restore
#line 171 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
                             Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/update/password"",
                            data: JSON.stringify(userdto),
                            contentType: ""application/json; charset=utf-8"",
                            success: function (data) {
                                $('#changepass-form').hide();
                                $('#changepassinfo').html(""<span style='color: mediumspringgreen;'>Password updated successfully</span>"");
                            },
                            error: function (data) {
                                $('#changepassinfo').html(""<span style='color: crimson;'>Failed to update password</span>"");
                            }
                        });
                    });
                });


                $('button#changeemail').click(function () {
                    let userdto = {
                        FirstName: user.firstName,
                        LastName: user.lastName,
                        Email: """",
                        CompanyName: user.companyN");
            WriteLiteral(@"ame,
                        Password: ""Roflan""
                    };
                    $('#changeemail-form').show();
                    $('#changeemail-form').submit(function (e) {
                        e.preventDefault();
                        userdto.Email = $('input[name=""changeemail""]').val();
                        $.ajax({
                            method: ""PUT"",
                            headers: {
                                'Authorization': ""Bearer ");
#nullable restore
#line 201 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
                                                    Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                            },\r\n                            async: false,\r\n                            url: \"");
#nullable restore
#line 204 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
                             Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/update/email"",
                            data: JSON.stringify(userdto),
                            contentType: ""application/json; charset=utf-8"",
                            success: function (data) {
                                $('#changeemail-form').hide();
                                $('#changeemailinfo').html(""<span style='color: mediumspringgreen;'>Email updated successfully</span>"");
                            },
                            error: function (data) {
                                alert(JSON.stringify(data));
                                $('#changeemailinfo').html(""<span style='color: crimson;'>Failed to update email</span>"");
                            }
                        });
                    });
                });
            }
        });

    </script>
");
            WriteLiteral(@"<div class=""user"">
    <div><img src=""https://flaticons.net/icon.php?slug_category=application&slug_icon=user-profile"" id=""profileimg""></div>
    <div>
        <div class=""user-data names"">
        </div>
        <div class=""user-data email""></div>
    </div>
    <h4 id=""changeerror"" style=""color: crimson;""></h4>
    <ul class=""changedataul"">
        <li class=""change-data"" id=""email-field"">
            <button type=""button"" class=""changedata"" id=""changeemail"">Изменить почту</button>
            <form method=""post"" id=""changeemail-form"" style=""margin-top: 20px"">
                <input type=""email"" name=""changeemail"" style=""border: none; font-size: 16px; padding: 5px""><br />
                <button type=""submit"" class=""signout"" style=""border:none; color: white; padding: 0 10px;
                        font-size: 22px; background-color: dodgerblue; font-family: Bahnschrift, Arial, serif; cursor: pointer; margin-top: 20px"">
                    Подтвердить
                </button>
            </");
            WriteLiteral(@"form>
            <h5 id=""changeemailinfo"" style=""margin-top: 20px""></h5>
        </li>
        <li class=""change-data"" id=""pass-field"">
            <button type=""button"" class=""changedata"" id=""changepass"">Изменить пароль</button>
            <form method=""post"" id=""changepass-form"" style=""margin-top: 20px"">
                <input type=""password"" name=""changepass"" style=""border: none; font-size: 16px; padding: 5px""><br />
                <button type=""submit"" class=""signout"" style=""border:none; color: white; padding: 0 10px;
                        font-size: 22px; background-color: dodgerblue; font-family: Bahnschrift, Arial, serif; cursor: pointer; margin-top: 20px"">Подтвердить</button>
            </form>
            <h5 id=""changepassinfo"" style=""margin-top: 20px""></h5>
        </li>
        <li class=""change-data"">
            <button type=""button"" id=""addadsbtn"" class=""changedata""></button>
        </li>
    </ul>
    ");
#nullable restore
#line 256 "C:\Users\ASUS\source\repos\StudentPositionHunters\Views\Home\Profile.cshtml"
Write(await Html.PartialAsync("AdvertisementForm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"employerads\">\r\n\r\n    </div>\r\n    <script>\r\n        $(\'#addadsbtn\').click(function () {\r\n            $(\'.advertisement\').show();\r\n        });\r\n        $(\'.advertisement\').hide();\r\n");
            WriteLiteral("    </script>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Views_Home_Profile> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_Profile> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_Profile>)PageContext?.ViewData;
        public Views_Home_Profile Model => ViewData.Model;
    }
}
#pragma warning restore 1591
