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
    
    public partial class Object_Area_Item
    {
        public int Object_Area_Item_id { get; set; }
        public Nullable<int> Object_Area_id { get; set; }
        public Nullable<int> Object_Item_id { get; set; }
    
        public virtual Object_Area Object_Area { get; set; }
        public virtual Object_Item Object_Item { get; set; }
    }
}
