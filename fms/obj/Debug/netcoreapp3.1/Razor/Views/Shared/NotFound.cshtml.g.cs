#pragma checksum "E:\WDDN-PROJECT\fms\fms\Views\Shared\NotFound.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "26c28903826d0e49b07f88b9cf116ee718be0d96"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_NotFound), @"mvc.1.0.view", @"/Views/Shared/NotFound.cshtml")]
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
#line 1 "E:\WDDN-PROJECT\fms\fms\Views\_ViewImports.cshtml"
using fms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\WDDN-PROJECT\fms\fms\Views\_ViewImports.cshtml"
using fms.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26c28903826d0e49b07f88b9cf116ee718be0d96", @"/Views/Shared/NotFound.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3707bf8577d6690eda379682e04effd996a011e2", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_NotFound : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\WDDN-PROJECT\fms\fms\Views\Shared\NotFound.cshtml"
  
    ViewBag.Title = "Not Found";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 5 "E:\WDDN-PROJECT\fms\fms\Views\Shared\NotFound.cshtml"
Write(ViewBag.ErrorMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
        }
        #pragma warning restore 1998
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
