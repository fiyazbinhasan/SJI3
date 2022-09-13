using System;
using System.Text.Json.Serialization;

namespace SJI3.Infrastructure.AntiCorruption.HttpClients.Requests;

public class DemoPostBody
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}