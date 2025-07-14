using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Entities;

public class Location : ILocation
{
    public Guid Id { get; }
    public string Description { get; }

    public Location(Guid id, string description)
    {
        // utilizado pelos visitors, por isso não precisa de verificações       

        Id = id;
        Description = description;
    }

    public Location(string description)
    {
        if (!IsValidDescription(description))
            throw new ArgumentException("Description must be a valid URL or a physical location (4-20 alphanumeric characters).");

        Id = Guid.NewGuid();
        Description = description;
    }

    private static bool IsValidDescription(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        var validUrl = Uri.TryCreate(input, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        var validDescription = input.Length is >= 4 and <= 20 && Regex.IsMatch(input, @"^[a-zA-Z0-9 ]+$");

        return validUrl || validDescription;
    }
}
