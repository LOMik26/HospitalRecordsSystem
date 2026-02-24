using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordUtils
{
    public static string HashPassword(string password)
    {
        // For the educational project we store plaintext passwords.
        // WARNING: This is insecure and only for learning purposes.
        return password ?? string.Empty;
    }

    public static bool Verify(string password, string storedHash)
    {
        return HashPassword(password) == storedHash;
    }
}