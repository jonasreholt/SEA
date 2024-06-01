namespace Model.Structures;

public struct UserId(string userName, string password)
{
    public string UserName = userName;
    public int PasswordHash = password.GetHashCode();
}