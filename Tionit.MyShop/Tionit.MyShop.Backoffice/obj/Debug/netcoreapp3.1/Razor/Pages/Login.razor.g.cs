#pragma checksum "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\Pages\Login.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7d10ed6b5d0be7804a3769bc4eaaefeb32169cf9"
// <auto-generated/>
#pragma warning disable 1591
namespace Tionit.ShopOnline.Backoffice.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Tionit.ShopOnline.Backoffice;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Tionit.ShopOnline.Backoffice.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Telerik.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Telerik.Blazor.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\_Imports.razor"
using Messages = Tionit.ShopOnline.Backoffice.InteropServices.Messages;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Login : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "kt-grid kt-grid--ver kt-grid--root");
            __builder.AddMarkupContent(2, "\r\n    ");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "kt-grid kt-grid--hor kt-grid--root  kt-login kt-login--v3 kt-login--signin");
            __builder.AddAttribute(5, "id", "kt_login");
            __builder.AddMarkupContent(6, "\r\n        ");
            __builder.OpenElement(7, "div");
            __builder.AddAttribute(8, "class", "kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor");
            __builder.AddAttribute(9, "style", "background-image: url(\'img/login-bg.jpg\');");
            __builder.AddMarkupContent(10, "\r\n            ");
            __builder.OpenElement(11, "div");
            __builder.AddAttribute(12, "class", "kt-grid__item kt-grid__item--fluid kt-login__wrapper");
            __builder.AddMarkupContent(13, "\r\n                ");
            __builder.OpenElement(14, "div");
            __builder.AddAttribute(15, "class", "kt-login__container");
            __builder.AddMarkupContent(16, "\r\n                    ");
            __builder.AddMarkupContent(17, "<div class=\"kt-login__logo\">\r\n                        <a href=\"#\">\r\n                            <img src=\"img/logo.png\" width=\"300\">\r\n                        </a>\r\n                    </div>\r\n                    ");
            __builder.OpenElement(18, "div");
            __builder.AddAttribute(19, "class", "kt-login__signin");
            __builder.AddMarkupContent(20, "\r\n                        ");
            __builder.AddMarkupContent(21, "<div class=\"kt-login__head\">\r\n                            <h3 class=\"kt-login__title\">Вход в систему</h3>\r\n                        </div>\r\n                        ");
            __builder.OpenElement(22, "form");
            __builder.AddAttribute(23, "class", "kt-form");
            __builder.AddAttribute(24, "onsubmit", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.EventArgs>(this, 
#nullable restore
#line 18 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\Pages\Login.razor"
                                                          LogIn

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(25, "\r\n                            ");
            __builder.OpenElement(26, "div");
            __builder.AddAttribute(27, "class", "input-group");
            __builder.AddMarkupContent(28, "\r\n                                ");
            __builder.OpenElement(29, "input");
            __builder.AddAttribute(30, "class", "form-control");
            __builder.AddAttribute(31, "type", "text");
            __builder.AddAttribute(32, "placeholder", "Логин");
            __builder.AddAttribute(33, "name", "email");
            __builder.AddAttribute(34, "autocomplete", "off");
            __builder.AddAttribute(35, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 20 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\Pages\Login.razor"
                                                     Username

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(36, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Username = __value, Username));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(37, "\r\n                            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n                            ");
            __builder.OpenElement(39, "div");
            __builder.AddAttribute(40, "class", "input-group");
            __builder.AddMarkupContent(41, "\r\n                                ");
            __builder.OpenElement(42, "input");
            __builder.AddAttribute(43, "class", "form-control");
            __builder.AddAttribute(44, "type", "password");
            __builder.AddAttribute(45, "placeholder", "Пароль");
            __builder.AddAttribute(46, "name", "password");
            __builder.AddAttribute(47, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 23 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\Pages\Login.razor"
                                                     Password

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(48, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Password = __value, Password));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(49, "\r\n                            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n                            ");
            __builder.OpenElement(51, "div");
            __builder.AddAttribute(52, "class", "kt-login__actions");
            __builder.AddMarkupContent(53, "\r\n                                ");
            __builder.OpenElement(54, "button");
            __builder.AddAttribute(55, "id", "kt_login_signin_submit");
            __builder.AddAttribute(56, "disabled", 
#nullable restore
#line 26 "C:\Users\Melkonyan\Desktop\Tionit.ShopOnline\Tionit.ShopOnline.Backoffice\Pages\Login.razor"
                                                                                !IsLoginAvailable

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(57, "class", "btn btn-success btn-elevate kt-login__btn-primary");
            __builder.AddMarkupContent(58, "\r\n                                    Войти\r\n                                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(59, "\r\n                            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(60, "\r\n                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(61, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(62, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(63, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(64, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(65, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(66, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591