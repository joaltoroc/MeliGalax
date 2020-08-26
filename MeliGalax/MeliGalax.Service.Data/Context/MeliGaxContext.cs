namespace MeliGalax.Service.Data.Context
{
    using Microsoft.EntityFrameworkCore;

    public class MeliGaxContext : DbContext
    {
        /// <summary>
        /// Entidad de Resultados.
        /// </summary>
        public virtual DbSet<Models.DAO.Resultado> Resultados { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeliGaxContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public MeliGaxContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
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