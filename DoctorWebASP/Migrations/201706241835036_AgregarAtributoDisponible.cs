namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarAtributoDisponible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calendarios", "Disponible", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Calendarios", "Disponible");
        }
    }
}
