namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foranea : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Almacens", "CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Almacens", "RecursoHospitalario_RecursoHospitalarioId", "dbo.RecursoHospitalarios");
            DropForeignKey("dbo.Almacens", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropIndex("dbo.Almacens", new[] { "CentroMedicoId" });
            DropIndex("dbo.Almacens", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Almacens", new[] { "RecursoHospitalario_RecursoHospitalarioId" });
            DropColumn("dbo.Almacens", "CentroMedicoId");
            RenameColumn(table: "dbo.Almacens", name: "CentroMedico_CentroMedicoId", newName: "CentroMedicoId");
            AlterColumn("dbo.Almacens", "CentroMedicoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Almacens", "CentroMedicoId");
            AddForeignKey("dbo.Almacens", "CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId", cascadeDelete: true);
            DropColumn("dbo.Almacens", "RecursoHospitalario_RecursoHospitalarioId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Almacens", "RecursoHospitalario_RecursoHospitalarioId", c => c.Int());
            DropForeignKey("dbo.Almacens", "CentroMedicoId", "dbo.CentroMedicoes");
            DropIndex("dbo.Almacens", new[] { "CentroMedicoId" });
            AlterColumn("dbo.Almacens", "CentroMedicoId", c => c.Int());
            RenameColumn(table: "dbo.Almacens", name: "CentroMedicoId", newName: "CentroMedico_CentroMedicoId");
            AddColumn("dbo.Almacens", "CentroMedicoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Almacens", "RecursoHospitalario_RecursoHospitalarioId");
            CreateIndex("dbo.Almacens", "CentroMedico_CentroMedicoId");
            CreateIndex("dbo.Almacens", "CentroMedicoId");
            AddForeignKey("dbo.Almacens", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId");
            AddForeignKey("dbo.Almacens", "RecursoHospitalario_RecursoHospitalarioId", "dbo.RecursoHospitalarios", "RecursoHospitalarioId");
            AddForeignKey("dbo.Almacens", "CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId", cascadeDelete: true);
        }
    }
}
