using BlazorPerformance.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorPerformance.Client.Speakers
{
    public partial class SpeakerRow
    {
        [Parameter] public Speaker Speaker { get; set; } = default!;

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine($"Speaker rendered: {Speaker.Id}");
            base.OnAfterRender(firstRender);
        }
    }
}