using System;
using System.Collections.Generic;
using System.Text;

namespace WzMVVM.Service
{
    public class DialogParameters
    {
        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

        // 添加参数
        public void Add(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            _parameters[key] = value;
        }

        // 获取参数值（强类型）
        public T GetValue<T>(string key)
        {
            if (!_parameters.TryGetValue(key, out object value))
                throw new KeyNotFoundException($"Key {key} not found");

            return (T)value;
        }

        // 尝试获取参数值（安全方式）
        public bool TryGetValue<T>(string key, out T value)
        {
            if (_parameters.TryGetValue(key, out object objValue) && objValue is T)
            {
                value = (T)objValue;
                return true;
            }

            value = default;
            return false;
        }

        // 检查参数是否存在
        public bool ContainsKey(string key) => _parameters.ContainsKey(key);

    }
}
