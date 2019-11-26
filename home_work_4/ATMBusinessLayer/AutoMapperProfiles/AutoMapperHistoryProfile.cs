using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data;

namespace ATMBusinessLayer
{
    sealed class AutoMapperHistoryProfile: Profile
    {
        public AutoMapperHistoryProfile()
        {
            IMappingExpression<DataRow, History> mappingExpression;
            mappingExpression = CreateMap<DataRow, History>();
            mappingExpression.ForMember(d => d.Id, o => o.MapFrom(s => s["id"]));
            mappingExpression.ForMember(d => d.DateOperation, o => o.MapFrom(s => s["dateoperation"]));
            mappingExpression.ForMember(d => d.OpType, o => o.MapFrom(s => s["typeop"]));
            mappingExpression.ForMember(d => d.Amount, o => o.MapFrom(s => s["amount"]));
            mappingExpression.ForMember(d => d.IdAccount, o => o.MapFrom(s => s["idaccount"]));
        }
    }
}
