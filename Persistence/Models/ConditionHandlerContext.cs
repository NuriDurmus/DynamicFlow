using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Models
{
    public partial class ConditionHandlerContext : DbContext
    {
        public ConditionHandlerContext()
        {
        }

        public ConditionHandlerContext(DbContextOptions<ConditionHandlerContext> options)
            : base(options)
        {

        }

        public virtual DbSet<ConditionActions> ConditionAction { get; set; }
        public virtual DbSet<ConditionSet> ConditionSet { get; set; }
        public virtual DbSet<Condition> Condition { get; set; }
        public virtual DbSet<MasterConditionSet> MasterConditionSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=ConditionHandler;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConditionActions>(entity =>
            {
                entity.Property(e => e.ActionName).HasMaxLength(250);


                entity.HasOne(d => d.MasterConditionSet)
                    .WithMany(p => p.ConditionActions)
                    .HasForeignKey(d => d.MasterConditionSetId)
                    .HasConstraintName("FK_ConditionActions_MasterConditionSet");
            });

            modelBuilder.Entity<ConditionSet>(entity =>
            {
                entity.Property(e => e.MasterConditionSetId).HasColumnName("MasterConditionISetd");

                entity.HasOne(d => d.MasterConditionSet)
                    .WithMany(p => p.ConditionSet)
                    .HasForeignKey(d => d.MasterConditionSetId)
                    .HasConstraintName("FK_ConditionSet_MasterConditionSet");
            });

            modelBuilder.Entity<Condition>(entity =>
            {
                entity.Property(e => e.ConditionName).HasMaxLength(250);

                entity.Property(e => e.ConditionPropertyValue).HasMaxLength(250);

                entity.Property(e => e.PropertyName).HasMaxLength(250);

                entity.HasOne(d => d.ConditionSet)
                    .WithMany(p => p.Conditions)
                    .HasForeignKey(d => d.ConditionSetId)
                    .HasConstraintName("FK_Conditions_ConditionSet1");
            });

            modelBuilder.Entity<MasterConditionSet>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public void SeedDatabase(ConditionHandlerContext context)
        {
            if (context.MasterConditionSet.Any()) return;
            var masterConditionSet = new MasterConditionSet()
            {
                Name = "Kişi Kontrolü",
                Description = "Kişi kontrolü yaparak yetkilendirme yapar ve mail gönderir",
            };
            context.MasterConditionSet.Add(masterConditionSet);
            context.SaveChanges();
            var conditionSet = new ConditionSet()
            {
                MasterConditionSetId = masterConditionSet.Id
            };
            context.ConditionSet.Add(conditionSet);
            context.SaveChanges();
            var condition = new Condition()
            {
                ConditionSetId = conditionSet.Id,
                PropertyName = "PersonName",
                ConditionName = "Contains",
                ConditionValue = "Nuri",
                ConditionPropertyValue = ""
            };
            context.Condition.Add(condition);
            context.SaveChanges();


            var condition2 = new Condition()
            {
                ConditionSetId = conditionSet.Id,
                PropertyName = "Age",
                ConditionName = "Bigger",
                ConditionValue = "18",
                ConditionPropertyValue = ""
            };
            context.Condition.Add(condition2);
            context.SaveChanges();


            var conditionAction = new ConditionActions()
            {
                ActionName = "EmailService.SendEmail",
                ActionParameterValues = "testbody,testsubject",
                CronExpression = "",
                RetryCount = 0,
                OrderId = 0,
                StopOnException = true,
                MasterConditionSetId = masterConditionSet.Id
            };
            context.ConditionAction.Add(conditionAction);
            context.SaveChanges();
        }
    }


}
