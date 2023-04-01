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

        //[HttpGet("mensagens/{id}")]
        //public IActionResult MinhasMensagensTotais(int id)
        //{
        //    // Busca as mensagens em que o usuário é o remetente ou destinatário com base no ID
        //    var mensagens = _context.Message.Where(m => m.ReceiverId == id || m.SenderId == id).ToList();

        //    // Obtém os senderIds únicos
        //    var senderIds = mensagens.Select(m => m.SenderId).Distinct().ToList();

        //    // Itera por cada senderId e calcula o número de mensagens não lidas para ele
        //    foreach (var senderId in senderIds)
        //    {
        //        var naoLidas = _context.Message.Count(m => m.SenderId == senderId && m.ReceiverId == id && m.Lida == false);
        //        // Atualiza o valor de NaoLidas para todas as mensagens com o senderId atual
        //        foreach (var mensagem in mensagens.Where(m => m.SenderId == senderId))
        //        {
        //            mensagem.NaoLidas = naoLidas;
        //        }
        //    }

        //    // Retorna as mensagens atualizadas
        //    return Ok(mensagens);
        //}


        [HttpGet("mensagens/{id}")]
        public IActionResult MinhasMensagensTotais(int id)
        {
            // Busca as mensagens em que o usuário é o remetente ou destinatário com base no ID
            var mensagens = _context.Message.Where(m => m.ReceiverId == id || m.SenderId == id).ToList();

            // Obtém os senderIds e receiverIds únicos
            var senderIds = mensagens.Select(m => m.SenderId).Distinct().ToList();
            var receiverIds = mensagens.Select(m => m.ReceiverId).Distinct().ToList();
            var userIds = senderIds.Concat(receiverIds).Distinct().ToList();

            // Cria um dicionário com os usuários correspondentes a cada id de usuário
            var usuarios = new Dictionary<int, Usuario>();
            foreach (var userId in userIds)
            {
                var usuario = _context.Usuario.FirstOrDefault(u => u.Id == userId);
                usuarios.Add(userId, usuario);
            }

            // Itera por cada mensagem e adiciona as informações do outro usuário correspondente
            foreach (var mensagem in mensagens)
            {
                if (mensagem.SenderId == id)
                {
                    mensagem.NomeCompletoReceiver = usuarios[mensagem.ReceiverId].NomeCompleto;
                    mensagem.UsernameReceiver = usuarios[mensagem.ReceiverId].Username;
                }
                else
                {
                    mensagem.NomeCompletoSender = usuarios[mensagem.SenderId].NomeCompleto;
                    mensagem.UsernameSender = usuarios[mensagem.SenderId].Username;
                }
            }

            // Obtém o número de mensagens não lidas para cada remetente
            foreach (var senderId in senderIds)
            {
                var naoLidas = _context.Message.Count(m => m.SenderId == senderId && m.ReceiverId == id && !m.Lida);
                foreach (var mensagem in mensagens.Where(m => m.SenderId == senderId))
                {
                    mensagem.NaoLidas = naoLidas;
                }
            }

            // Retorna as mensagens atualizadas
            return Ok(mensagens);
        }


        [HttpGet("mensagens/busca")]
        public IActionResult BuscaMensagensPorRemetente(string busca, int id)
        {
            // Busca as mensagens em que o usuário é o remetente ou destinatário com base no ID
            var mensagens = _context.Message.Where(m => m.ReceiverId == id || m.SenderId == id).ToList();

            // Obtém os senderIds e receiverIds únicos
            var senderIds = mensagens.Select(m => m.SenderId).Distinct().ToList();
            var receiverIds = mensagens.Select(m => m.ReceiverId).Distinct().ToList();
            var userIds = senderIds.Concat(receiverIds).Distinct().ToList();

            // Cria um dicionário com os usuários correspondentes a cada id de usuário
            var usuarios = new Dictionary<int, Usuario>();
            foreach (var userId in userIds)
            {
                var usuario = _context.Usuario.FirstOrDefault(u => u.Id == userId);
                usuarios.Add(userId, usuario);
            }

            // Itera por cada mensagem e adiciona as informações do outro usuário correspondente
            foreach (var mensagem in mensagens)
            {
                if (mensagem.SenderId == id)
                {
                    mensagem.NomeCompletoReceiver = usuarios[mensagem.ReceiverId].NomeCompleto;
                    mensagem.UsernameReceiver = usuarios[mensagem.ReceiverId].Username;
                }
                else
                {
                    mensagem.NomeCompletoSender = usuarios[mensagem.SenderId].NomeCompleto;
                    mensagem.UsernameSender = usuarios[mensagem.SenderId].Username;
                }
            }

            // Obtém o número de mensagens não lidas para cada remetente
            foreach (var senderId in senderIds)
            {
                var naoLidas = _context.Message.Count(m => m.SenderId == senderId && m.ReceiverId == id && !m.Lida);
                foreach (var mensagem in mensagens.Where(m => m.SenderId == senderId))
                {
                    mensagem.NaoLidas = naoLidas;
                }
            }

            // Retorna as mensagens atualizadas filtrando pelo nome completo ou username do remetente
            var mensagensFiltradas = mensagens.Where(m =>
                (usuarios[m.SenderId].NomeCompleto.Contains(busca) || usuarios[m.SenderId].Username.Contains(busca))
                && m.SenderId != id);

            return Ok(mensagensFiltradas);
        }






        [HttpGet("mensagens-recebidas")]
        public async Task<IActionResult> MinhasMensagensRecebidas(int id, int senderId)
        {
            // Busca as mensagens em que o usuário é o destinatário com base no ID
            var mensagens = await _context.Message.Where(m => m.ReceiverId == id && m.SenderId == senderId).ToListAsync();

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
