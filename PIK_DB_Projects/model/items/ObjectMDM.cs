using PIK_DB_Projects.model.db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIK_DB_Projects
{
    public class ObjectMDM
    {
        public ObjectMDM()
        {

        }

        public ObjectMDM(Object_cascade objDb, ObjectMDM parent)
        {
            Id = objDb.Object_id;
            ObjectType = MDMService.GetObjectType(objDb.Object_type);
            FullName = objDb.Object;
            IsBuilding = objDb.Is_building;
            InnerObjects = new List<ObjectMDM>();
            Parent = parent;
        }

        public ObjectTypeEnum ObjectType { get; set; }        
        public string FullName { get; set; }
        public int Id { get; set; }
        public List<ObjectMDM> InnerObjects { get; set; }
        public bool? IsBuilding { get; set; }

        public ObjectMDM Parent { get; set; }

        /// <summary>
        /// Определение вложенных объектов 
        /// </summary>
        /// <param name="level">Уровень вложенности по типу объекта</param>
        internal void DefineInners(DbSet<Object_cascade> objCascade, ObjectTypeEnum level,ref List<ObjectMDM> levelObjects)
        {
            if (ObjectType > level) return;
            if (ObjectType == level)
            {
                levelObjects.Add(this);
                return;
            }
            var innersObjDB = objCascade.Where(w => w.Object_id_parent == Id);
            foreach (var item in innersObjDB)
            {
                var innerObj = new ObjectMDM(item, this);
                InnerObjects.Add(innerObj);
                innerObj.DefineInners(objCascade, level, ref levelObjects);
            }
        }

        public List<ObjectMDM> GetAllObjects()
        {
            if (InnerObjects == null) return null;
            var resObjs = InnerObjects.ToList();
            foreach (var item in InnerObjects)
            {
                var innerObjs = item.GetAllObjects();
                if (innerObjs != null)
                {
                    resObjs.AddRange(innerObjs);
                }
            }
            return resObjs;
        }
    }
}
