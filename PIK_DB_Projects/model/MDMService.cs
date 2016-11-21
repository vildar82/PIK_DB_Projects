using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIK_DB_Projects.model.db;
using NetLib;

namespace PIK_DB_Projects
{
    public static class MDMService
    {
        public const string TypeNameProject = "Проект";
        public const string TypeNameSite = "Площадка";
        public const string TypeNameQueue = "Очередь";
        public const string TypeNameBlock = "Блок";
        public const string TypeNameHouse = "Корпус";

        private static MDMEntities GetEntities()
        {
            var con = new EntityConnection(Properties.Settings.Default.MdmCon);
            return new MDMEntities(con);
        }

        public static List<ProjectMDM> GetProjects ()
        {
            using (var ents = GetEntities())
            {
                var projects = ents.Objects.Where(w => w.Object_type.Object_type1 == TypeNameProject)
                    .Select(s => new ProjectMDM
                    {
                        Name = s.Object_name,
                        Id = s.Object_id
                    }).ToList();
                return projects;
            }
        }                

        /// <summary>
        /// Иерархия объекта
        /// </summary>        
        public static List<ObjectMDM> GetObjects (int objectId, ObjectTypeEnum level)
        {
            if (objectId == 0) return null;
            var resObjs = new List<ObjectMDM>();
            using (var ents = GetEntities())
            {   
                // Начальный объект
                var obj = ents.Object_cascade.FirstOrDefault(o => o.Object_id == objectId);
                if (obj == null) return null;
                var objType = obj.Object_type.GetObjectType();                
                // Получение вложенных объектов
                var objMdm = new ObjectMDM(obj.Object_id, objType, obj.Object, null);
                objMdm.DefineInners(ents.Object_cascade, level);
                resObjs = objMdm.InnerObjects;
            }
            return resObjs;
        }             

        public static string GetObjectType (this ObjectTypeEnum objType)
        {
            return objType.Description();            
        }

        public static ObjectTypeEnum GetObjectType (this string objType)
        {
            return objType.ToEnum<ObjectTypeEnum>();
        }
    }    
}
