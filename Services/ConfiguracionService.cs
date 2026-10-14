using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ConfiguracionService : IConfiguracionService
    {
        private readonly AppDbContext _context;

        public ConfiguracionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ConfiguracionDto> GetConfiguracionAsync()
        {
            var config = await _context.ConfiguracionSistema.FirstOrDefaultAsync(c => c.Id == 1);

            if (config == null)
            {
                // Si por alguna razón no existe el registro seed, lo creamos
                config = new ConfiguracionSistema
                {
                    Id = 1,
                    NombreMunicipalidad = "Municipalidad",
                    Ruc = "00000000000",
                    UITActual = 5150.00m,
                    TasaInteresMoratorio = 1.20m
                };
                _context.ConfiguracionSistema.Add(config);
                await _context.SaveChangesAsync();
            }

            return MapToDto(config);
        }

        public async Task<ConfiguracionDto> UpdateConfiguracionAsync(UpdateConfiguracionDto dto)
        {
            var config = await _context.ConfiguracionSistema.FirstOrDefaultAsync(c => c.Id == 1);

            if (config == null)
            {
                config = new ConfiguracionSistema { Id = 1 };
                _context.ConfiguracionSistema.Add(config);
            }

            config.NombreMunicipalidad = dto.NombreMunicipalidad;
            config.Ruc = dto.Ruc;
            config.Direccion = dto.Direccion;
            config.Telefono = dto.Telefono;
            config.EmailContacto = dto.EmailContacto;
            config.TasaInteresMoratorio = dto.TasaInteresMoratorio;
            config.UITActual = dto.UITActual;
            config.Moneda = dto.Moneda;

            await _context.SaveChangesAsync();

            return MapToDto(config);
        }

        private static ConfiguracionDto MapToDto(ConfiguracionSistema c)
        {
            return new ConfiguracionDto
            {
                Id = c.Id,
                NombreMunicipalidad = c.NombreMunicipalidad,
                Ruc = c.Ruc,
                Direccion = c.Direccion,
                Telefono = c.Telefono,
                EmailContacto = c.EmailContacto,
                TasaInteresMoratorio = c.TasaInteresMoratorio,
                UITActual = c.UITActual,
                Moneda = c.Moneda
            };
        }
    }
}
