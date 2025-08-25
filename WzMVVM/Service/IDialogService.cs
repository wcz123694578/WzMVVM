using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WzMVVM.Service
{
    /// <summary>
    /// 对话框服务接口
    /// </summary>
    public interface IDialogService
    {
        DialogResult ShowDialog<TViewModel>(DialogParameters parameters, TViewModel viewModel) where TViewModel : class;
    }
}
