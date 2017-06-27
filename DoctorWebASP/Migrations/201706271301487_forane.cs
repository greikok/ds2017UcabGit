namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forane : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecursoHospitalarios", "AlmacenId", c => c.Int(nullable: false));
            CreateIndex("dbo.RecursoHospitalarios", "AlmacenId");
            AddForeignKey("dbo.RecursoHospitalarios", "AlmacenId", "dbo.Almacens", "AlmacenId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecursoHospitalarios", "AlmacenId", "dbo.Almacens");
            DropIndex("dbo.RecursoHospitalarios", new[] { "AlmacenId" });
            DropColumn("dbo.RecursoHospitalarios", "AlmacenId");
        }
    }
}
