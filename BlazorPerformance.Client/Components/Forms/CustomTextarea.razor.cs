﻿using Microsoft.AspNetCore.Components;
using System;

namespace BlazorPerformance.Client.Components.Forms
{
    public partial class CustomTextarea
    {
        [Parameter] public string Label { get; set; }

        private string? _renderMessage;
        private int _renderCount = 0;

        #region Optimierung
        //private int _valueHashCode;
        //private bool _shouldRender;

        //protected override bool ShouldRender() => _shouldRender;

        //protected override void OnParametersSet()
        //{
        //    Console.WriteLine($"{typeof(CustomTextarea).Name}.{Label} OnParametersSet");
        //    if (PreventRendering)
        //    {
        //        var lastHashCode = _valueHashCode;
        //        _valueHashCode = Value?.GetHashCode() ?? 0;
        //        var shouldRender = _valueHashCode != lastHashCode;
        //        Console.WriteLine($"OnParametersSet called. Returns: {shouldRender}");
        //        _shouldRender = shouldRender;
        //    }
        //    base.OnParametersSet();
        //}

        #endregion

        protected override void OnParametersSet()
        {
            Console.WriteLine($"{GetType().Name} | OnParametersSet");
            base.OnParametersSet();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
            {
                _renderCount++;
                _renderMessage = firstRender ? $"{typeof(CustomTextarea).Name}.{Label}: Rendered by first Render" : $"{typeof(CustomTextarea).Name}.{Label}: Render by ShouldRender";
                _renderMessage = $"{_renderMessage}. RenderCount: {_renderCount} times.";
                Console.WriteLine(_renderMessage);
            }
            base.OnAfterRender(firstRender);
        }

        protected override bool TryParseValueFromString(string value, out string result,
            out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}
