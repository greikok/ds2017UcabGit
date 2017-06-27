namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGeneroFechaCreacionToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personas", "Genero", c => c.String(nullable: false));
            AddColumn("dbo.Personas", "FechaCreacion", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Personas", "FechaCreacion");
            DropColumn("dbo.Personas", "Genero");
        }
    }
}
