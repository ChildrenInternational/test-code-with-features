using System;
using System.Text.RegularExpressions;

public class FullNameParser
{
    public static (string FirstName, string LastName) ParseFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Full name cannot be null or empty.", nameof(fullName));
        }

        var nameParts = fullName.Trim().Split(' ');

        if (nameParts.Length < 2)
        {
            throw new ArgumentException("Full name must contain at least a first name and a last name.", nameof(fullName));
        }

        var firstName = nameParts[0];
        var lastName = string.Join(" ", nameParts, 1, nameParts.Length - 1);

        return (firstName, lastName);
    }
}
