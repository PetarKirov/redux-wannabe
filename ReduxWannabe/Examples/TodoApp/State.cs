using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ReduxWannabe.Examples.TodoApp.State
{
    public class ApplicationState
    {
        public ApplicationState(TodosFilter filter, ImmutableArray<Todo> todos)
        {
            Filter = filter;
            AllTodos = todos;
            FilteredTodos = filter == TodosFilter.All ?
                AllTodos :
                AllTodos.Where(t => t.IsCompleted == (filter == TodosFilter.Completed));
            CompleteAllIsChecked = AllTodos.All(x => x.IsCompleted);
            CompleteAllIsVisible = AllTodos.Any();
            ClearTodosIsVisible = AllTodos.Any(todo => todo.IsCompleted);
            AreFiltersVisible = AllTodos.Any();

            var count = AllTodos.Count(todo => !todo.IsCompleted);
            var items = count == 1 ? "item" : "items";
            ActiveTodosCounterMessage = $"{count} {items} left";
        }

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
    }

    public class Todo
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
