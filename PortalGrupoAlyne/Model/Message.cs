using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool Lida { get; set; }

        public Message(int senderId, int receiverId, string body)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Body = body;
            Date = DateTime.Now;
            Lida = false;
        }

        [NotMapped]
        public int? NaoLidas { get; set; }
        [NotMapped]
        public string? NomeCompletoSender { get; set; }
        [NotMapped]
        public string? UsernameSender { get; set; }
        [NotMapped]
        public string? NomeCompletoReceiver { get; set; }
        [NotMapped]
        public string? UsernameReceiver { get; set; }
    }
}

