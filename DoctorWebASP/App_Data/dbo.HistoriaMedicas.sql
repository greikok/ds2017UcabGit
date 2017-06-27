CREATE TABLE [dbo].[HistoriaMedicas] (
    [HistoriaMedicaId]   INT IDENTITY (1, 1) NOT NULL,
    [Paciente_PersonaId] INT NULL,
    [Ante_Personal] VARCHAR(50) NULL, 
    [Ante_Familiar] VARCHAR(50) NULL, 
    [Hab_Psicologico] VARCHAR(50) NULL, 
    [Ante_Prenatal] VARCHAR(50) NULL, 
    [Examen_Fisico] VARCHAR(50) NULL, 
    [Motivo_Consulta] VARCHAR(50) NULL, 
    [Enf_Actual] VARBINARY(50) NULL, 
    [Diag_Ingreso] VARCHAR(50) NULL, 
    [Fecha] DATE NULL, 
    [Tipo_Ingreso] VARCHAR(50) NULL, 
    CONSTRAINT [PK_dbo.HistoriaMedicas] PRIMARY KEY CLUSTERED ([HistoriaMedicaId] ASC),
    CONSTRAINT [FK_dbo.HistoriaMedicas_dbo.Personas_Paciente_PersonaId] FOREIGN KEY ([Paciente_PersonaId]) REFERENCES [dbo].[Personas] ([PersonaId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Paciente_PersonaId]
    ON [dbo].[HistoriaMedicas]([Paciente_PersonaId] ASC);

