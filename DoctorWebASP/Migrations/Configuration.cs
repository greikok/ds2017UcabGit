namespace DoctorWebASP.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DoctorWebASP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DoctorWebASP.Models.ApplicationDbContext context)
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

            var PasswordHash = new PasswordHasher();

            context.Users.AddOrUpdate(x => x.Id,
                new ApplicationUser() { Id = "1", Email = "usuario1@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario1*"), UserName = "usuario1@gmail.com" },
                new ApplicationUser() { Id = "2", Email = "usuario2@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario2*"), UserName = "usuario2@gmail.com" },
                new ApplicationUser() { Id = "3", Email = "usuario3@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario3*"), UserName = "usuario3@gmail.com" },
                new ApplicationUser() { Id = "4", Email = "usuario4@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario4*"), UserName = "usuario4@gmail.com" },
                new ApplicationUser() { Id = "5", Email = "usuario5@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario5*"), UserName = "usuario5@gmail.com" },
                new ApplicationUser() { Id = "6", Email = "usuario6@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario6*"), UserName = "usuario6@gmail.com" },
                new ApplicationUser() { Id = "7", Email = "usuario7@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario7*"), UserName = "usuario7@gmail.com" },
                new ApplicationUser() { Id = "8", Email = "usuario8@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario8*"), UserName = "usuario8@gmail.com" },
                new ApplicationUser() { Id = "9", Email = "usuario9@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario9*"), UserName = "usuario9@gmail.com" },
                new ApplicationUser() { Id = "10", Email = "usuario10@gmail.com", EmailConfirmed = true, PasswordHash = PasswordHash.HashPassword("Usuario10*"), UserName = "usuario10@gmail.com" });
        }
    }
}
