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
    }

}
