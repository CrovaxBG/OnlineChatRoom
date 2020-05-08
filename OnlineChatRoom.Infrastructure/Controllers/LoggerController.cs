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
    public class LoggerController : ControllerBase
    {
        private readonly ChatContext _context;
        private readonly IMapper _mapper;

        public LoggerController(ChatContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [Route(nameof(GetLogs))]
        public IActionResult GetLogs()
        {
            try
            {
                return Ok(_context.Log.Select(_mapper.Map<LogDTO>));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route(nameof(GetLog))]
        public IActionResult GetLog(int? logId)
        {
            if (logId == null) { return BadRequest(); }

            try
            {
                var log = _context.Log.FirstOrDefault(l => l.Id == logId);

                if (log == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<LogDTO>(log));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route(nameof(AddLog))]
        public IActionResult AddLog(LogDTO log)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            try
            {
                log.Date = DateTime.Now;
                log.Id = 0;

                var entity = _mapper.Map<Log>(log);
                _context.Log.Add(entity);
                _context.SaveChanges();

                if (entity.Id > 0)
                {
                    return Ok(entity.Id);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return BadRequest();
        }
    }
}