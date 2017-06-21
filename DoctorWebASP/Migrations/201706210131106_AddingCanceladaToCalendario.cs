namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCanceladaToCalendario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calendarios", "Cancelada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Calendarios", "Cancelada");
        }
    }
}
