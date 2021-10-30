#pragma checksum "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\Pages\NumberInput.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "809d78c5a10cc5f4cea7d126002c6c5d973546ee"
// <auto-generated/>
#pragma warning disable 1591
namespace WebGL_Playground_Site.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using WebGL_Playground_Site;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using WebGL_Playground_Site.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using Blazor.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\_Imports.razor"
using Blazor.Extensions.Canvas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\Pages\NumberInput.razor"
using System.Globalization;

#line default
#line hidden
#nullable disable
    public partial class NumberInput : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "textarea");
            __builder.AddAttribute(1, "placeholder", 
#nullable restore
#line 3 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\Pages\NumberInput.razor"
                                                                  PlaceHolder

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(2, "style", "resize: none");
            __builder.AddAttribute(3, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 3 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\Pages\NumberInput.razor"
                 InputString

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => InputString = __value, InputString));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 5 "D:\Documents\PROJECTS\Blazor\WebGL_Playground\WebGL_Playground\WebGL_Playground_Site\Pages\NumberInput.razor"
       
    [Parameter]
    public double? DefaultValue { get; set; } = null;
    public double Value { get; set; } = 0;
    [Parameter]
    public string PlaceHolder { get; set; } = "Type number here...";
    [Parameter]
    public bool IntegerNumber { get; set; } = false;

    private bool inputStringIsEmpty = true;
    private bool showTrailingZeroes = false;
    private bool showMinus = false;

    private string FormatOutput(double d) {
        return d.ToString() + (showTrailingZeroes ? "." : "");
    }

    private string InputString {
        get {
            if(showMinus) {
                return "-";
            }
            if(inputStringIsEmpty) {
                return "";
            }
            return FormatOutput(Value);
        }
        set {
            if(value == "-") {
                showMinus = true;
            } else if(double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double v)) {
                if(IntegerNumber) {
                    v = (int)v;
                } else {
                    showTrailingZeroes = value.Last() == '.';
                }
                Value = v;
                inputStringIsEmpty = false;
                showMinus = false;
            } else if(String.IsNullOrEmpty(value)) {
                Value = 0;
                showMinus = showTrailingZeroes = false;
                inputStringIsEmpty = true;
            }
        }
    }

    protected override void OnParametersSet() {
        if(DefaultValue != null) {
            Value = (double)DefaultValue;
            inputStringIsEmpty = false;
        }
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
