using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public IActionResult GetChatConnection(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId)) { return BadRequest(); }

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

        [HttpPut]
        [Route(nameof(UpdateChatConnection))]
        public IActionResult UpdateChatConnection(ChatConnectionsDTO connectionData)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            try
            {
                var existingEntity =
                    _context.ChatConnections.Include(c => c.RoomNameNavigation).SingleOrDefault(p => p.ConnectionId == connectionData.ConnectionId);
                
                if (existingEntity == null)
                {
                    return NotFound();
                }

                var newEntity = _mapper.Map<ChatConnections>(connectionData);
                _mapper.Map(newEntity, existingEntity);
                _context.SaveChanges();

                return Ok(existingEntity.ConnectionId);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route(nameof(DeleteChatConnection))]
        public IActionResult DeleteChatConnection(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId) || !ModelState.IsValid) { return BadRequest(); }

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