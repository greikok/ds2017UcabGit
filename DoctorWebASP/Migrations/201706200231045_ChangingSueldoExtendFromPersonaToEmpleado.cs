namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingSueldoExtendFromPersonaToEmpleado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personas", "Rol", c => c.String());
            AddColumn("dbo.Personas", "Sueldo1", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Personas", "Sueldo1");
            DropColumn("dbo.Personas", "Rol");
        }
    }
}
