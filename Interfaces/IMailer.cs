
using System.Threading.Tasks;

namespace nuget_host.Interfaces
{
    public interface IMailer
    {
        Task SendMailAsync(string name, string email, string subjet, string body);
    }
}