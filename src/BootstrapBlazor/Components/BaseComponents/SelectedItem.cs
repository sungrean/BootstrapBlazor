﻿namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 选项类
    /// </summary>
    public class SelectedItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectedItem() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectedItem (string value, string text) => (Value, Text) = (value, text);

        /// <summary>
        /// 获得/设置 显示名称
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// 获得/设置 选项值
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否选中
        /// </summary>
        public bool Active { get; set; }
    }
}
