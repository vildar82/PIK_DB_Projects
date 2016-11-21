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

        public ObjectMDM(int object_id, ObjectTypeEnum objType, string objectName, ObjectMDM parent)
        {
            Id = object_id;
            ObjectType = objType;
            FullName = objectName;
            InnerObjects = new List<ObjectMDM>();
            Parent = parent;
        }

        public ObjectTypeEnum ObjectType { get; set; }        
        public string FullName { get; set; }
        public int Id { get; set; }
        public List<ObjectMDM> InnerObjects { get; set; }

        public ObjectMDM Parent { get; set; }

        /// <summary>
        /// Определение вложенных объектов 
        /// </summary>
        /// <param name="level">Уровень вложенности по типу объекта</param>
        internal void DefineInners(DbSet<Object_cascade> objCascade, ObjectTypeEnum level)
        {
            if (ObjectType >= level) return;
            var innersObjDB = objCascade.Where(w => w.Object_id_parent == Id);
            foreach (var item in innersObjDB)
            {
                var innerObj = new ObjectMDM(item.Object_id, item.Object_type.GetObjectType(), item.Object, this);
                InnerObjects.Add(innerObj);
                innerObj.DefineInners(objCascade, level);
            }
        }
    }
}
