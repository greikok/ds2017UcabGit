namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelAfterRestart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FalsaPersonas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FalsaPersonas", new[] { "ApplicationUser_Id" });
            RenameColumn(table: "dbo.Citas", name: "Evento_CitaId", newName: "Evento_CalendarioId");
            RenameIndex(table: "dbo.Citas", name: "IX_Evento_CitaId", newName: "IX_Evento_CalendarioId");
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
                        TipoSangre = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        CentroMedico_CentroMedicoId = c.Int(),
                        EspecialidadMedica_EspecialidadMedicaId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonaId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .ForeignKey("dbo.EspecialidadMedicas", t => t.EspecialidadMedica_EspecialidadMedicaId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.EspecialidadMedica_EspecialidadMedicaId);
            
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
                .PrimaryKey(t => t.CalendarioId)
                .ForeignKey("dbo.Personas", t => t.Medico_PersonaId)
                .Index(t => t.Medico_PersonaId);
            
            CreateTable(
                "dbo.HistoriaMedicas",
                c => new
                    {
                        HistoriaMedicaId = c.Int(nullable: false, identity: true),
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.HistoriaMedicaId)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .Index(t => t.Paciente_PersonaId);
            
            CreateTable(
                "dbo.EspecialidadMedicas",
                c => new
                    {
                        EspecialidadMedicaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadMedicaId);
            
            AddColumn("dbo.Citas", "Paciente_PersonaId", c => c.Int());
            CreateIndex("dbo.Citas", "Paciente_PersonaId");
            AddForeignKey("dbo.Citas", "Paciente_PersonaId", "dbo.Personas", "PersonaId");
            DropTable("dbo.FalsaPersonas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FalsaPersonas",
                c => new
                    {
                        FalsaPersonaId = c.Int(nullable: false, identity: true),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FalsaPersonaId);
            
            DropForeignKey("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId", "dbo.EspecialidadMedicas");
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.HistoriaMedicas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Citas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Calendarios", "Medico_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Personas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.HistoriaMedicas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.Calendarios", new[] { "Medico_PersonaId" });
            DropIndex("dbo.Citas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.Personas", new[] { "EspecialidadMedica_EspecialidadMedicaId" });
            DropIndex("dbo.Personas", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Personas", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Citas", "Paciente_PersonaId");
            DropTable("dbo.EspecialidadMedicas");
            DropTable("dbo.HistoriaMedicas");
            DropTable("dbo.Calendarios");
            DropTable("dbo.Personas");
            RenameIndex(table: "dbo.Citas", name: "IX_Evento_CalendarioId", newName: "IX_Evento_CitaId");
            RenameColumn(table: "dbo.Citas", name: "Evento_CalendarioId", newName: "Evento_CitaId");
            CreateIndex("dbo.FalsaPersonas", "ApplicationUser_Id");
            AddForeignKey("dbo.FalsaPersonas", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
