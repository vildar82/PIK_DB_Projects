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
    
    public partial class Insolation_version
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Insolation_version()
        {
            this.Insolation_Object = new HashSet<Insolation_Object>();
        }
    
        public int Insolation_version_id { get; set; }
        public Nullable<System.DateTime> Insolation_version_datetime { get; set; }
        public string Insolation_version1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Insolation_Object> Insolation_Object { get; set; }
    }
}
