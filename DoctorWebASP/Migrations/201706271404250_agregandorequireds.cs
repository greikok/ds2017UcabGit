namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregandorequireds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Supervisors", "medico_PersonaId", "dbo.Personas");
            DropIndex("dbo.Supervisors", new[] { "medico_PersonaId" });
            AlterColumn("dbo.Areas", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Areas", "Descripcion", c => c.String(nullable: false));
            AlterColumn("dbo.Areas", "Horarios", c => c.String(nullable: false));
            AlterColumn("dbo.EquipoMedicoes", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.EquipoMedicoes", "Descripcion", c => c.String(nullable: false));
            AlterColumn("dbo.EquipoMedicoes", "Indicaciones", c => c.String(nullable: false));
            AlterColumn("dbo.RecursoHospitalarios", "Tipo", c => c.String(nullable: false));
            AlterColumn("dbo.RecursoHospitalarios", "Componentes", c => c.String(nullable: false));
            AlterColumn("dbo.RecursoHospitalarios", "Posologia", c => c.String(nullable: false));
            AlterColumn("dbo.RecursoHospitalarios", "Recomendaciones", c => c.String(nullable: false));
            AlterColumn("dbo.Supervisors", "Horarios", c => c.String(nullable: false));
            AlterColumn("dbo.Supervisors", "medico_PersonaId", c => c.Int(nullable: false));
            AlterColumn("dbo.Utensilios", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Utensilios", "Descripcion", c => c.String(nullable: false));
            CreateIndex("dbo.Supervisors", "medico_PersonaId");
            AddForeignKey("dbo.Supervisors", "medico_PersonaId", "dbo.Personas", "PersonaId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supervisors", "medico_PersonaId", "dbo.Personas");
            DropIndex("dbo.Supervisors", new[] { "medico_PersonaId" });
            AlterColumn("dbo.Utensilios", "Descripcion", c => c.String());
            AlterColumn("dbo.Utensilios", "Nombre", c => c.String());
            AlterColumn("dbo.Supervisors", "medico_PersonaId", c => c.Int());
            AlterColumn("dbo.Supervisors", "Horarios", c => c.String());
            AlterColumn("dbo.RecursoHospitalarios", "Recomendaciones", c => c.String());
            AlterColumn("dbo.RecursoHospitalarios", "Posologia", c => c.String());
            AlterColumn("dbo.RecursoHospitalarios", "Componentes", c => c.String());
            AlterColumn("dbo.RecursoHospitalarios", "Tipo", c => c.String());
            AlterColumn("dbo.EquipoMedicoes", "Indicaciones", c => c.String());
            AlterColumn("dbo.EquipoMedicoes", "Descripcion", c => c.String());
            AlterColumn("dbo.EquipoMedicoes", "Nombre", c => c.String());
            AlterColumn("dbo.Areas", "Horarios", c => c.String());
            AlterColumn("dbo.Areas", "Descripcion", c => c.String());
            AlterColumn("dbo.Areas", "Nombre", c => c.String());
            CreateIndex("dbo.Supervisors", "medico_PersonaId");
            AddForeignKey("dbo.Supervisors", "medico_PersonaId", "dbo.Personas", "PersonaId");
        }
    }
}
