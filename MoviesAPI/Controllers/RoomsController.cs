﻿using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;
using System.Collections.Generic;

namespace MoviesAPI.Controllers
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
        /// <returns>Room if exist</returns>
        [HttpGet("{roomId}/Switches")]
        [ExecutionTime]
        public IActionResult GetReviews(int roomId)
        {
            var rooms = _roomsService.GetByRoomId(roomId);
            if (rooms == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<List<RoomResponse>>(rooms));
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
        [HttpPut]
        public IActionResult Put([FromBody]RoomRequest room)
        {
            if (_roomsService.UpdateRoom(AutoMapper.Mapper.Map<Room>(room)))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete room
        /// </summary>
        /// <param name="roomId"> room identifier</param>
        /// <returns></returns>
        [HttpDelete("{roomId}")]
        public IActionResult Delete(int roomId)
        {
            _roomsService.Remove(roomId);
            return Ok();
        }
    }
}
