namespace llanque_IIIU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PERSONA")]
    public partial class PERSONA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERSONA()
        {
            PERSONA_PROYECTO = new HashSet<PERSONA_PROYECTO>();
            USUARIO = new HashSet<USUARIO>();
        }

        [Key]
        public int ID_PERSONA { get; set; }

        [StringLength(8)]
        public string DNI { get; set; }

        [StringLength(250)]
        public string APELLIDOS { get; set; }

        [StringLength(250)]
        public string NOMBRES { get; set; }

        [StringLength(1)]
        public string SEXO { get; set; }

        [StringLength(250)]
        public string EMAIL { get; set; }

        [StringLength(9)]
        public string CELULAR { get; set; }

        [Column(TypeName = "text")]
        public string DIRECCION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONA_PROYECTO> PERSONA_PROYECTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO> USUARIO { get; set; }

        public static Func<int, PERSONA> getPersonaBy = (int _id) => {
            Model1 context = new Model1();
            return context.PERSONA.Find(_id);
        };
    }
}
