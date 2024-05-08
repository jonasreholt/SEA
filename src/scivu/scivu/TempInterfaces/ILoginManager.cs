using System.Threading.Tasks;

namespace scivu.Models;

public interface ILoginManager
{
    public Task<(bool, IReadSurvey?)> GetSurvey(int pin);
    
    public Task<bool> Login(string usrname, string passw);
}