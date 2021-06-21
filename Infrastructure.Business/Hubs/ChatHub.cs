using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Business.Hubs
{
    public class ChatHub : Hub
    {
        private static string _groupName;

        public async Task CreateGroup(string employerId, string studentId)
        {
            if(employerId != studentId) _groupName = employerId + studentId;
           await Groups.AddToGroupAsync(Context.ConnectionId, _groupName);
        }

        public async Task SendStudent(string message, string studentName)
        {
            await Clients.Group(_groupName).SendAsync("SendStudent", message, studentName);
        }
        
        public async Task SendEmployer(string message, string employerName)
        {
            await Clients.Group(_groupName).SendAsync("SendEmployer", message, employerName);
        }

        public async Task SendResume(string url, string name)
        {
            await Clients.Group(_groupName).SendAsync("SendResume", url, name);
        }
    }
}
