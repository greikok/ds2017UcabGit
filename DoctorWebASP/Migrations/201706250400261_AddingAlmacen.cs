namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAlmacen : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Almacens", "RecursoHospitalario_RecursoHospitalarioId", "dbo.RecursoHospitalarios");
            DropForeignKey("dbo.Almacens", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropIndex("dbo.Almacens", new[] { "RecursoHospitalario_RecursoHospitalarioId" });
            DropIndex("dbo.Almacens", new[] { "CentroMedico_CentroMedicoId" });
            DropTable("dbo.Almacens");
        }
    }
}
