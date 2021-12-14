using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCome.Models;

namespace TimeCome.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        
        public DbSet<MyTask> MyTasks { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<TaskFile> TaskFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var user = new ApplicationUser {
                Id =                    "9cabe323-1c38-4e7d-a6c7-e089525e4c5d",
                UserName =              "admin123@ukr.net",
                NormalizedUserName =    "ADMIN123@UKR.NET",
                Email =                 "admin123@ukr.net",
                NormalizedEmail =       "ADMIN123@UKR.NET",
                EmailConfirmed =        true,
                PasswordHash =          "AQAAAAEAACcQAAAAEDzO5m4JfpUrzKT4Rx09wJBwirycpi+dVRGAdD+rzMA03ahQf9UYNkZNB3AeYoX50Q==", // Admin_123
                SecurityStamp =         "QRYXP27GFXH75UU2TLVEM77V3EL3KMJT",
                ConcurrencyStamp =      "88cf51ba-55bf-4683-b1b2-b8a0f08caef0",
                PhoneNumber =           null,
                PhoneNumberConfirmed =  false,
                TwoFactorEnabled =      false,
                LockoutEnd =            null,
                LockoutEnabled =        true,
                AccessFailedCount=      0
            };
            var priorities = new Priority[]
            {
                new Priority { Id = 1, Name = "low" },
                new Priority { Id = 2, Name = "medium" },
                new Priority { Id = 3, Name = "high" }
            };
            var hashtags = new Hashtag[]
            {
                new Hashtag { Id = 1, Name = "#test" },
                new Hashtag { Id = 2, Name = "#myHashtag" },
                new Hashtag { Id = 3, Name = "#todo" },
                new Hashtag { Id = 4, Name = "#home" },
                new Hashtag { Id = 5, Name = "#work" },
                new Hashtag { Id = 6, Name = "#homework" },
                new Hashtag { Id = 7, Name = "#my" }
            };
            var mytasks = new MyTask[]
            {
                new MyTask { Id = 1, About = "task1", Comment = "test", CreateTime = DateTime.Now, EndTime = DateTime.Now.AddDays(7), Status = false, ApplicationUserId = user.Id, PriorityId = 1 },
                new MyTask { Id = 2, About = "task2", Comment = "test", CreateTime = DateTime.Now, EndTime = DateTime.Now.AddDays(7), Status = false, ApplicationUserId = user.Id, PriorityId = 1 }
            };
            //builder.Entity<ApplicationUser>().HasKey(u => u.Id);
            builder.Entity<ApplicationUser>().HasData(user);
            //builder.Entity<Priority>().HasKey(u => u.Id);
            builder.Entity<Priority>().HasData(priorities);
            //builder.Entity<Hashtag>().HasKey(u => u.Id);
            builder.Entity<Hashtag>().HasData(hashtags);
            //builder.Entity<MyTask>().HasKey(u => u.Id);
            builder.Entity<MyTask>().HasData(mytasks);
            base.OnModelCreating(builder);
        }
    }
}
