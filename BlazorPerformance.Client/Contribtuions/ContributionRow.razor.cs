using BlazorPerformance.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorPerformance.Client.Contribtuions
{
    public partial class ContributionRow
    {
        [Parameter] public Contribution Contribution { get; set; }
        [Parameter] public string SearchTerm { get; set; }
        [Parameter] public EventCallback ItemClicked { get; set; }

        private void OnItemClicked()
        {
            ItemClicked.InvokeAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine($"Rendered conribution {Contribution?.Id}");
            base.OnAfterRender(firstRender);
        }
    }
}