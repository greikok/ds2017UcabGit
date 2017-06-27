namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calendarios",
                c => new
                    {
                        CalendarioId = c.Int(nullable: false, identity: true),
                        HoraInicio = c.DateTime(nullable: false),
                        HoraFin = c.DateTime(nullable: false),
                        Medico_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.CalendarioId)
                .ForeignKey("dbo.Personas", t => t.Medico_PersonaId)
                .Index(t => t.Medico_PersonaId);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        PersonaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Cedula = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Email = c.String(),
                        Direccion = c.String(),
                        Especialidad = c.String(),
                        Sueldo = c.Double(),
                        TipoSangre = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        CentroMedico_CentroMedicoId = c.Int(),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PersonaId)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        CitaId = c.Int(nullable: false, identity: true),
                        CentroMedico_CentroMedicoId = c.Int(),
                        Evento_CitaId = c.Int(),
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .ForeignKey("dbo.Citas", t => t.Evento_CitaId)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .Index(t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.Evento_CitaId)
                .Index(t => t.Paciente_PersonaId);
            
            CreateTable(
                "dbo.CentroMedicoes",
                c => new
                    {
                        CentroMedicoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Rif = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                    })
                .PrimaryKey(t => t.CentroMedicoId);
            
            CreateTable(
                "dbo.RecursoHospitalarios",
                c => new
                    {
                        RecursoHospitalarioId = c.Int(nullable: false, identity: true),
                        CentroMedico_CentroMedicoId = c.Int(),
                    })
                .PrimaryKey(t => t.RecursoHospitalarioId)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.CentroMedico_CentroMedicoId);
            
            CreateTable(
                "dbo.Tratamientoes",
                c => new
                    {
                        TratamientoId = c.Int(nullable: false, identity: true),
                        Cita_CitaId = c.Int(),
                    })
                .PrimaryKey(t => t.TratamientoId)
                .ForeignKey("dbo.Citas", t => t.Cita_CitaId)
                .Index(t => t.Cita_CitaId);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Calendarios", "Medico_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Personas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HistoriaMedicas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Tratamientoes", "Cita_CitaId", "dbo.Citas");
            DropForeignKey("dbo.Citas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Citas", "Evento_CitaId", "dbo.Citas");
            DropForeignKey("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Citas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.HistoriaMedicas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.Tratamientoes", new[] { "Cita_CitaId" });
            DropIndex("dbo.RecursoHospitalarios", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Citas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.Citas", new[] { "Evento_CitaId" });
            DropIndex("dbo.Citas", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Personas", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Personas", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Calendarios", new[] { "Medico_PersonaId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.HistoriaMedicas");
            DropTable("dbo.Tratamientoes");
            DropTable("dbo.RecursoHospitalarios");
            DropTable("dbo.CentroMedicoes");
            DropTable("dbo.Citas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Personas");
            DropTable("dbo.Calendarios");
        }
    }
}
