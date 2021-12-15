using Datalayer;
using Datalayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Pressures
{
    public class PressureService : IPressureService
    {

        private DataContext _context;

        public PressureService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PressureDto>> GetAllAsync()
        {
            return await _context.Pressures.ToDto().ToListAsync();
        }

        public async Task<PressureDto> GetLatestAsync()
        {
            return await _context.Pressures.ToDto().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PressureDto>> GetTimeSpanAsync(DateTime start, DateTime end)
        {
            return await _context.Pressures
                                    .Where(t => t.Date > start)
                                    .Where(t => t.Date < end)
                                    .ToDto()
                                    .ToListAsync();
        }

        public async Task SetAsync(double Pressure)
        {
            Pressure press = new Pressure()
            {
                Date = DateTime.Now,
                Value = Pressure
            };

            _context.Pressures.Add(press);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Cant Create Pressure: " + e.Message);
            }
        }
    }
}
