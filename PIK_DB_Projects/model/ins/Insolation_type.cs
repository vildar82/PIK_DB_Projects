//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PIK_DB_Projects.model.ins
{
    using System;
    using System.Collections.Generic;
    
    public partial class Insolation_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Insolation_type()
        {
            this.Insolation_Object_Point = new HashSet<Insolation_Object_Point>();
        }
    
        public int Insolation_type_id { get; set; }
        public string Insolation_type_description { get; set; }
        public string Insolation_type1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Insolation_Object_Point> Insolation_Object_Point { get; set; }
    }
}
