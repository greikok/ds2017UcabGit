namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Almacens", "CentroMedicoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Almacens", "CentroMedicoId");
            AddForeignKey("dbo.Almacens", "CentroMedicoId", "dbo.CentroMedicoes", "CentroMedicoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Almacens", "CentroMedicoId", "dbo.CentroMedicoes");
            DropIndex("dbo.Almacens", new[] { "CentroMedicoId" });
            DropColumn("dbo.Almacens", "CentroMedicoId");
        }
    }
}
