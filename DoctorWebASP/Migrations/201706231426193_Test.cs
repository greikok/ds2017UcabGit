namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Citas", name: "Evento_CitaId", newName: "Evento_CalendarioId");
            RenameIndex(table: "dbo.Citas", name: "IX_Evento_CitaId", newName: "IX_Evento_CalendarioId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Citas", name: "IX_Evento_CalendarioId", newName: "IX_Evento_CitaId");
            RenameColumn(table: "dbo.Citas", name: "Evento_CalendarioId", newName: "Evento_CitaId");
        }
    }
}
