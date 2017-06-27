namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAttributesToRecursoHospitalario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecursoHospitalarios", "Nombre", c => c.String());
            AddColumn("dbo.RecursoHospitalarios", "Descripcion", c => c.String());
            AddColumn("dbo.RecursoHospitalarios", "Tipo", c => c.String());
            AddColumn("dbo.RecursoHospitalarios", "Componentes", c => c.String());
            AddColumn("dbo.RecursoHospitalarios", "Posologia", c => c.String());
            AddColumn("dbo.RecursoHospitalarios", "Recomendaciones", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecursoHospitalarios", "Recomendaciones");
            DropColumn("dbo.RecursoHospitalarios", "Posologia");
            DropColumn("dbo.RecursoHospitalarios", "Componentes");
            DropColumn("dbo.RecursoHospitalarios", "Tipo");
            DropColumn("dbo.RecursoHospitalarios", "Descripcion");
            DropColumn("dbo.RecursoHospitalarios", "Nombre");
        }
    }
}
