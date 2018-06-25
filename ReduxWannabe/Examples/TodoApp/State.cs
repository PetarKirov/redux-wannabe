using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ReduxWannabe.Examples.TodoApp.State
{
    public class ApplicationState
    {
        // The minimal application state:
        public ImmutableArray<Todo> AllTodos { get; }
        public TodosFilter Filter { get; }

        // Misc "ViewModel" properties
        // (can be derived from the previous two):
        public IEnumerable<Todo> FilteredTodos { get; }
        public bool CompleteAllIsChecked { get; }
        public bool CompleteAllIsVisible { get; }
        public bool ClearTodosIsVisible { get; }
        public string ActiveTodosCounterMessage { get; }
        public bool AreFiltersVisible { get; }

        public ApplicationState(
            ImmutableArray<Todo> allTodos,
            TodosFilter filter,
            IEnumerable<Todo> filteredTodos,
            bool completeAllIsChecked,
            bool completeAllIsVisible,
            bool clearTodosIsVisible,
            string activeTodosCounterMessage,
            bool areFiltersVisible)
        {
            AllTodos = allTodos;
            Filter = filter;
            FilteredTodos = filteredTodos;
            CompleteAllIsChecked = completeAllIsChecked;
            CompleteAllIsVisible = completeAllIsVisible;
            ClearTodosIsVisible = clearTodosIsVisible;
            ActiveTodosCounterMessage = activeTodosCounterMessage;
            AreFiltersVisible = areFiltersVisible;
        }

        public static ApplicationState Initial { get; } = new ApplicationState(
            ImmutableArray.Create<Todo>(),
            TodosFilter.All,
            Enumerable.Empty<Todo>(),
            false,
            false,
            false,
            string.Empty,
            false);
    }

    public struct Todo
    {
        public string Text { get; }
        public bool IsCompleted { get; }
        public Guid Id { get; }

        public Todo(string text, bool isCompleted, Guid id)
        {
            Text = text;
            IsCompleted = isCompleted;
            Id = id;
        }
    }

    public enum TodosFilter
    {
        All,
        InProgress,
        Completed
    }
}
