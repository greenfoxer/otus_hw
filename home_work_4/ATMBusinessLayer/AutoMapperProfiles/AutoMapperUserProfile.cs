using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data;

namespace ATMBusinessLayer
{
    sealed class AutoMapperUserProfile: Profile
    {
        public AutoMapperUserProfile()
        {
            IMappingExpression<DataRow, User> mappingExpression;
            mappingExpression = CreateMap<DataRow, User>();
            mappingExpression.ForMember(d => d.Id, o => o.MapFrom(s => s["id"]));
            mappingExpression.ForMember(d => d.Name, o => o.MapFrom(s => s["name"]));
            mappingExpression.ForMember(d => d.MiddleName, o => o.MapFrom(s => s["middlename"]));
            mappingExpression.ForMember(d => d.Surname, o => o.MapFrom(s => s["surname"]));
            mappingExpression.ForMember(d => d.Phone, o => o.MapFrom(s => s["phone"]));
            mappingExpression.ForMember(d => d.IdentityData, o => o.MapFrom(s => s["identitydata"]));
            mappingExpression.ForMember(d => d.Datereg, o => o.MapFrom(s => s["datereg"]));
            mappingExpression.ForMember(d => d.Login, o => o.MapFrom(s => s["login"]));
            mappingExpression.ForMember(d => d.Password, o => o.MapFrom(s => s["password"]));
        }
    }
}
