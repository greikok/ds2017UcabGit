namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarClaseEspecialidadMedicaYRelacionConMedico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EspecialidadMedicas",
                c => new
                    {
                        EspecialidadMedicaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadMedicaId);
            
            AddColumn("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId", c => c.Int());
            CreateIndex("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId");
            AddForeignKey("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId", "dbo.EspecialidadMedicas", "EspecialidadMedicaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId", "dbo.EspecialidadMedicas");
            DropIndex("dbo.Personas", new[] { "EspecialidadMedica_EspecialidadMedicaId" });
            DropColumn("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId");
            DropTable("dbo.EspecialidadMedicas");
        }
    }
}
