using Microsoft.AspNetCore.Components;

namespace BlazorPerformance.Client.Components.Forms
{
    public partial class HandleEventInputText // : IHandleEvent
    {
        [Parameter] public string Id { get; set; } = string.Empty;
        [Parameter] public string Label { get; set; } = string.Empty;

        private string? _renderMessage;
        private int _renderCount = 0;

        protected override void OnParametersSet()
        {
            Console.WriteLine($"{GetType().Name} | OnParametersSet");
            base.OnParametersSet();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _renderCount++;
                _renderMessage = firstRender ? $"{typeof(HandleEventInputText).Name}.{Label}: Rendered by first Render" : $"{typeof(HandleEventInputText).Name}.{Label}: Render by ShouldRender";
                _renderMessage = $"{_renderMessage}. RenderCount: {_renderCount} times.";
                Console.WriteLine(_renderMessage);
            }
            base.OnAfterRender(firstRender);
        }

        private void HandleOnInput(string value)
        {
            CurrentValue = value;
        }

        //Task IHandleEvent.HandleEventAsync(
        //    EventCallbackWorkItem callback, object? arg) => callback.InvokeAsync(arg);

        protected override bool TryParseValueFromString(string value, out string result,
            out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = string.Empty;
            return true;
        }
    }
}