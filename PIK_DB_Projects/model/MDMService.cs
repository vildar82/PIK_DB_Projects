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
                var projects = ents.Object_cascade.Where(w => w.Object_type == TypeNameProject)
                    .Select(s => new ProjectMDM
                    {
                        Name = s.Object,
                        Id = s.Object_id
                    }).ToList();
                return projects;
            }
        }                

        /// <summary>
        /// Иерархия объекта
        /// </summary>        
        /// <param name="level">Уровень вложенности</param>
        public static List<ObjectMDM> GetObjects (int objectId, ObjectTypeEnum level)
        {
            if (objectId == 0) return null;
            var resObjs = new List<ObjectMDM>();
            using (var ents = GetEntities())
            {   
                // Начальный объект
                var obj = ents.Object_cascade.FirstOrDefault(o => o.Object_id == objectId);
                if (obj == null) return null;                
                // Получение вложенных объектов
                var objMdm = new ObjectMDM(obj, null);
                objMdm.DefineInners(ents.Object_cascade, level, ref resObjs);                
            }
            return resObjs;
        }             

        public static List<ObjectMDM> GetHouses (int parentId)
        {
            if (parentId == 0) return null;
            var resObjs = new List<ObjectMDM>();
            using (var ents = GetEntities())
            {
                // Начальный объект
                var obj = ents.Object_cascade.FirstOrDefault(o => o.Object_id == parentId);
                if (obj == null) return null;                
                // Получение вложенных объектов нужного уровня (Корпус)
                var objParent = new ObjectMDM(obj, null);
                objParent.DefineInners(ents.Object_cascade,  ObjectTypeEnum.House, ref resObjs);

                // В найденных корпусах оставить только те, которые являются зданиями
                resObjs = resObjs.Where(w => w.IsBuilding == true).ToList();

                // Среди дочерних элементов найти блоки у которые являются домами
                var allInnersObjs = objParent.GetAllObjects();
                if (allInnersObjs != null)
                {
                    var blockBuilds = allInnersObjs.Where(w => w.ObjectType == ObjectTypeEnum.Block && w.IsBuilding== true);
                    if (blockBuilds.Any())
                    {
                        resObjs.AddRange(blockBuilds);
                    }
                }
            }
            return resObjs;
        }

        public static string GetObjectType (this ObjectTypeEnum objType)
        {
            return objType.Description();            
        }

        public static ObjectTypeEnum GetObjectType (this string objType)
        {
            switch (objType)
            {
                case TypeNameProject:
                    return ObjectTypeEnum.Project;
                case TypeNameSite:
                    return ObjectTypeEnum.Site;
                case TypeNameQueue:
                    return ObjectTypeEnum.Queue;
                case TypeNameBlock:
                    return ObjectTypeEnum.Block;
                case TypeNameHouse:
                    return ObjectTypeEnum.House;
                default:
                    throw new Exception($"Неверный тип проекта - {objType}.");
            }
        }
    }    
}
