//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PIK_DB_Projects.model.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project_type()
        {
            this.Objects = new HashSet<Objects>();
        }
    
        public int Project_type_id { get; set; }
        public Nullable<int> Project_type_id_access { get; set; }
        public string Project_type1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Objects> Objects { get; set; }
    }
}
