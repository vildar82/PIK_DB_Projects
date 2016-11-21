using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIK_DB_Projects
{
    public enum ObjectTypeEnum
    {
        [Description("Не назначено")]
        None,
        [Description(MDMService.TypeNameProject)]        
        Project,
        [Description(MDMService.TypeNameSite)]
        Site,
        [Description(MDMService.TypeNameQueue)]
        Queue,
        [Description(MDMService.TypeNameBlock)]
        Block,
        [Description(MDMService.TypeNameHouse)]
        House, 
    }
}
