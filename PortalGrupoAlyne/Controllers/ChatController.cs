using Microsoft.AspNetCore.Mvc;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

      


        [HttpGet("mensagens/{id}")]
        public IActionResult MinhasMensagensTotais(int id)
        {
            // Busca as mensagens em que o usuário é o remetente ou destinatário com base no ID
            var mensagens = _context.Message.Where(m => m.ReceiverId == id || m.SenderId == id).ToList();

            // Retorna as mensagens em arrays separados
            return Ok(mensagens);
        }

        [HttpGet("mensagens-recebidad/{id}")]
        public async Task<IActionResult> MinhasMensagensRecebidas(int id)
        {
            // Busca as mensagens em que o usuário é o destinatário com base no ID
            var mensagens = await _context.Message.Where(m => m.ReceiverId == id).ToListAsync();

            // Define a propriedade Lida como true e salva as mudanças no banco de dados
            mensagens.ForEach(m => m.Lida = true);
            await _context.SaveChangesAsync();

            // Retorna as mensagens
            return Ok(mensagens);
        }



        [HttpGet("mensagens")]
        public IActionResult MinhasMensagens(int senderId, int receiverId)
        {
            // Busca as mensagens em que o usuário é o remetente ou destinatário
            var mensagens = _context.Message.Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                                                        (m.SenderId == receiverId && m.ReceiverId == senderId))
                                             .ToList();

            // Retorna as mensagens em arrays separados
            return Ok(mensagens);
        }


        //[HttpPost]
        //public async Task<ActionResult> PostMessage(Message message)
        //{
        //    // Verifica se o destinatário está conectado
        //    var receiver = await _context.Usuario.FindAsync(message.ReceiverId);
        //    bool isReceiverConnected = receiver?.Conectado ?? false;

        //    // Define a propriedade Lida com base na conectividade do destinatário
        //    message.Lida = isReceiverConnected;

        //    // Salva a mensagem no banco de dados
        //    _context.Message.Add(message);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
        [HttpPost]
        public async Task<ActionResult> PostMessage(Message message)
        {
            // Define a propriedade Lida como false
            message.Lida = false;

            // Salva a mensagem no banco de dados
            _context.Message.Add(message);
            await _context.SaveChangesAsync();

            return Ok();
        }



    }
}
