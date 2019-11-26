using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data;

namespace ATMBusinessLayer
{
    sealed class AutoMapperAccountProfile: Profile
    {
        public AutoMapperAccountProfile()
        {
            IMappingExpression<DataRow, Account> mappingExpression;
            mappingExpression = CreateMap<DataRow, Account>();
            mappingExpression.ForMember(d => d.Id, o => o.MapFrom(s => s["id"]));
            mappingExpression.ForMember(d => d.DateCreation, o => o.MapFrom(s => s["datecreation"]));
            mappingExpression.ForMember(d => d.Total, o => o.MapFrom(s => s["total"]));
            mappingExpression.ForMember(d => d.IdOwner, o => o.MapFrom(s => s["idowner"]));
        }
    }
}
