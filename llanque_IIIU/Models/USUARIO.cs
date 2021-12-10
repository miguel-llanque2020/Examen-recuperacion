namespace llanque_IIIU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Spatial;

    [Table("USUARIO")]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            PROYECTO = new HashSet<PROYECTO>();
        }

        [Key]
        public int ID_USUARIO { get; set; }

        public int? ID_PERSONA { get; set; }

        [Column("USUARIO")]
        [StringLength(100)]
        public string USUARIO1 { get; set; }

        [StringLength(250)]
        public string CLAVE { get; set; }

        [StringLength(20)]
        public string NIVEL { get; set; }

        [StringLength(1)]
        public string ESTADO { get; set; }

        public virtual PERSONA PERSONA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROYECTO> PROYECTO { get; set; }

        public static Func<List<USUARIO>> getUsers = () => {
            Model1 context = new Model1();
            return context.USUARIO.ToList();
        };

        public static Func<string, USUARIO> getUserByNick = (string _user) => {
            Model1 context = new Model1();
            return context.USUARIO.Where(x => x.USUARIO1 == _user).First();
        };

        public static Func<string, string, bool> login = (string _usuario, string _clave) => {
            Model1 context = new Model1();
            USUARIO usuario = context.USUARIO.Where(x => x.USUARIO1.Equals(_usuario) && x.CLAVE.Equals(_clave)).FirstOrDefault();
            return usuario != null ? true : false;
        };


    }
}
