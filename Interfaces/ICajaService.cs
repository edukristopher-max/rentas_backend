using backend.dto;

namespace backend.Interfaces
{
    public interface ICajaService
    {
        Task<CajaAperturaResponseDto> AbrirCajaAsync(AbrirCajaDto dto);
        Task<CajaAperturaResponseDto?> ObtenerCajaAbiertaAsync(int usuarioId);
        Task<CajaMovimientoResponseDto> RegistrarMovimientoAsync(RegistrarMovimientoDto dto);
        Task<IEnumerable<CajaMovimientoResponseDto>> ObtenerMovimientosAsync(int cajaAperturaId);
        Task<CajaCierreResponseDto> CerrarCajaAsync(CerrarCajaDto dto);
    }
}
