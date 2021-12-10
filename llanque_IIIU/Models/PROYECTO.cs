namespace llanque_IIIU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    [Table("PROYECTO")]
    public partial class PROYECTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROYECTO()
        {
            PERSONA_PROYECTO = new HashSet<PERSONA_PROYECTO>();
        }

        [Key]
        public int ID_PROYECTO { get; set; }

        public int? ID_LINEA { get; set; }

        public int? ID_USUARIO { get; set; }

        [StringLength(250)]
        public string NOMBRE { get; set; }

        [Column(TypeName = "text")]
        public string DESCRIPCION { get; set; }

        [StringLength(1)]
        public string ESTADO { get; set; }

        public virtual LINEA_INVESTIGACION LINEA_INVESTIGACION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONA_PROYECTO> PERSONA_PROYECTO { get; set; }

        public virtual USUARIO USUARIO { get; set; }

        public static Func<List<PROYECTO>> getProjects = () => {
            Model1 context = new Model1();
            return context.PROYECTO.ToList();
        };

        public static Func<int, PROYECTO> getProjectById = (int _id) => {
            Model1 context = new Model1();
            return context.PROYECTO.Find(_id);
        };

        public static Func<PROYECTO, bool> createProject = (PROYECTO _proyecto) => {
            try
            {
                Model1 context = new Model1();
                if (_proyecto.ID_PROYECTO > 0)
                {
                    context.Entry(_proyecto).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(_proyecto).State = EntityState.Added;
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
               // throw ex;
                return false;

            }
        };



        public static bool changeState(int cod)
        {
            try
            {
                Model1 context = new Model1();
                PROYECTO proyecto = context.PROYECTO.Find(cod);
                proyecto.ESTADO = "0";
                context.Entry(proyecto).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool changeStateAvailable(int iD_PROYECTO)
        {
            try
            {
                Model1 context = new Model1();
                PROYECTO proyecto = context.PROYECTO.Find(iD_PROYECTO);
                proyecto.ESTADO = "1";
                context.Entry(proyecto).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Func<int, bool> Eliminar = (int _id) => {
            try
            {
                Model1 context = new Model1();
                if (_id > 0)
                {
                    var proyecto = context.PROYECTO.Find(_id);
                    context.Entry(proyecto).State = EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        };
    }
}
