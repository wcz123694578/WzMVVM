using System;
using System.Collections.Generic;
using System.Text;

namespace WzMVVM.Service
{
    /// <summary>
    /// 对话框结果
    /// </summary>
    public class DialogResult
    {
        /// <summary>
        /// 对话框结果：true=OK, false=Cancel, null=Closed
        /// </summary>
        public bool? Result { get; set; }

        /// <summary>
        /// 附加参数（可以传递数据回来）
        /// </summary>
        public DialogParameters Parameters { get; } = new DialogParameters();
    }
}
