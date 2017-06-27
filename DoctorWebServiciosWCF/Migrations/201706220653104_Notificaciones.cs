namespace DoctorWebServiciosWCF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notificaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notificacions",
                c => new
                    {
                        NotificacionId = c.Int(nullable: false, identity: true),
                        Estado = c.Byte(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Descripcion = c.String(nullable: false, maxLength: 255),
                        Asunto = c.String(nullable: false, maxLength: 128),
                        Contenido = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NotificacionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notificacions");
        }
    }
}
