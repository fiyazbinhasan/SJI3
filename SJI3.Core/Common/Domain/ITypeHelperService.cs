using System.Reflection;

namespace SJI3.Core.Common.Domain;

public interface ITypeHelperService
{
    bool TypeHasProperties<T>(string fields);

    bool TypeHasProperty<T>(string orderBy);
}

public class TypeHelperService : ITypeHelperService
{
    public bool TypeHasProperties<T>(string fields)
    {
        if (string.IsNullOrWhiteSpace(fields))
        {
            return true;
        }

        var fieldsAfterSplit = fields.Split(',');

        foreach (var field in fieldsAfterSplit)
        {
            var propertyName = field.Trim();

            var propertyInfo = typeof(T)
                .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                return false;
            }
        }

        return true;
    }

    public bool TypeHasProperty<T>(string orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return true;
        }

        var orderByAfterSplit = orderBy.Split(' ');

        var propertyName = orderByAfterSplit[0].Trim();

        var propertyInfo = typeof(T)
            .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (propertyInfo == null)
        {
            return false;
        }

        return true;
    }
}