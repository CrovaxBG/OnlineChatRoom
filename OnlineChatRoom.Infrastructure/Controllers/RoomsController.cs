using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineChatRoom.Common.DTO;
using OnlineChatRoom.DataAccess.Models;

namespace OnlineChatRoom.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ChatContext _context;
        private readonly IMapper _mapper;

        public RoomsController(ChatContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [Route(nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            try
            {
                return Ok(_context.Rooms.Select(_mapper.Map<RoomsDTO>));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route(nameof(GetRoom))]
        public IActionResult GetRoom(string roomName)
        {
            if (roomName == null) { return BadRequest(); }

            try
            {
                var room = _context.Rooms.Include(r => r.ChatConnections).FirstOrDefault(l => l.RoomName == roomName);

                if (room == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<RoomsDTO>(room));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route(nameof(CreateRoom))]
        public IActionResult CreateRoom(RoomsDTO room)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            try
            {
                var entity = _mapper.Map<Rooms>(room);
                _context.Rooms.Add(entity);
                _context.SaveChanges();

                return Ok(entity.RoomName);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route(nameof(RemoveRoom))]
        public IActionResult RemoveRoom(string roomName)
        {
            if (roomName == null || !ModelState.IsValid) { return BadRequest(); }

            try
            {
                var entity = _context.Rooms.SingleOrDefault(p => p.RoomName == roomName);
                if (entity == null)
                {
                    return NotFound();
                }

                _context.Rooms.Remove(entity);
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