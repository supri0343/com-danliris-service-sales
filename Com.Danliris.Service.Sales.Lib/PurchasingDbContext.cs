using Com.Danliris.Service.Sales.Lib.Models.PurchasingModel;
using Com.Danliris.Service.Sales.Lib.Models.PurchasingModel.GarmentPurchaseRequest;
using Com.Moonlay.Data.EntityFrameworkCore;
using Com.Moonlay.Data.EntityFrameworkCore.ValueGenerators;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib
{
    public class PurchasingDbContext : DbContext
    {
        public PurchasingDbContext(DbContextOptions<PurchasingDbContext> options) : base(options)
        {
        }

        public DbSet<GarmentPurchaseRequest> GarmentPurchaseRequests { get; set; }
        public DbSet<GarmentPurchaseRequestItem> GarmentPurchaseRequestItems { get; set; }
        public DbSet<LogHistory> LogHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //ConfigureEntities(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<GarmentPurchaseRequest>()
             .HasIndex(i => i.PRNo)
             .IsUnique()
             .HasFilter("[IsDeleted]=(0) AND [CreatedUtc]>CONVERT([datetime2],'2019-10-01 00:00:00.0000000')");

            modelBuilder.Entity<GarmentPurchaseRequest>()
                .HasIndex(i => i.RONo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0) AND [CreatedUtc]>CONVERT([datetime2],'2019-10-01 00:00:00.0000000')");
        }

        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            Type baseType = typeof(IStandardEntity);
            foreach (IStandardEntity item in from typeInfo in GetType().Assembly.DefinedTypes
                                             where baseType.IsAssignableFrom(typeInfo) && !typeInfo.IsAbstract && typeInfo.IsClass
                                             select typeInfo into info
                                             select Activator.CreateInstance(info) as IStandardEntity)
            {
                EntityTypeBuilder builder = modelBuilder.Entity(item.GetType());
                //ConfigureKeys(builder, item);
                ConfigureProperties(builder);
                ConfigureQueryFilter(builder);
            }
        }

        //private void ConfigureKeys(EntityTypeBuilder builder, IStandardEntity entity)
        //{
        //    IEnumerable<string> source = from p in entity.GetType().GetProperties()
        //                                 where p.GetCustomAttribute(typeof(KeyAttribute)) != null
        //                                 select p.Name;
        //    if (source.Count() > 0)
        //    {
        //        builder.Property("Id").Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        //        builder.HasKey(source.ToArray());
        //    }
        //    else
        //    {
        //        builder.HasKey("Id");
        //    }

        //    if (entity is IEntity<string>)
        //    {
        //        builder.Property("Id").HasMaxLength(32).HasValueGenerator<StringValueGenerator>();
        //    }
        //    else if (entity is IEntity<Guid>)
        //    {
        //        builder.Property("Id").HasValueGenerator<GuidValueGenerator>();
        //    }
        //}

        private void ConfigureProperties(EntityTypeBuilder builder)
        {
            builder.Property("LastModifiedBy").IsRequired().HasMaxLength(255);
            builder.Property("LastModifiedAgent").IsRequired().HasMaxLength(255);
            builder.Property("CreatedBy").IsRequired().HasMaxLength(255);
            builder.Property("CreatedAgent").IsRequired().HasMaxLength(255);
            builder.Property("DeletedBy").IsRequired().HasMaxLength(255);
            builder.Property("DeletedAgent").IsRequired().HasMaxLength(255);
        }

        private static void ConfigureQueryFilter(EntityTypeBuilder builder)
        {
            ParameterExpression parameterExpression = Expression.Parameter(builder.Metadata.ClrType, "IsDeleted");
            MemberExpression left = Expression.Property(parameterExpression, "IsDeleted");
            ConstantExpression right = Expression.Constant(false);
            BinaryExpression body = Expression.Equal(left, right);
            builder.HasQueryFilter(Expression.Lambda(body, parameterExpression));
        }
    }
    
    
}
