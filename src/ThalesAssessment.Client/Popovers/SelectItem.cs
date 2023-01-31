using System;

namespace ThalesAssessment.Client.Popovers;

public class SelectItem
{
    public string DisplayString { get; init; } = string.Empty;

    public object Item { get; init; } = string.Empty;

    public static SelectItem FromObject<T>(T item, Func<T, string> displayStringSelector)
    {
        return new SelectItem
        {
            DisplayString = displayStringSelector(item),
            Item = item
        };
    }
}
