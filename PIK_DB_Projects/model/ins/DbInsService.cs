using PIK_DB_Projects.model.ins;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIK_DB_Projects.Ins
{
    /// <summary>
    /// Сервис работы с базой инсоляции
    /// </summary>
    public static class DbInsService
    {
        private static Dictionary<string, int> dictInsValueIds;
        private static InsEntities GetEntities()
        {
            var con = new EntityConnection(Properties.Settings.Default.InsCon);
            return new InsEntities(con);
        }

        /// <summary>
        /// Список данных инсоляций для проекта
        /// </summary>
        /// <param name="projectId">Проект</param>
        /// <returns>Список данных инсоляций проекта</returns>
        public static List<InsData> Load (int projectId)
        {
            if (projectId == 0) return null;
            var resDatas = new List<InsData>();
            using (var ent = GetEntities())
            {
                // Объекты домов для проекта
                var housesDb = MDMService.GetHouses(projectId);
                if (housesDb == null || !housesDb.Any()) return null;                

                // Найти данные инс связанные с этими домами
                // Все объекты инс для домов проекта
                var insObjectsDb = ent.Insolation_Object.ToList().Where(w => housesDb.Any(h => h.Id == w.Object_id)).ToList();                
                // данные объектов инсоляции для каждого объекта (InsVer-версия инс, данные объекта)
                var objectsData = new List<Tuple<Insolation_version, InsObject>>();
                foreach (var objDb in insObjectsDb)
                {
                    var pts = new List<InsPoint>();
                    foreach (var ptDb in objDb.Insolation_Object_Point)
                    {                        
                        pts.Add(new InsPoint
                        {
                            InsValue = ptDb.Insolation_type.Insolation_type1,
                            Row = ptDb.Point_row.Value,
                            Column = ptDb.Point_column.Value,
                        });
                    }
                    var objData = new InsObject { Id = objDb.Object_id, Points = pts };
                    objectsData.Add(new Tuple<Insolation_version, InsObject>(objDb.Insolation_version, objData));
                }
                // группировка по версии инсоляции
                var versionsInsObjects = objectsData.GroupBy(g => g.Item1.Insolation_version_id);
                foreach (var verInsObjects in versionsInsObjects)
                {
                    var insVer = verInsObjects.First().Item1;
                    var insData = new InsData {
                        Date = insVer.Insolation_version_datetime.Value,
                        Name = insVer.Insolation_version1,
                        Objects = verInsObjects.Select(s => s.Item2).ToList()                       
                    };
                    resDatas.Add(insData);
                }                
            }
            // Сортировка по дате
            resDatas.Sort((d1, d2) => DateTime.Compare(d1.Date, d2.Date));
            return resDatas;
        }

        /// <summary>
        /// Парсинг значения точки инс
        /// </summary>
        /// <param name="point_string">Коор точки (строка;столбец)</param>
        /// <returns>Строка, столбец</returns>
        private static Tuple<int, int> ParsePoint(string point_string)
        {
            var split = point_string.Split(';');
            var r = int.Parse(split[0]);
            var c= int.Parse(split[1]);
            var res = new Tuple<int, int>(r, c);
            return res;
        }

        /// <summary>
        /// Сохранение данных инсоляции - одного расчета (группы)
        /// </summary>
        /// <param name="data">Даннын</param>
        public static void Save (InsData data)
        {
            if (data == null) return;
            using (var ent = GetEntities())
            {
                Init(ent);
                // Запись версии
                var insVer= ent.Insolation_version.Add(new Insolation_version {
                    Insolation_version1 = data.Name,
                    Insolation_version_datetime = data.Date
                });

                // Запись объектов инсоляции
                if (data.Objects == null || !data.Objects.Any() ||
                    data.Objects.All(a => a.Points == null || !a.Points.Any()))
                {
                    // Нет данных!?
                    return;
                }

                foreach (var itemObj in data.Objects)
                {
                    // Запись объекта
                    var obj = ent.Insolation_Object.Add(new Insolation_Object {
                        Insolation_version = insVer,
                        Object_id = itemObj.Id
                    });                    
                    // Запись точек инс
                    foreach (var itemPt in itemObj.Points)
                    {                        
                        var pt = ent.Insolation_Object_Point.Add(new Insolation_Object_Point
                        {
                            Insolation_Object = obj,
                            Point_row = itemPt.Row,
                            Point_column = itemPt.Column,
                            Insolation_type_id = GetInsValueId(itemPt.InsValue)
                        });                        
                    }
                }
                ent.SaveChanges();
            }
        }

        private static void Init(InsEntities ent)
        {
            if (dictInsValueIds == null)
            {
                var insValTypes = ent.Insolation_type.ToList();
                dictInsValueIds = insValTypes.ToDictionary(k=>k.Insolation_type1.Trim(), v=>v.Insolation_type_id);
            }
        }

        private static int? GetInsValueId(string insValue)
        {
            int res;
            if (dictInsValueIds.TryGetValue(insValue, out res))
            {
                return res;
            }
            return null;
        }
    }
}
