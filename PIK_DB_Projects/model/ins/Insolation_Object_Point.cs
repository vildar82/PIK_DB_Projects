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
    
    public partial class Insolation_Object_Point
    {
        public int Insolation_Object_Point_id { get; set; }
        public int Insolation_Object_id { get; set; }
        public Nullable<int> Insolation_type_id { get; set; }
        public Nullable<int> Point_row { get; set; }
        public Nullable<int> Point_column { get; set; }
    
        public virtual Insolation_Object Insolation_Object { get; set; }
        public virtual Insolation_type Insolation_type { get; set; }
    }
}
