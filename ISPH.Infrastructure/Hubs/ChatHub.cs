using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Hubs
{
    public class ChatHub : Hub
    {
        private static string groupName;

        public async Task CreateGroup(string employerId, string studentId)
        {
            if(employerId != studentId) groupName = employerId + studentId;
           await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendStudent(string message, string studentName)
        {
            await Clients.Group(groupName).SendAsync("SendStudent", message, studentName);
        }
        
        public async Task SendEmployer(string message, string employerName)
        {
            await Clients.Group(groupName).SendAsync("SendEmployer", message, employerName);
        }

        public async Task SendResume(string url, string name)
        {
            await Clients.Group(groupName).SendAsync("SendResume", url, name);
        }
    }
}
