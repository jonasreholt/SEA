using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Model.Structures;

public struct UserId
{
    public string UserName;
    public byte[] PasswordHash;

    public UserId(string userName, string password)
    {
        UserName = userName;
        using var hasher = SHA256.Create();

        PasswordHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public UserId(string userName, byte[] passwordHash)
    {
        UserName = userName;
        PasswordHash = passwordHash;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not UserId other) return false;
        if (this.UserName != other.UserName || this.PasswordHash.Length != other.PasswordHash.Length) return false;

        for (var i = 0; i < PasswordHash.Length; i++)
        {
            if (this.PasswordHash[i] != other.PasswordHash[i])
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserName, PasswordHash);
    }

    private const char Seperator = ':';
    public override string ToString()
    {
        return $"{UserName}{Seperator}{Convert.ToBase64String(PasswordHash)}";
    }

    public static UserId Parse(string str)
    {
        var split = str.Split(Seperator);
        Debug.Assert(split.Length == 2);

        return new UserId(split[0], Convert.FromBase64String(split[1]));
    }
}