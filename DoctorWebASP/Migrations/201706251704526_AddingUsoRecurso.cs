namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUsoRecurso : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropIndex("dbo.RecursoHospitalarios", new[] { "CentroMedico_CentroMedicoId" });
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
            
            AlterColumn("dbo.RecursoHospitalarios", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.RecursoHospitalarios", "Descripcion", c => c.String(nullable: false));
            DropColumn("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId", c => c.Int());
            DropForeignKey("dbo.UsoRecursoes", "RecursoHospitalario_RecursoHospitalarioId", "dbo.RecursoHospitalarios");
            DropForeignKey("dbo.UsoRecursoes", "Cita_CitaId", "dbo.Citas");
            DropIndex("dbo.UsoRecursoes", new[] { "RecursoHospitalario_RecursoHospitalarioId" });
            DropIndex("dbo.UsoRecursoes", new[] { "Cita_CitaId" });
            AlterColumn("dbo.RecursoHospitalarios", "Descripcion", c => c.String());
            AlterColumn("dbo.RecursoHospitalarios", "Nombre", c => c.String());
            DropTable("dbo.UsoRecursoes");
            CreateIndex("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId");
            AddForeignKey("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId");
        }
    }
}
