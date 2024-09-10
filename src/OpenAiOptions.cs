using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

public class OpenAiOptions
{
    [Required]
    [Url]
    public string Endpoint { get; set; } = default!;

    [Required]
    public string ApiKey { get; set; } = default!;

    [Required]
    public string Deployment { get; set; } = default!;
}