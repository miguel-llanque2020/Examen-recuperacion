namespace llanque_IIIU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    public partial class PERSONA_PROYECTO
    {
        [Key]
        public int ID_PERSONA_PROYECTO { get; set; }

        public int? ID_PERSONA { get; set; }

        public int? ID_PROYECTO { get; set; }

        public virtual PERSONA PERSONA { get; set; }

        public virtual PROYECTO PROYECTO { get; set; }

        public static Func<PERSONA_PROYECTO, bool> createProject = (PERSONA_PROYECTO _obj) => {
            try
            {
                Model1 context = new Model1();
                if (_obj.ID_PERSONA_PROYECTO > 0)
                {
                    context.Entry(_obj).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(_obj).State = EntityState.Added;
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        };

        public static Func<List<PERSONA_PROYECTO>> getAllMembers = () => {
            Model1 context = new Model1();
            return context.PERSONA_PROYECTO.ToList();
        };

        public static Func<int, List<PERSONA_PROYECTO>> getPersona_ProyectoByProject = (int _id) => {
            Model1 context = new Model1();
            return context.PERSONA_PROYECTO.Where(x => x.ID_PROYECTO == _id).ToList();
        };

        public static bool deleteMember(int cod)
        {
            try
            {
                Model1 context = new Model1();
                PERSONA_PROYECTO _obj = context.PERSONA_PROYECTO.Find(cod);
                if (_obj.ID_PERSONA_PROYECTO > 0)
                {
                    context.Entry(_obj).State = EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
