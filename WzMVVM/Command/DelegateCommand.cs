using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WzMVVM.Command
{
    /// <summary>
    /// 实现 ICommand 的委托命令
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;
        private bool _isActive;

        /// <summary>
        /// 创建新命令
        /// </summary>
        /// <param name="execute">执行逻辑</param>
        /// <param name="canExecute">判断是否可执行逻辑</param>
        public DelegateCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        public void Execute(object parameter)
        {
            _execute();
        }

        /// <summary>
        /// 触发 CanExecuteChanged 事件
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 命令的可执行状态改变时触发
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 获取或设置命令的活动状态（常用于UI指示）
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnIsActiveChanged();
                }
            }
        }

        /// <summary>
        /// IsActive 属性改变时触发
        /// </summary>
        public event EventHandler? IsActiveChanged;

        protected virtual void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 带参数的泛型 DelegateCommand
    /// </summary>
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool>? _canExecute;
        private bool _isActive;

        /// <summary>
        /// 创建新命令
        /// </summary>
        /// <param name="execute">执行逻辑</param>
        /// <param name="canExecute">判断是否可执行逻辑</param>
        public DelegateCommand(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            if (parameter == null && typeof(T).IsValueType)
                return false;

            return parameter is T typedParameter ? _canExecute(typedParameter) : _canExecute(default(T));
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        public void Execute(object parameter)
        {
            if (parameter is T typedParameter)
            {
                _execute(typedParameter);
            }
            else if (parameter == null && !typeof(T).IsValueType)
            {
                _execute(default(T));
            }
        }

        /// <summary>
        /// 触发 CanExecuteChanged 事件
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 命令的可执行状态改变时触发
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 获取或设置命令的活动状态
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnIsActiveChanged();
                }
            }
        }

        /// <summary>
        /// IsActive 属性改变时触发
        /// </summary>
        public event EventHandler? IsActiveChanged;

        protected virtual void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
