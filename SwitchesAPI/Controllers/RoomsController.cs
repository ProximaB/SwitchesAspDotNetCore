using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;
using System.Collections.Generic;

namespace SwitchesAPI.Controllers
{
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomsService _roomsService;

        public RoomsController(IRoomsService reviewsService)
        {
            _roomsService = reviewsService;
        }

        /// <summary>
        /// Get all Rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllRooms()
        {
            var rooms = AutoMapper.Mapper.Map<List<RoomResponse>>(_roomsService.GetAll());
            return Ok(rooms);
        }

        /// <summary>
        /// Get Room by id
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <returns>Room if exist</returns>
        [HttpGet("{roomId}")]
        public IActionResult Get(int roomId)
        {
            Room room = _roomsService.GetById(roomId);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<RoomResponse>(room));
        }

        /// <summary>
        /// Get List of switches
        /// </summary>
        /// <param name="roomId">room id</param>
        /// <returns>Room's switches if exist</returns>
        [HttpGet("{roomId}/Switches")]
        [ExecutionTime]
        public IActionResult GetReviews(int roomId)
        {
            var switches = _roomsService.GetSwitchesByRoomId(roomId);
            if (switches == null)
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<List<SwitchResponse>>(switches));
        }

        /// <summary>
        /// Add new room
        /// </summary>
        /// <param name="room">Room</param>
        /// <returns></returns>
        [HttpPost]
        [SwitchApiExceptionFilter]
        public IActionResult Post([FromBody]RoomRequest room)
        {
            if (!_roomsService.AddNewRoom(AutoMapper.Mapper.Map<Room>(room)))
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Update room in repositorium
        /// </summary>
        /// <param name="room">updated room</param>
        /// <returns></returns>
        [HttpPut("{roomId}")]
        public IActionResult Put(int roomId, [FromBody] RoomRequest room)
        {
            if (_roomsService.UpdateRoom(roomId, AutoMapper.Mapper.Map<Room>(room)))
            {
                return Ok();
            }

            return BadRequest();
        }
        //public IActionResult Put([FromBody]RoomRequestPut room)
        //{
        //    if (_roomsService.UpdateRoom(AutoMapper.Mapper.Map<Room>(room)))
        //    {
        //        return Ok();
        //    }

        //    return BadRequest();
        //}

        /// <summary>
        /// Delete room
        /// </summary>
        /// <param name="roomId"> room identifier</param>
        /// <returns></returns>
        [HttpDelete("{roomId}")]
        public IActionResult Delete(int roomId)
        {
            if (!_roomsService.Delete(roomId))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
