﻿using AutoMapper;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Models;

namespace SwitchesAPI.Mapping
{
    public class SwitchMappingProfile : Profile
    {
        public SwitchMappingProfile()
        {
            CreateMap<SwitchRequest, Switch>();
            CreateMap<Switch, SwitchResponse>();
        }
    }
}
