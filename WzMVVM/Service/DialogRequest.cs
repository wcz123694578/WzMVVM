using System;
using System.Collections.Generic;
using System.Text;

namespace WzMVVM.Service
{
    /// <summary>
    /// 对话框请求参数
    /// </summary>
    public class DialogRequest
    {
        public string Title { get; set; } = "提示";
        public string Message { get; set; }
        public DialogButton Button { get; set; } = DialogButton.OK;
        public object DialogContent { get; set; }
        public Type DialogType { get; set; }
    }

}
