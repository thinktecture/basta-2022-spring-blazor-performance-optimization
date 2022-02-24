using BlazorPerformance.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorPerformance.Client.Conferences
{
    public partial class ConferenceRow
    {
        [Parameter] public Conference Conference { get; set; } = default!;

        private int _count;
        private string _hightlightClass = string.Empty;
        private bool _shouldRender = true;

        protected override void OnInitialized()
        {
            _count = Conference?.VisitorsCount ?? 0;
            base.OnInitialized();
        }

        protected override bool ShouldRender() => _shouldRender;

        protected override void OnParametersSet()
        {
            if (_count != Conference.VisitorsCount)
            {
                _hightlightClass = "highlight";
                Console.WriteLine($"Conference with id {Conference.Id} will be rendered.");
                _count = Conference.VisitorsCount;
                _shouldRender = true;
            }
            else if (!string.IsNullOrWhiteSpace(_hightlightClass))
            {
                _hightlightClass = string.Empty;
                _shouldRender = true;
            }
            else
            {
                _shouldRender = false;
            }

            base.OnParametersSet();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine($"Conference rendered: {Conference.Id}");
            base.OnAfterRender(firstRender);
        }
    }
}