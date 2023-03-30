using Microsoft.AspNetCore.Mvc;

namespace PortalGrupoAlyne.Controllers
{
    
    public class ChatController : Controller
    {
        private readonly DataContext _context;
        public ChatController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult MinhasMensagens(int id)
        {
            // Busca as mensagens em que o usuário é o remetente ou destinatário
            var mensagensRecebidas = _context.Message
                .Where(m => m.ReceiverId == id)
                .ToList();

            var mensagensEnviadas = _context.Message
                .Where(m => m.SenderId == id)
                .ToList();

            // Retorna as mensagens em arrays separados
            return Ok(new { Recebidas = mensagensRecebidas, Enviadas = mensagensEnviadas });
        }

        [HttpPost]
        public async Task<ActionResult> PostMessage(Message message)
        {
            // Verifica se o destinatário está conectado
            var receiver = await _context.Usuario.FindAsync(message.ReceiverId);
            bool isReceiverConnected = receiver?.Conectado ?? false;

            // Define a propriedade Lida com base na conectividade do destinatário
            message.Lida = isReceiverConnected;

            // Salva a mensagem no banco de dados
            _context.Message.Add(message);
            await _context.SaveChangesAsync();

            return Ok();
        }



    }
}
