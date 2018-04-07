using InLogFrota.Data.Configurations.Base;
using InLogFrota.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InLogFrota.Data.Configurations
{
    public class VeiculoConfig : IEntityTypeConfiguration<Veiculo>, IConfiguring
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.Property(c => c.Id);

            builder.Property(c => c.Chassi)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Cor)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.NumeroPassageiros)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .IsRequired();
        }
    }
}
