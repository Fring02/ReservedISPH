#pragma checksum "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d38061925923f39a6b8cb87dabca87839867f65b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_NewsPage), @"mvc.1.0.view", @"/Views/Home/NewsPage.cshtml")]
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
#line 1 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsPage.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d38061925923f39a6b8cb87dabca87839867f65b", @"/Views/Home/NewsPage.cshtml")]
    public class Views_Home_NewsPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<link rel=""stylesheet"" href=""https://pro.fontawesome.com/releases/v5.10.0/css/all.css""
      integrity=""sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p"" crossorigin=""anonymous"" />
<style>
    div.newsblock {
        font-family: Bahnschrift, Arial, serif;
        padding: 40px;
        background-color: white;
        color: black;
    }

    .newsblock h1 {
        margin: 20px 0;
        border-bottom: 2px solid dodgerblue;
    }

    .newsblock p {
        margin-top: 20px;
        font-style: italic;
    }

    .newsimg {
        display: block;
        max-width: 70%;
        position: relative;
        left: 15%;
        border: 2px solid black;
    }

    .newsdate {
        margin-top: 20px;
        color: darkorange;
    }
</style>
<script>
    $(document).ready(function () {
        let newsUrl = substr(5, location.pathname);
        let news = {};
        $.get(
            newsUrl
        ).done(function (data) {
            news = {");
            WriteLiteral(@"
                Id: data.newsId,
                ImagePath: data.imagePath,
                Title: data.title,
                PublishDate: data.publishDate,
                Description: data.description
            };
            $('.newsblock').append(data);
            $('.newsblock h1').html(data.title);
            $('.newsdate').html(""Дата публикации: "" + data.publishDateString);
            $('.newsblock p').html(data.description);
            $('.newsimgblock').append('<img src=""' + data.imagePath + '"" class=""newsimg"">');
            $('#deletenews-form').submit(function (e) {
                e.preventDefault();
                    $.ajax({
                            type: ""DELETE"",
                            headers:
                            {
                            'Authorization': 'Bearer ");
#nullable restore
#line 61 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsPage.cshtml"
                                                Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                            },
                            async: false,
                            url: '/news/id=' + data.newsId + '/delete',
                            success: function (response) {
                                window.location.replace(""/home/index"");
                             },
                            error: function(response) {
                                alert(JSON.stringify(response));
                            }
                    });
            });
        });


                    $('#changenewstitlebtn').click(function () {
                        $('.changenewstitleli').append('<h5 id=""changenewstitleinfo""></h5><form id=""changenewstitle-form""><input type=""text"" id=""newnewstitle"" style=""font-size: 17px; padding: 3px 0;"">' +
                        '<br><br><button type=""submit"" class=""signout hoverbtn"" style=""background-color: dodgerblue"">Отправить</button></form>');
                        $('#changenewstitle-form').submit(function (e) {
  ");
            WriteLiteral(@"                          e.preventDefault();
                            news.Title = $('#newnewstitle').val();
                            $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 85 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsPage.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                                },
                                async: false,
                                url: ""/news/id="" + news.Id + ""/update"",
                                data: JSON.stringify(news),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                    window.location.reload();
                                },
                                error: function (data) {
                                    $('#changenewstitleinfo').html(""<span style='color: crimson;'>Failed to update title</span>"");
                                }
                            });
                        });
                    });



                    $('#changenewsdatebtn').click(function () {
                        $('.changenewsdateli').append('<h5 id=""changenewsdateinfo""></h5><form id=""changenewsdate-form""><input type=""date"" id=""newnewsdate"" style=""font-size: 17px; pa");
            WriteLiteral(@"dding: 3px 0;"">' +
                            '<br><br><button type=""submit"" class=""signout hoverbtn"" style=""background-color: dodgerblue"">Отправить</button></form>');
                        $('#changenewsdate-form').submit(function (e) {
                            e.preventDefault();
                            news.PublishDate = $('#newnewsdate').val();
                            $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 112 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsPage.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                                },
                                async: false,
                                url: ""/news/id="" + news.Id + ""/update"",
                                data: JSON.stringify(news),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                    window.location.reload();
                                },
                                error: function (data) {
                                    $('#changenewsdateinfo').html(""<span style='color: crimson;'>Failed to update date</span>"");
                                }
                            });
                        });
                    });

                    $('#changenewsdescbtn').click(function () {
                        $('.changenewsdescli').append('<h5 id=""changenewsdescinfo""></h5><form id=""changenewsdesc-form""><textarea id=""newnewsdesc"" style=""font-size: 15px;""></textarea>' +
");
            WriteLiteral(@"                            '<br><br><button type=""submit"" class=""signout hoverbtn"" style=""background-color: dodgerblue"">Отправить</button></form>');
                        $('#changenewsdesc-form').submit(function (e) {
                            e.preventDefault();
                            news.Description = $('#newnewsdesc').val();
                            $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 137 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsPage.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                                },
                                async: false,
                                url: ""/news/id="" + news.Id + ""/update"",
                                data: JSON.stringify(news),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                    window.location.reload();
                                },
                                error: function (data) {
                                    $('#changenewsdescinfo').html(""<span style='color: crimson;'>Failed to update description</span>"");
                                }
                            });
                        });
                    });
    });
</script>
<div class=""newsblock"">
");
#nullable restore
#line 155 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsPage.cshtml"
     if (HttpContextAccessor.HttpContext.Session.GetString("Role") == "admin")
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <form id=""deletenews-form""><button type=""submit"" style=""border: none; background: none; cursor: pointer; font-size: 2em;""><i class=""fas fa-trash-alt""></i></button></form>
        <ul style=""margin-top: 20px;"">
            <li style=""margin-bottom: 15px; list-style-type: none;"" class=""changenewstitleli""><button type=""button"" id=""changenewstitlebtn"" class=""signout hoverbtn"">Изменить заголовок</button></li>
            <li style=""margin-bottom: 15px; list-style-type: none;"" class=""changenewsdateli""><button type=""button"" id=""changenewsdatebtn"" class=""signout hoverbtn"">Изменить дату</button></li>
            <li style=""margin-bottom: 15px; list-style-type: none;"" class=""changenewsdescli""><button type=""button"" id=""changenewsdescbtn"" class=""signout hoverbtn"">Изменить описание</button></li>
        </ul>
");
#nullable restore
#line 163 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsPage.cshtml"
   }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"newsimgblock\"></div>\r\n    <div class=\"newsdate\"></div>\r\n    <h1></h1>\r\n    <p>\r\n    </p>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
