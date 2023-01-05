using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace SynergyISP.Domain.Helpers;

public static class StringHelper
{
    /// <summary>
    /// Indicates whether a specified string is <see langword="null"/> or an empty string ("").
    /// </summary>
    /// <param name="value">Current string.</param>
    /// <returns>
    ///     <see langword="true"/> if the <paramref name="value"/> is <see langword="null"/> or an empty string (""); otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Indicates whether a specified string is <see langword="null"/>, <seealso cref="string.Empty"/> or consists only of white-space characters; otherwise, <see langword="false"/>.
    /// </summary>
    /// <param name="value">Current string.</param>
    /// <returns>
    ///     <see langword="true"/> if the <paramref name="value"/> is <see langword="null"/> or <see cref="string.Empty"/>,
    ///     or if <paramref name="value"/> consists exclusively of white-space characters.
    /// </returns>
    public static bool IsNullOrWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Determines whether the string and a specified <see cref="string" /> object has the same value.
    /// It will use <seealso cref="StringComparison.OrdinalIgnoreCase"/> while comparing two strings.
    /// Compare strings using ordinal (binary) sort rules and ignoring the case of the
    /// strings being compared.
    /// </summary>
    /// <param name="str">The current string.</param>
    /// <param name="value">The string to compare to this instance.</param>
    /// <returns><see langword="true"/> if the value of the <paramref name="value"/> parameter is the same as this string. otherwise <see langword="false"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the comparison type is not supported.</exception>
    public static bool EqualsOrdinalIgnoreCase(this string str, [NotNullWhen(true)] string? value)
    {
        return str.Equals(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines whether the string and a specified <see cref="string" /> object has the same value.
    /// It will use <see href="StringComparison.InvariantCultureIgnoreCase"/> while comparing two strings.
    ///  Compare strings using culture-sensitive sort rules, the invariant culture, and
    ///  ignoring the case of the strings being compared.
    /// </summary>
    /// <param name="str">The current string.</param>
    /// <param name="value">The string to compare to this instance.</param>
    /// <returns><see langword="true"/> if the value of the <paramref name="value"/> parameter is the same as this string. otherwise <see langword="false"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the comparison type is not supported.</exception>
    public static bool EqualsInvariantCultureIgnoreCase(this string str, [NotNullWhen(true)] string? value)
    {
        return str.Equals(value, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Gets the bytes.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>An array of byte.</returns>
    public static byte[] GetBytes(this string value, Encoding encoding)
    {
        return encoding.GetBytes(value);
    }

    /// <summary>
    /// Computes the hash.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="algorithm">The algorithm.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>A string.</returns>
    public static string ComputeHash(
        this string value,
        HashAlgorithm algorithm,
        Encoding encoding)
    {
        byte[] bytes = value.GetBytes(encoding);
        byte[] hashedBytes = algorithm.ComputeHash(bytes);
        return hashedBytes.GetString();
    }

    /// <summary>
    /// Computes the hash async.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="algorithm">The algorithm.</param>
    /// <param name="encoding">The encoding.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Task.</returns>
    public static async Task<string> ComputeHashAsync(
        this string value,
        HashAlgorithm algorithm,
        Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        using MemoryStream stream = value.GetStream(encoding);
        byte[] hashedBytes = await algorithm.ComputeHashAsync(stream, cancellationToken);
        return hashedBytes.GetString();
    }

    /// <summary>
    /// Gets the stream.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>A MemoryStream.</returns>
    public static MemoryStream GetStream(
        this string value,
        Encoding encoding)
    {
        var bytes = value.GetBytes(encoding);
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// Gets the string.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    /// <returns>A string.</returns>
    public static string GetString(this byte[] bytes)
    {
        StringBuilder hashedInputStringBuilder = new (128);
        foreach (var b in bytes)
        {
            hashedInputStringBuilder.Append(b.ToString("X2"));
        }

        return hashedInputStringBuilder.ToString();
    }

    /// <summary>
    /// Gets the string.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>A string.</returns>
    public static string GetString(this byte[] bytes, Encoding encoding)
    {
        return encoding.GetString(bytes);
    }
}
