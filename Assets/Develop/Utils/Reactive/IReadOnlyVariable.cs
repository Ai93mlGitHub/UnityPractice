using System;

namespace Assets.Develop.Utils.Reactive
{
    public interface IReadOnlyVariable<T>
    {
        event Action<T, T> Changed;

        T Value
        {
            get;
        }
    }
}