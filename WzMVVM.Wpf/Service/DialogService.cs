using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WzMVVM.Service;

namespace WzMVVM.Wpf.Service
{
    public class DialogService : IDialogService
    {
        private readonly IServiceProvider _provider;
        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        public DialogService(IServiceProvider provider)
        {
            _provider = provider;
        }

        // 建立 VM 和 View 的映射
        public void Register<TView, TViewModel>()
            where TView : Window
            where TViewModel : class
        {
            _mappings[typeof(TViewModel)] = typeof(TView);
        }

        public DialogResult ShowDialog<TViewModel>(DialogParameters parameters, TViewModel viewModel) where TViewModel : class
        {
            if (!_mappings.TryGetValue(typeof(TViewModel), out var viewType))
                throw new InvalidOperationException($"未注册 ViewModel {typeof(TViewModel).Name} 的对应窗口");

            var window = (Window)Activator.CreateInstance(viewType);
            window.DataContext = viewModel;

            if (viewModel is IDialogAware aware)
                aware.OnDialogOpened(parameters);

            bool? result = window.ShowDialog();

            if (viewModel is IDialogAware aware2)
            {
                var dialogResult = aware2.OnDialogClosed();
                dialogResult.Result = result;
                return dialogResult;
            }

            return new DialogResult { Result = result };
        }
    }
}
