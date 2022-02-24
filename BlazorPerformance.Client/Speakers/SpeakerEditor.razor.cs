using BlazorPerformance.Client.Services;
using BlazorPerformance.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorPerformance.Client.Speakers
{
    public partial class SpeakerEditor
    {
        [Inject] public DataService<Speaker> ContributionsService { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public int Id { get; set; }
        [Parameter] public bool PreventRendering { get; set; }

        private Speaker? _speaker;

        protected override async Task OnInitializedAsync()
        {
            _speaker = await ContributionsService.GetItemAsync(Id, CancellationToken.None);
            await base.OnInitializedAsync();
        }

        private async Task Submit()
        {
            if (_speaker != null)
            {
                await ContributionsService.UpdateItemAsync(_speaker);
                MudDialog.Close(DialogResult.Ok(_speaker));
            }
        }
        void Cancel() => MudDialog.Cancel();
    }
}