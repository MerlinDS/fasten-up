using System.Collections.Generic;
using FastenUp.Runtime.Bindings;

namespace FastenUp.Runtime.Proxies
{
    public struct DataProxy<T>
    {
        private T _cachedValue;
        private IList<IBindingPoint<T>> _bindings;

        public void Bind(IBindingPoint<T> bindingPoint)
        {
            _bindings ??= new List<IBindingPoint<T>>();
            _bindings.Add(bindingPoint);
            _cachedValue = bindingPoint.Value;
        }

        public void Unbind(IBindingPoint<T> bindingPoint)
        {
            _bindings?.Remove(bindingPoint);
            if (_bindings is null || _bindings.Count == 0)
            {
                _cachedValue = default;
                return;
            }
            _cachedValue = _bindings[0].Value;
        }

        public T Value
        {
            get => _cachedValue;
            set
            {
                _cachedValue = value;
                if (_bindings is null || _bindings.Count == 0)
                    return;

                foreach (var binding in _bindings)
                    binding.Value = value;
            }
        }
    }
}