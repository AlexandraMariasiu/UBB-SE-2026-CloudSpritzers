using System;
using System.Collections.Generic;
using System.ComponentModel;
using CloudSpritzers.src.model.message;
using CloudSpritzers.src.model.chat;

namespace CloudSpritzers.src.model.chat
{
    public class Chat
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public ChatStatus Status { get; set; }

        public List<IMessage> Messages { get; set; }

        public Chat(int chatId, int userId, int employeeId, ChatStatus chatStatus)
        {
            ChatId = chatId;
            UserId = userId;
            EmployeeId = employeeId;
            Status = chatStatus;

            Messages = new List<IMessage>();
        }

        public void AddMessage(IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message), "message is empty");
            Messages.Add(message);
        }


        public void AssignEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "No employee. Crickets");
            EmployeeId = employee.EmployeeId; 
        }

        public int MessageCount()
        {
            return Messages.Count;
        }

    }
}
