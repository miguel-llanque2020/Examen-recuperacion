namespace llanque_IIIU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Spatial;

    public partial class LINEA_INVESTIGACION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LINEA_INVESTIGACION()
        {
            PROYECTO = new HashSet<PROYECTO>();
        }

        [Key]
        public int ID_LINEA { get; set; }

        [StringLength(250)]
        public string NOMBRE { get; set; }

        [Column(TypeName = "text")]
        public string DESCRIPCION { get; set; }

        public int? ESTADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROYECTO> PROYECTO { get; set; }

        public static Func<List<LINEA_INVESTIGACION>> getLineas = () => {
            Model1 context = new Model1();
            return context.LINEA_INVESTIGACION.ToList();
        };
    }
}
