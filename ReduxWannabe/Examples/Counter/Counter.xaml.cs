using ReduxWannabe.Core;

namespace ReduxWannabe.Examples.CounterApp
{
    public sealed partial class CounterAppMainView
    {
        public CounterAppMainView() => InitializeComponent();
    }

    public class CounterDataContext
    {
        public static IStore<int> Store { get; } = new Store<int>(Counter, 0);

        public RXBasicCommand IncCmd { get; } = new RXBasicCommand(() => Store.Dispatch(Inc.Instance));
        public RXBasicCommand DecCmd { get; } = new RXBasicCommand(() => Store.Dispatch(Dec.Instance));

        public class Inc : IAction { public static Inc Instance = new Inc(); }
        public class Dec : IAction { public static Dec Instance = new Dec(); }

        public static int Counter(int state, IAction action)
        {
            switch (action)
            {
                case Inc i: return state + 1;
                case Dec d: return state - 1;
                default: return state;
            }
        }
    }
}
