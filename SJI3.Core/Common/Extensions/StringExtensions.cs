using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SJI3.Core.Common.Extensions;

public static class StringExtensions
{
    public static IEnumerable<T> ParseStringToCollection<T>(this string value)
    {
        return TryParseStringToCollection<T>(value, out var result) ? result : Array.Empty<T>();
    }

    private static bool TryParseStringToCollection<T>(string value, out IEnumerable<T> result)
    {
        try
        {
            var collection = JsonSerializer.Deserialize<T[]>(value, new JsonSerializerOptions());
            result = collection;
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }
}