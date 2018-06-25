using System;
using System.Collections.Immutable;

using ReduxWannabe.Core;
using ReduxWannabe.Examples.TodoApp.Actions;
using ReduxWannabe.Examples.TodoApp.State;
using static ReduxWannabe.Examples.TodoApp.Reducers;

namespace ReduxWannabe.Examples.TodoApp
{
    public partial class TodoAppMainView
    {
        public TodoAppMainView() => InitializeComponent();
    }

    public class TodoDataContext
    {
        public IStore<ApplicationState> Store { get; } =
            new Store<ApplicationState>(
                ReduceApplication,
                ApplicationState.Initial);

        public RXParamCommand<string> AddTodoCmd { get; }
        public RXParamCommand<Guid> DeleteTodoCmd { get; }
        public RXParamCommand<Guid> CompleteTodoCmd { get; }
        public RXParamCommand<bool> CompleteAllTodosCmd { get; }
        public RXBasicCommand ClearCompletedTodosCmd { get; }
        public RXParamCommand<TodosFilter> FilterTodosCmd { get; }

        public TodoDataContext()
        {
            AddTodoCmd = new RXParamCommand<string>(txt =>
                Store.Dispatch(new AddTodoAction { Text = txt }));

            DeleteTodoCmd = new RXParamCommand<Guid>(id =>
                Store.Dispatch(new DeleteTodoAction { TodoId = id }));

            CompleteTodoCmd = new RXParamCommand<Guid>(id =>
                Store.Dispatch(new CompleteTodoAction { TodoId = id }));

            CompleteAllTodosCmd = new RXParamCommand<bool>(completed =>
                Store.Dispatch(new CompleteAllTodosAction { IsCompleted = completed }));

            ClearCompletedTodosCmd = new RXBasicCommand(() =>
                Store.Dispatch(new ClearCompletedTodosAction()));

            FilterTodosCmd = new RXParamCommand<TodosFilter>(filter =>
                Store.Dispatch(new FilterTodosAction { Filter = filter }));
        }
    }
}
