using System.Collections;

namespace Groove.Core;

public interface INavigationParameters : IDictionary
{
    public bool TryGetValue<T>(string key, out T? value);
}