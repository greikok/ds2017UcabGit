namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingListOfEmpleadosToCentroMedico : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            AddColumn("dbo.Personas", "CentroMedico_CentroMedicoId1", c => c.Int());
            CreateIndex("dbo.Personas", "CentroMedico_CentroMedicoId1");
            AddForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId");
            AddForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId1", "dbo.CentroMedicoes", "CentroMedicoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId1", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropIndex("dbo.Personas", new[] { "CentroMedico_CentroMedicoId1" });
            DropColumn("dbo.Personas", "CentroMedico_CentroMedicoId1");
            AddForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId");
        }
    }
}
