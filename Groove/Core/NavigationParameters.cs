namespace Groove.Core;

public class NavigationParameters : Dictionary<string, object>, INavigationParameters
{
    public bool TryGetValue<T>(string key, out T? value)
    {
        if (base.TryGetValue(key, out var val))
        {
            if (val is T castVal)
            {
                value = castVal;
                return true;
            }

            value = default;
            return false;
        }
        
        value = default;
        return false;
    }
}