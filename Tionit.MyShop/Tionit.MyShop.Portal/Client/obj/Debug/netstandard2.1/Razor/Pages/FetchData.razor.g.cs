#pragma checksum "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "16de128eb43dd02c390a0958edc45630e7e4f202"
// <auto-generated/>
#pragma warning disable 1591
namespace Tionit.MyShop.Portal.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using Tionit.MyShop.Portal.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\_Imports.razor"
using Tionit.MyShop.Portal.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
using Tionit.MyShop.Portal.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/fetchdata")]
    public partial class FetchData : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Weather forecast</h1>\r\n\r\n");
            __builder.AddMarkupContent(1, "<p>This component demonstrates fetching data from the server.</p>\r\n\r\n");
#nullable restore
#line 9 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
 if (forecasts == null)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(2, "    ");
            __builder.AddMarkupContent(3, "<p><em>Loading...</em></p>\r\n");
#nullable restore
#line 12 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(4, "    ");
            __builder.OpenElement(5, "table");
            __builder.AddAttribute(6, "class", "table");
            __builder.AddMarkupContent(7, "\r\n        ");
            __builder.AddMarkupContent(8, "<thead>\r\n            <tr>\r\n                <th>Date</th>\r\n                <th>Temp. (C)</th>\r\n                <th>Temp. (F)</th>\r\n                <th>Summary</th>\r\n            </tr>\r\n        </thead>\r\n        ");
            __builder.OpenElement(9, "tbody");
            __builder.AddMarkupContent(10, "\r\n");
#nullable restore
#line 25 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
             foreach (var forecast in forecasts)
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(11, "                ");
            __builder.OpenElement(12, "tr");
            __builder.AddMarkupContent(13, "\r\n                    ");
            __builder.OpenElement(14, "td");
            __builder.AddContent(15, 
#nullable restore
#line 28 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
                         forecast.Date.ToShortDateString()

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n                    ");
            __builder.OpenElement(17, "td");
            __builder.AddContent(18, 
#nullable restore
#line 29 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
                         forecast.TemperatureC

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n                    ");
            __builder.OpenElement(20, "td");
            __builder.AddContent(21, 
#nullable restore
#line 30 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
                         forecast.TemperatureF

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n                    ");
            __builder.OpenElement(23, "td");
            __builder.AddContent(24, 
#nullable restore
#line 31 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
                         forecast.Summary

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\r\n");
#nullable restore
#line 33 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(27, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(28, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n");
#nullable restore
#line 36 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 38 "C:\Users\Melkonyan\source\repos\MyShop\Tionit.MyShop\Tionit.MyShop.Portal\Client\Pages\FetchData.razor"
       
    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
