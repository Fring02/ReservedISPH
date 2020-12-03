#pragma checksum "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1f7b5e630b38bdf102f544b0337c4cee8f557197"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Header), @"mvc.1.0.razor-page", @"/Views/Home/Header.cshtml")]
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
#line 2 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f7b5e630b38bdf102f544b0337c4cee8f557197", @"/Views/Home/Header.cshtml")]
    public class Views_Home_Header : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<header class=""header"">
    <nav class=""navbar navbar-expand-lg navbar-light bg-light"">
        <a href=""/home/main"">
            <img src=""https://thumb.tildacdn.com/tild3366-6632-4135-a433-623932303730/-/resize/240x/-/format/webp/Astana_IT_University.png"" 
                 alt=""Responsive image"" class=""header-img""></a>
        <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#navbarSupportedContent""
                aria-controls=""navbarSupportedContent"" aria-expanded=""false"" aria-label=""Toggle navigation"">
            <span class=""navbar-toggler-icon""></span>
        </button>
        <div class=""collapse navbar-collapse"" id=""navbarSupportedContent"">
            <ul class=""navbar-nav mr-auto"">
                <li class=""nav-item"">
                    <a class=""nav-link nav-option"" href=""#"">For students</a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link nav-option"" href=""#"">For employers</a>
                </");
            WriteLiteral("li>\r\n                <li class=\"nav-item\">\r\n                    <a class=\"nav-link nav-option\" href=\"#\">Companies</a>\r\n                </li>\r\n            </ul>\r\n");
#nullable restore
#line 25 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
              
                var session = HttpContextAccessor.HttpContext.Session;
                var notAvailableToken = string.IsNullOrEmpty(session.GetString("Token"));
                var loc = session.GetString("Role") + "s";
                if (notAvailableToken)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""user-buttons"">
                        <button type=""button"" class=""btn btn-outline-primary"" data-toggle=""modal"" data-target=""#signupModal"">
                            Sign up
                        </button>
                        <button type=""button"" class=""btn btn-primary"" data-toggle=""modal"" data-target=""#signinModal"">
                            Sign in
                        </button>
                    </div>
");
#nullable restore
#line 39 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
                }
                else
                {
                    var isAdmin = session.GetString("Role") == "admin";
                    var isEmployer = session.GetString("Role") == "employer";

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"user-buttons\">\r\n");
#nullable restore
#line 45 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
                                  
                                    if (isAdmin)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <button type=\"button\" class=\"btn btn-warning addarticlebtn\">Add article</button>\r\n                                        <button type=\"button\" class=\"btn btn-warning addnewsbtn\">Add news</button>\r\n");
#nullable restore
#line 50 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
                                    }
                                    else if (isEmployer)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <button type=\"button\" class=\"btn btn-info\" data-toggle=\"modal\" data-target=\"#chatModal\">Chat</button>\r\n");
#nullable restore
#line 54 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
                                    }
                                

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <a href=""/home/profile"">
                                    <img src=""https://cdn4.iconfinder.com/data/icons/small-n-flat/24/user-alt-512.png""
                                         class=""img-fluid profile"">
                                </a>
                                <form method=""post""");
            BeginWriteAttribute("action", " action=\"", 3360, "\"", 3393, 3);
            WriteAttributeValue("", 3369, "/users/", 3369, 7, true);
#nullable restore
#line 60 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
WriteAttributeValue("", 3376, loc, 3376, 4, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3380, "/auth/signout", 3380, 13, true);
            EndWriteAttribute();
            WriteLiteral(" style=\"display: inline; margin-left: 10px;\">\r\n                                    <button type=\"submit\" class=\"btn btn-danger\">Sign out</button>\r\n                                </form>\r\n                            </div>\r\n");
#nullable restore
#line 64 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
                        }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </nav>\r\n</header>\r\n\r\n");
#nullable restore
#line 70 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
Write(await Html.PartialAsync("RegistrationForm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 71 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
Write(await Html.PartialAsync("LoginForm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 72 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
   
    if(session.GetString("Role") == "employer")
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 75 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
   Write(await Html.PartialAsync("ChatApp"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 75 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Header.cshtml"
                                           
    }

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Views_Home_Header> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_Header> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_Header>)PageContext?.ViewData;
        public Views_Home_Header Model => ViewData.Model;
    }
}
#pragma warning restore 1591