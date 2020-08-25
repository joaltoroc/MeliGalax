namespace MeliGalax.Service.Data.Context
{
    using Microsoft.EntityFrameworkCore;

    public class MeliGaxContext : DbContext
    {
        public virtual DbSet<Models.DAO.Resultado> Resultados { get; set; }

        public MeliGaxContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.DAO.Resultado>(entity =>
            {
                entity.ToTable("Resultado", "data");

                entity.HasKey(k => k.Id);

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasDefaultValueSql("NEWID()");

                entity.Ignore(i => i.Perimetro);

                entity.Ignore(i => i.Clima);
            });
        }
    }
}