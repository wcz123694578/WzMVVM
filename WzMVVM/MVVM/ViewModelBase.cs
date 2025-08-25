using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WzMVVM.MVVM
{
    /// <summary>
    /// ViewModel 的基类，提供了属性变更通知、验证和命令支持
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo, IDisposable
    {
        #region INotifyPropertyChanged 实现

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 触发 PropertyChanged 事件
        /// </summary>
        /// <param name="propertyName">属性名称（通常使用 CallerMemberName 自动获取）</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 设置属性值并在值改变时自动触发 PropertyChanged 事件
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="field">后备字段的引用</param>
        /// <param name="value">新值</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>如果值已更改返回 true，否则返回 false</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region INotifyDataErrorInfo 实现

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get { return _errorsByPropertyName.Any(); }
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                // 返回所有错误
                return _errorsByPropertyName.Values.SelectMany(errors => errors).ToList();
            }

            return _errorsByPropertyName.ContainsKey(propertyName)
                ? _errorsByPropertyName[propertyName]
                : Enumerable.Empty<string>();
        }

        /// <summary>
        /// 添加验证错误
        /// </summary>
        protected void AddError(string errorMessage, [CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;

            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(errorMessage))
            {
                _errorsByPropertyName[propertyName].Add(errorMessage);
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>
        /// 清除指定属性的所有错误
        /// </summary>
        protected void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;

            if (_errorsByPropertyName.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>
        /// 清除所有验证错误
        /// </summary>
        protected void ClearAllErrors()
        {
            var propertyNames = _errorsByPropertyName.Keys.ToList();
            _errorsByPropertyName.Clear();

            foreach (var propertyName in propertyNames)
            {
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>
        /// 触发 ErrorsChanged 事件
        /// </summary>
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            // 错误状态变化可能影响命令的执行能力
            RaisePropertyChanged(nameof(HasErrors));
        }

        #endregion

        #region IDisposable 实现

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // 释放托管资源
                // 例如：取消事件订阅、取消 CancellationTokenSource 等
            }

            // 释放非托管资源（如果有）

            _disposed = true;
        }

        ~ViewModelBase()
        {
            Dispose(false);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 刷新所有属性的 UI 绑定（谨慎使用，性能开销较大）
        /// </summary>
        protected void RefreshAll()
        {
            RaisePropertyChanged(string.Empty);
        }

        #endregion
    }
}
