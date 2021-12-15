using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Tempratures
{
    public interface ITempratureService
    {

        public Task<TempratureDto> GetLatestAsync();

        public Task<IEnumerable<TempratureDto>> GetAllAsync();

        public Task<IEnumerable<TempratureDto>> GetTimeSpanAsync(DateTime start, DateTime end);

        public Task SetAsync(double temprature);

    }
}
