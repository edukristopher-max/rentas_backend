using System.Diagnostics;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(AppDbContext context, ILogger<DatabaseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Verifica el estado de la conexión a la base de datos configurada.
        /// Rutas disponibles:
        /// GET /api/database
        /// GET /api/database/check-connection
        /// </summary>
        [HttpGet]
        [HttpGet("check-connection")]
        public async Task<IActionResult> CheckConnection()
        {
            var stopwatch = Stopwatch.StartNew();
            var dbConnection = _context.Database.GetDbConnection();

            try
            {
                bool canConnect = await _context.Database.CanConnectAsync();
                stopwatch.Stop();

                if (!canConnect)
                {
                    _logger.LogWarning("No se pudo conectar a la base de datos '{Database}' en el servidor '{DataSource}'.", 
                        dbConnection.Database, dbConnection.DataSource);

                    return StatusCode(StatusCodes.Status503ServiceUnavailable, new
                    {
                        success = false,
                        status = "Error",
                        message = "No fue posible conectar con la base de datos.",
                        database = dbConnection.Database,
                        server = dbConnection.DataSource,
                        responseTimeMs = stopwatch.ElapsedMilliseconds,
                        timestamp = DateTime.UtcNow
                    });
                }

                // Verificar si existen migraciones pendientes
                IEnumerable<string> pendingMigrations = Enumerable.Empty<string>();
                try
                {
                    pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                }
                catch (Exception migEx)
                {
                    _logger.LogWarning(migEx, "No se pudieron consultar las migraciones pendientes.");
                }

                return Ok(new
                {
                    success = true,
                    status = "Conectado",
                    message = "Conexión a la base de datos establecida exitosamente.",
                    database = dbConnection.Database,
                    server = dbConnection.DataSource,
                    responseTimeMs = stopwatch.ElapsedMilliseconds,
                    hasPendingMigrations = pendingMigrations.Any(),
                    pendingMigrations = pendingMigrations,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Excepción ocurrida al intentar conectar a la base de datos.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    status = "Error",
                    message = "Ocurrió una excepción al intentar conectarse a la base de datos.",
                    error = ex.Message,
                    database = dbConnection.Database,
                    server = dbConnection.DataSource,
                    responseTimeMs = stopwatch.ElapsedMilliseconds,
                    timestamp = DateTime.UtcNow
                });
            }
        }

        /// <summary>
        /// Aplica las migraciones pendientes en la base de datos a petición.
        /// Ruta: POST /api/database/migrate
        /// </summary>
        [HttpPost("migrate")]
        public async Task<IActionResult> Migrate()
        {
            var stopwatch = Stopwatch.StartNew();
            var dbConnection = _context.Database.GetDbConnection();

            try
            {
                var pendingMigrations = (await _context.Database.GetPendingMigrationsAsync()).ToList();

                if (!pendingMigrations.Any())
                {
                    stopwatch.Stop();
                    return Ok(new
                    {
                        success = true,
                        message = "La base de datos ya está completamente actualizada. No hay migraciones pendientes.",
                        appliedCount = 0,
                        database = dbConnection.Database,
                        server = dbConnection.DataSource,
                        elapsedMs = stopwatch.ElapsedMilliseconds,
                        timestamp = DateTime.UtcNow
                    });
                }

                _logger.LogInformation("Iniciando aplicación de {Count} migraciones pendientes...", pendingMigrations.Count);
                await _context.Database.MigrateAsync();
                stopwatch.Stop();

                return Ok(new
                {
                    success = true,
                    message = $"Se aplicaron exitosamente {pendingMigrations.Count} migración(es) a la base de datos.",
                    appliedCount = pendingMigrations.Count,
                    appliedMigrations = pendingMigrations,
                    database = dbConnection.Database,
                    server = dbConnection.DataSource,
                    elapsedMs = stopwatch.ElapsedMilliseconds,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error al ejecutar las migraciones en la base de datos.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "Ocurrió un error al ejecutar las migraciones.",
                    error = ex.Message,
                    database = dbConnection.Database,
                    server = dbConnection.DataSource,
                    elapsedMs = stopwatch.ElapsedMilliseconds,
                    timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
