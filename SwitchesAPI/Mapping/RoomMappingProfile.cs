﻿using AutoMapper;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Models;
using System.Collections.Generic;

namespace SwitchesAPI.Mapping
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<RoomRequest, Room>();
            CreateMap<Room, RoomResponse>();
        }
    }
}
