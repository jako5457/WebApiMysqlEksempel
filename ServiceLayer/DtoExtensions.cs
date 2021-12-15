using ServiceLayer.Dto;
using Datalayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class DtoExtensions
    {

        public static IQueryable<TempratureDto> ToDto(this IQueryable<Temprature> tempratures)
        {
            return tempratures.Select(t => new TempratureDto()
            {   
                Date = t.Date,
                Value = t.Value
            });
        }

        public static IQueryable<PressureDto> ToDto(this IQueryable<Pressure> pressures)
        {
            return pressures.Select(t => new PressureDto()
            {
                Date = t.Date,
                Value = t.Value
            });
        }



    }
}
