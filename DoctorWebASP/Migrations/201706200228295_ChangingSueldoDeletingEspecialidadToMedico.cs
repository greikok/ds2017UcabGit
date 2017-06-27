namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingSueldoDeletingEspecialidadToMedico : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Personas", "Sueldo", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Personas", "Especialidad");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personas", "Especialidad", c => c.String());
            AlterColumn("dbo.Personas", "Sueldo", c => c.Double());
        }
    }
}
