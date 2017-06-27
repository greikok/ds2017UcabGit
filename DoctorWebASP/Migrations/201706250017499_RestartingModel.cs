namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestartingModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId1", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Citas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.HistoriaMedicas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Personas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId", "dbo.EspecialidadMedicas");
            DropForeignKey("dbo.Calendarios", "Medico_PersonaId", "dbo.Personas");
            DropIndex("dbo.Calendarios", new[] { "Medico_PersonaId" });
            DropIndex("dbo.Personas", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Personas", new[] { "CentroMedico_CentroMedicoId1" });
            DropIndex("dbo.Personas", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Personas", new[] { "EspecialidadMedica_EspecialidadMedicaId" });
            DropIndex("dbo.Citas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.HistoriaMedicas", new[] { "Paciente_PersonaId" });
            CreateTable(
                "dbo.FalsaPersonas",
                c => new
                    {
                        FalsaPersonaId = c.Int(nullable: false, identity: true),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FalsaPersonaId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Citas", "Paciente_PersonaId");
            DropTable("dbo.Calendarios");
            DropTable("dbo.Personas");
            DropTable("dbo.HistoriaMedicas");
            DropTable("dbo.EspecialidadMedicas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EspecialidadMedicas",
                c => new
                    {
                        EspecialidadMedicaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadMedicaId);
            
            CreateTable(
                "dbo.HistoriaMedicas",
                c => new
                    {
                        HistoriaMedicaId = c.Int(nullable: false, identity: true),
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.HistoriaMedicaId);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        PersonaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Cedula = c.String(nullable: false),
                        Genero = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        Email = c.String(),
                        Direccion = c.String(),
                        Sueldo = c.Decimal(precision: 18, scale: 2),
                        Rol = c.String(),
                        Sueldo1 = c.Decimal(precision: 18, scale: 2),
                        TipoSangre = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        CentroMedico_CentroMedicoId = c.Int(),
                        CentroMedico_CentroMedicoId1 = c.Int(),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        EspecialidadMedica_EspecialidadMedicaId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonaId);
            
            CreateTable(
                "dbo.Calendarios",
                c => new
                    {
                        CalendarioId = c.Int(nullable: false, identity: true),
                        HoraInicio = c.DateTime(nullable: false),
                        HoraFin = c.DateTime(nullable: false),
                        Cancelada = c.Boolean(nullable: false),
                        Disponible = c.Byte(nullable: false),
                        Medico_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.CalendarioId);
            
            AddColumn("dbo.Citas", "Paciente_PersonaId", c => c.Int());
            DropForeignKey("dbo.FalsaPersonas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FalsaPersonas", new[] { "ApplicationUser_Id" });
            DropTable("dbo.FalsaPersonas");
            CreateIndex("dbo.HistoriaMedicas", "Paciente_PersonaId");
            CreateIndex("dbo.Citas", "Paciente_PersonaId");
            CreateIndex("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId");
            CreateIndex("dbo.Personas", "ApplicationUser_Id");
            CreateIndex("dbo.Personas", "CentroMedico_CentroMedicoId1");
            CreateIndex("dbo.Personas", "CentroMedico_CentroMedicoId");
            CreateIndex("dbo.Calendarios", "Medico_PersonaId");
            AddForeignKey("dbo.Calendarios", "Medico_PersonaId", "dbo.Personas", "PersonaId");
            AddForeignKey("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId", "dbo.EspecialidadMedicas", "EspecialidadMedicaId");
            AddForeignKey("dbo.Personas", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.HistoriaMedicas", "Paciente_PersonaId", "dbo.Personas", "PersonaId");
            AddForeignKey("dbo.Citas", "Paciente_PersonaId", "dbo.Personas", "PersonaId");
            AddForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId1", "dbo.CentroMedicoes", "CentroMedicoId");
            AddForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId");
        }
    }
}
