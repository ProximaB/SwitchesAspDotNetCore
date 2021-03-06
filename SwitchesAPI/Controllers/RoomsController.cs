﻿using System;
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
        private readonly IRoomsService roomsService;

        public RoomsController (IRoomsService _roomsService)
        {
            roomsService = _roomsService;
        }

        /// <summary>
        /// Get all Rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllRooms ()
        {
            var rooms = AutoMapper.Mapper.Map<List<RoomResponse>>(roomsService.GetAll());
            return Ok(rooms);
        }

        /// <summary>
        /// Get Room by id
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <returns>Room if exist</returns>
        [HttpGet("{roomId}")]
        public IActionResult Get (int roomId)
        {
            Room room = roomsService.GetById(roomId);

            if ( room == null )
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
        public IActionResult GetSwitches (int roomId)
        {
            var switches = roomsService.GetSwitchesByRoomId(roomId);
            if ( switches == null )
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
        public IActionResult Post ([FromBody]RoomRequest room)
        {
            if ( !roomsService.AddNewRoom(AutoMapper.Mapper.Map<Room>(room)) )
            {
                return BadRequest();
            }

            int roomId = roomsService.LastUpdatedId ?? throw new NullReferenceException();
            Room _room = roomsService.GetById(roomId);
            return Ok(AutoMapper.Mapper.Map<RoomResponse>(_room));
        }

        /// <summary>
        /// Update room in repositorium
        /// </summary>
        /// <param name="roomId">updated room</param>
        /// <param name="room">updated room</param>
        /// <returns></returns>
        [HttpPut("{roomId}")]
        public IActionResult Put (int roomId, [FromBody] RoomRequest room)
        {
            if ( !roomsService.UpdateRoom(roomId, AutoMapper.Mapper.Map<Room>(room)) )
            {
                return BadRequest();
            }

            //Room _room = roomsService.GetById(roomId);
            // return Ok(AutoMapper.Mapper.Map<List<RoomResponse>>(_room));   
            return Get(roomId);
        }

        //public IActionResult Put([FromBody]RoomRequestPut room)
        //{
        //    if (roomsService.UpdateRoom(AutoMapper.Mapper.Map<Room>(room)))
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
        public IActionResult Delete (int roomId)
        {
            if ( !roomsService.Delete(roomId) )
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
