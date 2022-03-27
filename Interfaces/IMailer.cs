
using System.Threading.Tasks;

namespace IdServer.Interfaces
{
    public interface IMailer
    {
        Task SendMailAsync(string name, string email, string subjet, string body);
    }
}