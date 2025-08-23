using System;
using System.Threading.Tasks;

namespace WzMVVM.Service
{
    /// <summary>
    /// 对话框服务接口
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// 显示消息对话框
        /// </summary>
        DialogResult ShowMessage(string message, string title = "提示", DialogButton button = DialogButton.OK);

        /// <summary>
        /// 异步显示消息对话框
        /// </summary>
        Task<DialogResult> ShowMessageAsync(string message, string title = "提示", DialogButton button = DialogButton.OK);

        /// <summary>
        /// 显示自定义对话框
        /// </summary>
        DialogResult ShowDialog(object dialogContent, string title = null);

        /// <summary>
        /// 显示自定义对话框（泛型版本）
        /// </summary>
        DialogResult ShowDialog<T>(T viewModel, string title = null) where T : class;

        /// <summary>
        /// 异步显示自定义对话框
        /// </summary>
        Task<DialogResult> ShowDialogAsync(object dialogContent, string title = null);

        /// <summary>
        /// 异步显示自定义对话框（泛型版本）
        /// </summary>
        Task<DialogResult> ShowDialogAsync<T>(T viewModel, string title = null) where T : class;

        /// <summary>
        /// 显示自定义窗口
        /// </summary>
        DialogResult ShowCustomDialog(Type windowType, object dataContext = null, string title = null);

        /// <summary>
        /// 注册窗口类型
        /// </summary>
        void RegisterWindow(Type viewModelType, Type windowType);

        /// <summary>
        /// 注册窗口类型（泛型版本）
        /// </summary>
        void RegisterWindow<TViewModel, TWindow>() where TWindow : Window;
    }
}
