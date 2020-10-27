#pragma checksum "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b71405669e6ea8c933f62f9a2452e419f7487f23"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_NewsForm), @"mvc.1.0.razor-page", @"/Views/Home/NewsForm.cshtml")]
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
#line 2 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\NewsForm.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b71405669e6ea8c933f62f9a2452e419f7487f23", @"/Views/Home/NewsForm.cshtml")]
    public class Views_Home_NewsForm : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js\"></script>\r\n<style>\r\n    ");
            WriteLiteral(@"@media screen and (max-width: 600px) {
        .col-25, .col-75, input[type=submit] {
            width: 100%;
            margin-top: 0;
        }
    }

    #addads input {
        font-size: 18px;
    }

    .adsfield {
        width: 100%;
        padding: 12px;
        border: 1px solid #ccc;
        border-radius: 4px;
        resize: vertical;
    }

    label {
        padding: 12px 12px 12px 0;
        display: inline-block;
    }

    #adssubmit {
        background-color: dodgerblue;
        color: white;
        padding: 12px 20px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        float: right;
    }

        #adssubmit:hover {
            background-color: mediumspringgreen;
        }

    .addnews {
        border-radius: 5px;
        border: 2px solid black;
        background: white;
        color: black;
        padding: 20px;
    }

    .col-25 {
        float: left;
        width: 25%;
        margin-top: 6px;");
            WriteLiteral(@"
    }

    .col-75 {
        float: left;
        width: 75%;
        margin-top: 6px;
    }

    /* Clear floats after the columns */
    .row:after {
        content: """";
        display: table;
        clear: both;
    }

    input::-webkit-outer-spin-button,
    input::-webkit-inner-spin-button {
        -webkit-appearance: none;
        margin: 0;
    }

    /* Firefox */
    #publishDate {
        -moz-appearance: textfield;
    }
</style>
<script>
    $(document).ready(function () {
        $('#addnewsblock').hide();
        $('#addNewsbtn').click(function () {
            $('#addnewsblock').show();
            $('#addarticleblock').hide();
        });
    });

</script>
<div id=""addnewsblock"" class=""addnews"">
    <h1>Новые новости</h1>
    <form method=""post"" action=""/news/add"" id=""addnewsform"" enctype=""multipart/form-data"">
        <h4 id=""adserror"" style=""color: crimson; font-weight: normal;""></h4>
        <div class=""row"">
            <div class=""col-25"">");
            WriteLiteral(@"
                <label for=""title"">Заголовок</label>
            </div>
            <div class=""col-75"">
                <input type=""text"" id=""newstitle"" name=""Title"" placeholder=""Ваш заголовок..."" class=""adsfield"">
            </div>
        </div>
        <div class=""row"">
            <div class=""col-25"">
                <label for=""salary"">Дата публикации</label>
            </div>
            <div class=""col-75"">
                <input type=""date"" id=""newspublishDate"" name=""PublishDate"" placeholder=""Ваша дата публикации..."" class=""adsfield"">
            </div>
        </div>
        <div class=""row"">
            <div class=""col-25"">
                <label for=""description"">Описание</label>
            </div>
            <div class=""col-75"">
                <textarea id=""newsdescription"" class=""adsfield"" name=""Description"" placeholder=""Разместите здесь описание новостей..."" style=""height:200px""></textarea>
            </div>
        </div>
        <div class=""row"">
            <div");
            WriteLiteral(@" class=""col-25"">
                <label for=""description"">Картинка</label>
            </div>
            <div class=""col-75"">
                <input type=""file"" name=""File""/>
            </div>
        </div>
        <div class=""row"">
            <input type=""submit"" value=""Submit"" id=""adssubmit"">
        </div>
    </form>
</div>
<script>
");
            WriteLiteral("</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Views_Home_NewsForm> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_NewsForm> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_NewsForm>)PageContext?.ViewData;
        public Views_Home_NewsForm Model => ViewData.Model;
    }
}
#pragma warning restore 1591
