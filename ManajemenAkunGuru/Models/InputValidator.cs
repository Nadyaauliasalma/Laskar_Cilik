namespace ManajemenAkunGuru.Models;

public static class InputValidator
{
    public static bool IsValidUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) return false;
        if (username.Length < 3 || username.Length > 10) return false;

        foreach (char c in username)
        {
            if (!char.IsLetter(c)) return false;
        }

        return true;
    }
}
