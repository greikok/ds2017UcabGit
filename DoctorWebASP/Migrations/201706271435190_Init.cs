namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Almacens",
                c => new
                    {
                        AlmacenId = c.Int(nullable: false, identity: true),
                        Disponible = c.Int(nullable: false),
                        CentroMedico_CentroMedicoId = c.Int(),
                        RecursoHospitalario_RecursoHospitalarioId = c.Int(),
                    })
                .PrimaryKey(t => t.AlmacenId)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .ForeignKey("dbo.RecursoHospitalarios", t => t.RecursoHospitalario_RecursoHospitalarioId)
                .Index(t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.RecursoHospitalario_RecursoHospitalarioId);
            
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
                "dbo.Citas",
                c => new
                    {
                        CitaId = c.Int(nullable: false),
                        Paciente_PersonaId = c.Int(),
                        CentroMedico_CentroMedicoId = c.Int(),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.Calendarios", t => t.CitaId)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.CitaId)
                .Index(t => t.Paciente_PersonaId)
                .Index(t => t.CentroMedico_CentroMedicoId);
            
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
                "dbo.EspecialidadMedicas",
                c => new
                    {
                        EspecialidadMedicaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadMedicaId);
            
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
                "dbo.RecursoHospitalarios",
                c => new
                    {
                        RecursoHospitalarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Tipo = c.String(),
                        Componentes = c.String(),
                        Posologia = c.String(),
                        Recomendaciones = c.String(),
                    })
                .PrimaryKey(t => t.RecursoHospitalarioId);
            
            CreateTable(
                "dbo.Bitacoras",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Usuario = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Accion = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UsoRecursoes",
                c => new
                    {
                        UsoRecursoId = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        Cita_CitaId = c.Int(),
                        RecursoHospitalario_RecursoHospitalarioId = c.Int(),
                    })
                .PrimaryKey(t => t.UsoRecursoId)
                .ForeignKey("dbo.Citas", t => t.Cita_CitaId)
                .ForeignKey("dbo.RecursoHospitalarios", t => t.RecursoHospitalario_RecursoHospitalarioId)
                .Index(t => t.Cita_CitaId)
                .Index(t => t.RecursoHospitalario_RecursoHospitalarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsoRecursoes", "RecursoHospitalario_RecursoHospitalarioId", "dbo.RecursoHospitalarios");
            DropForeignKey("dbo.UsoRecursoes", "Cita_CitaId", "dbo.Citas");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Almacens", "RecursoHospitalario_RecursoHospitalarioId", "dbo.RecursoHospitalarios");
            DropForeignKey("dbo.Almacens", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Tratamientoes", "Cita_CitaId", "dbo.Citas");
            DropForeignKey("dbo.Citas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Calendarios", "Medico_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId", "dbo.EspecialidadMedicas");
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Personas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HistoriaMedicas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Citas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Citas", "CitaId", "dbo.Calendarios");
            DropIndex("dbo.UsoRecursoes", new[] { "RecursoHospitalario_RecursoHospitalarioId" });
            DropIndex("dbo.UsoRecursoes", new[] { "Cita_CitaId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tratamientoes", new[] { "Cita_CitaId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.HistoriaMedicas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Personas", new[] { "EspecialidadMedica_EspecialidadMedicaId" });
            DropIndex("dbo.Personas", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Personas", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Calendarios", new[] { "Medico_PersonaId" });
            DropIndex("dbo.Citas", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Citas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.Citas", new[] { "CitaId" });
            DropIndex("dbo.Almacens", new[] { "RecursoHospitalario_RecursoHospitalarioId" });
            DropIndex("dbo.Almacens", new[] { "CentroMedico_CentroMedicoId" });
            DropTable("dbo.UsoRecursoes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Bitacoras");
            DropTable("dbo.RecursoHospitalarios");
            DropTable("dbo.Tratamientoes");
            DropTable("dbo.EspecialidadMedicas");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.HistoriaMedicas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Personas");
            DropTable("dbo.Calendarios");
            DropTable("dbo.Citas");
            DropTable("dbo.CentroMedicoes");
            DropTable("dbo.Almacens");
        }
    }
}
