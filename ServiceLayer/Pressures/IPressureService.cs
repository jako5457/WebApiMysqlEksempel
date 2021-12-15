using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Pressures
{
    public interface IPressureService
    {
        public Task<PressureDto> GetLatestAsync();

        public Task<IEnumerable<PressureDto>> GetAllAsync();

        public Task<IEnumerable<PressureDto>> GetTimeSpanAsync(DateTime start, DateTime end);

        public Task SetAsync(double Pressure);
    }
}
