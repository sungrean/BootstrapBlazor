﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TabContent 组件
    /// </summary>
    public class TabHeaderContent : ComponentBase
    {
        /// <summary>
        /// 获得/设置 点击 TabItem 时的回调委托
        /// </summary>
        [Parameter]
        public Action<TabItem>? OnClick { get; set; }

        /// <summary>
        /// 获得/设置 所属 Tab 实例
        /// </summary>
        [CascadingParameter]
        protected TabBase? TabSet { get; set; }

        /// <summary>
        /// OnAfterRender
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (!firstRender) TabSet?.ReActiveTab();
        }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (TabSet != null)
            {
                if (!TabSet.Items.Any(t => t.IsActive))
                {
                    var t = TabSet.Items.FirstOrDefault();
                    t?.SetActive(true);
                }

                foreach (var item in TabSet.Items)
                {
                    var index = 0;
                    builder.OpenElement(index++, "div");
                    builder.SetKey(item);
                    builder.AddAttribute(index++, "role", "tab");
                    builder.AddAttribute(index++, "tabindex", "-1");
                    builder.AddAttribute(index++, "class", GetClassString(item.IsActive));
                    builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(this, () => OnClick?.Invoke(item)));

                    if (!string.IsNullOrEmpty(item.Icon))
                    {
                        builder.OpenElement(index++, "i");
                        builder.AddAttribute(index++, "class", GetIconClassString(item.Icon));
                        builder.CloseElement();
                    }

                    builder.OpenElement(index++, "span");
                    builder.AddAttribute(index++, "class", "tabs-item-text");
                    builder.AddContent(index++, item.Text);
                    builder.CloseElement(); // end span

                    if (TabSet.ShowClose)
                    {
                        builder.OpenElement(index++, "span");
                        builder.AddAttribute(index++, "class", "tabs-item-close");

                        builder.OpenElement(index++, "i");
                        builder.AddAttribute(index++, "class", "fa fa-close");
                        builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(this, () => TabSet.Remove(item)));
                        builder.AddEventStopPropagationAttribute(index++, "onclick", true);
                        builder.CloseElement();

                        builder.CloseElement(); // end span
                    }

                    builder.CloseElement(); // end div
                }
            }
        }

        private string? GetIconClassString(string icon) => CssBuilder.Default("fa-fw")
            .AddClass(icon)
            .Build();

        private string? GetClassString(bool active) => CssBuilder.Default("tabs-item")
            .AddClass("is-active", active)
            .AddClass("is-closeable", TabSet?.ShowClose ?? false)
            .Build();
    }
}
