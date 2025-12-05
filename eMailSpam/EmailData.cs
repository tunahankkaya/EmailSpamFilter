using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eMailSpam;

public class EmailData
{
    [JsonPropertyName("Unnamed: 0")]
    public int Id { get; set; }
    [JsonPropertyName("label_num")]
    public int IsThisSpam { get; set; }
    [JsonPropertyName("text")]
    public string EmailContent { get; set; } = string.Empty;
}
