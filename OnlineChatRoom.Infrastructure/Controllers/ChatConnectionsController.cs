using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineChatRoom.Common.DTO;
using OnlineChatRoom.DataAccess.Models;

namespace OnlineChatRoom.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatConnectionsController : ControllerBase
    {
        private readonly ChatContext _context;
        private readonly IMapper _mapper;

        public ChatConnectionsController(ChatContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [Route(nameof(GetChatConnection))]
        public IActionResult GetChatConnection(Guid connectionId)
        {
            if (connectionId == Guid.Empty) { return BadRequest(); }

            try
            {
                var connection = _context.ChatConnections.FirstOrDefault(l => l.ConnectionId == connectionId);

                if (connection == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ChatConnectionsDTO>(connection));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route(nameof(CreateChatConnection))]
        public IActionResult CreateChatConnection(ChatConnectionsDTO connectionData)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            try
            {
                var entity = _mapper.Map<ChatConnections>(connectionData);
                _context.ChatConnections.Add(entity);
                _context.SaveChanges();

                return Ok(entity.ConnectionId);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route(nameof(DeleteChatConnection))]
        public IActionResult DeleteChatConnection(Guid connectionId)
        {
            if (connectionId == Guid.Empty || !ModelState.IsValid) { return BadRequest(); }

            try
            {
                var entity = _context.ChatConnections.SingleOrDefault(p => p.ConnectionId == connectionId);
                if (entity == null)
                {
                    return NotFound();
                }

                _context.ChatConnections.Remove(entity);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}