SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;

INSERT INTO LicenciasTasas (ContribuyenteId, Tipo, Concepto, Monto, FechaEmision, Estado, NumeroExpediente)
VALUES (1, 'Tasa', 'Licencia Test Fase 4C.7.1', 100.00, GETDATE(), 'Pendiente', 'EXP-2026-TEST');
DECLARE @LicenciaId INT = SCOPE_IDENTITY();

-- Create another Obligacion for testing (S/100) linked to the new Licencia
INSERT INTO Obligaciones (ContribuyenteId, LicenciaTasaId, OrigenReferencia, Periodo, EsHistoricoMigrado, Total, Saldo, Pagado, Estado, FechaVencimiento)
VALUES (1, @LicenciaId, 'MANUAL', '2026-TEST-2', 0, 100.00, 100.00, 0.00, 'Pendiente', GETDATE());
DECLARE @ObligacionId INT = SCOPE_IDENTITY();

INSERT INTO ObligacionesDetalles (ObligacionId, Concepto, Monto)
VALUES (@ObligacionId, 'Concepto Prueba Fase 4C.7.1', 100.00);

SELECT @ObligacionId AS TestObligacionId;
