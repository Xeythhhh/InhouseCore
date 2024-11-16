
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Api.Components;
public partial class App
{
    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

    private IComponentRenderMode? RenderModeForPage => HttpContext.Request.Path.StartsWithSegments("/Account") ? null
        : new InteractiveWebAssemblyRenderMode(prerender: false);
}