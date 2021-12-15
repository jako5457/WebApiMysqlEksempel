using Datalayer;
using Datalayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Tempratures
{
    public class TempratureService : ITempratureService
    {

        private DataContext _context;

        public TempratureService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TempratureDto>> GetAllAsync()
        {
            return await _context.Tempratures.ToDto().ToListAsync();
        }

        public async Task<TempratureDto> GetLatestAsync()
        {
            return await _context.Tempratures.ToDto().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TempratureDto>> GetTimeSpanAsync(DateTime start, DateTime end)
        {
            return await _context.Tempratures
                                    .Where(t => t.Date > start)
                                    .Where(t => t.Date < end)
                                    .ToDto()
                                    .ToListAsync();
        }

        public async Task SetAsync(double temprature)
        {
            Temprature Temp = new Temprature()
            {
                Date = DateTime.Now,
                Value = temprature
            };

            _context.Tempratures.Add(Temp);

            try
            {
               await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Cant Create Temprature: " + e.Message);
            }
        }
    }
}
