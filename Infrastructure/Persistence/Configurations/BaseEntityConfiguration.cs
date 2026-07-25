using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the key and audit columns shared by every entity. Derived
/// configurations should call <c>base.Configure(builder)</c> first.
/// </summary>
public abstract class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedBy).HasMaxLength(256);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(256);
    }
}
