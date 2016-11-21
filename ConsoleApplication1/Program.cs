using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIK_DB_Projects;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main (string[] args)
        {
            // Список проектов
            var projects = MDMService.GetProjects();
            foreach (var item in projects)
            {
                Console.WriteLine($"{item.Id} - {item.Name}");
            }
            Console.ReadKey();
        }
    }
}
