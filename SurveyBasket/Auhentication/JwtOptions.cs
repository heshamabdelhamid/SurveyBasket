using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Auhentication;

public class JwtOptions
{
    public static string SectionName = "Jwt";

    [Required]
    public string Key { get; init; } = string.Empty;

    [Required]
    public string Issuer { get; init; } = string.Empty;
    
    [Required]
    public string Audience { get; init; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Expiry minutes must be between 1 and 1440 (24 hours).")]
    public int ExpiryMinutes { get; init; }
}