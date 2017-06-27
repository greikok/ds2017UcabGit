namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBitacora : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bitacoras",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Usuario = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Accion = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bitacoras");
        }
    }
}
