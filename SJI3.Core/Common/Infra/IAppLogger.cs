using System;

namespace SJI3.Core.Common.Infra;

public interface IAppLogger<T>
{
    void LogInformation(string message, params object[] args);
    void LogWarning(Exception exception, string message, params object[] args);
    void LogError(Exception exception, string message, params object[] args);
}