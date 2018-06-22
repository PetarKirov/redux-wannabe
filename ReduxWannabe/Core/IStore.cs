using Reactive.Bindings;

namespace ReduxWannabe.Core
{
    public interface IAction { }

    public delegate TAppState Reducer<TAppState>(TAppState oldState, IAction action);

    public interface IStore<TAppState> : IReadOnlyReactiveProperty<TAppState>
    {
        void Dispatch(IAction action);
    }

    public class Store<TAppState> : ReactiveProperty<TAppState>, IStore<TAppState>
    {
        private readonly Reducer<TAppState> _reducer;

        public Store(Reducer<TAppState> reducer, TAppState initialState = default)
            : base(initialState)
        {
            _reducer = reducer;
        }

        public void Dispatch(IAction action)
        {
            Value = _reducer(Value, action);
        }
    }
}
