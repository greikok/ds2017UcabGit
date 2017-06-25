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
            AlterColumn("dbo.RecursoHospitalarios", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.RecursoHospitalarios", "Descripcion", c => c.String(nullable: false));
            DropColumn("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId", c => c.Int());
            AlterColumn("dbo.RecursoHospitalarios", "Descripcion", c => c.String());
            AlterColumn("dbo.RecursoHospitalarios", "Nombre", c => c.String());
            CreateIndex("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId");
            AddForeignKey("dbo.RecursoHospitalarios", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId");
        }
    }
}
