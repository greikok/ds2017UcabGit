namespace DoctorWebServiciosWCF.Migrations
{
    using DoctorWebServiciosWCF.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DoctorWebServiciosWCF.Model.ORM.ContextoBD>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DoctorWebServiciosWCF.Model.ORM.ContextoBD context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            #region Grupo 9
            context.Notificaciones.AddOrUpdate(
                notificacion => notificacion.Nombre,
                new Notificacion
                {
                    Nombre = "Echo",
                    Descripcion = "Esta notificacion no tiene otro proposito mas que probar el servicio de correos",
                    Asunto = "Notificacion Echo",
                    Estado = NotificacionEstado.Disponible,
                    Contenido = "ECHO {{FechaActual}} ECHO"
                },
                new Notificacion
                {
                    Nombre = "Saludo {{Nombre}}",
                    Descripcion = "Permite enviar un saludo a un destinatario. Se espera el nombre del destinatario.",
                    Asunto = "DoctorWeb : Saludo",
                    Estado = NotificacionEstado.Disponible,
                    Contenido = "Muy buen dia mi amigo {{nombre}}, tengo el gusto de saludarle y desearle un feliz dia."
                }
            );
            #endregion
        }
    }
}
