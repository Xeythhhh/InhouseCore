using Microsoft.AspNetCore.Components;

namespace WebApp.Extensions;

/// <summary>Provides extension methods for working with <see cref="IEnumerable{T}"/> collections.</summary>
public static class EnumerableExtensions
{
    /// <summary> Maps each element in the collection to a <see cref="RenderFragment"/> using the specified template function.</summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="list">The collection of elements to map.</param>
    /// <param name="template">A function that takes an element of type <typeparamref name="T"/> and returns a <see cref="RenderFragment"/>.</param>
    /// <returns>A <see cref="RenderFragment"/> that renders the mapped elements.</returns>
    public static RenderFragment Map<T>(this IEnumerable<T> list, Func<T, RenderFragment> template)
        where T : class =>
        builder =>
        {
            int sequence = 0;
            foreach (T? item in list)
            {
                builder.AddContent(sequence++, template(item));
            }
        };

    /// <summary> Maps the elements in the collection to a <see cref="RenderFragment"/> if any elements exist; otherwise, it renders a fallback fragment.</summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="list">The collection of elements to map.</param>
    /// <param name="template">A function that takes an element of type <typeparamref name="T"/> and returns a <see cref="RenderFragment"/>.</param>
    /// <param name="fallback">A <see cref="RenderFragment"/> to render when the collection is empty.</param>
    /// <returns>A <see cref="RenderFragment"/> that either renders the mapped elements or the fallback fragment if the collection is empty.</returns>
    public static RenderFragment MapIfAny<T>(this IEnumerable<T> list, Func<T, RenderFragment> template, RenderFragment fallback)
        where T : class =>
        builder =>
        {
            if (list.Any())
            {
                RenderFragment fragment = list.Map(template);
                fragment(builder);
            }
            else
            {
                builder.AddContent(0, fallback);
            }
        };
}
