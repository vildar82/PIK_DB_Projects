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
    
    public partial class Area_group
    {
        public int Area_group_id { get; set; }
        public Nullable<int> Area_group_id_access { get; set; }
        public int Area_ext_id { get; set; }
        public int Area_int_id { get; set; }
        public int Area_int_number { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Area Area1 { get; set; }
    }
}