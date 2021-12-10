using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace llanque_IIIU.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model16")
        {
        }

        public virtual DbSet<LINEA_INVESTIGACION> LINEA_INVESTIGACION { get; set; }
        public virtual DbSet<PERSONA> PERSONA { get; set; }
        public virtual DbSet<PERSONA_PROYECTO> PERSONA_PROYECTO { get; set; }
        public virtual DbSet<PROYECTO> PROYECTO { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LINEA_INVESTIGACION>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<LINEA_INVESTIGACION>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONA>()
                .Property(e => e.DNI)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONA>()
                .Property(e => e.APELLIDOS)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONA>()
                .Property(e => e.NOMBRES)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONA>()
                .Property(e => e.SEXO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PERSONA>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONA>()
                .Property(e => e.CELULAR)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONA>()
                .Property(e => e.DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<PROYECTO>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<PROYECTO>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PROYECTO>()
                .Property(e => e.ESTADO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.USUARIO1)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.CLAVE)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.NIVEL)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.ESTADO)
                .IsUnicode(false);
        }
    }
}
