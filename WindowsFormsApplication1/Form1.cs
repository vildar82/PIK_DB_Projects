using PIK_DB_Projects;
using PIK_DB_Projects.Ins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Dictionary<int, List<InsData>> dictInsDatas = new Dictionary<int, List<InsData>>();
        public Form1()
        {            
            InitializeComponent();
            LoadProjects();
        }

        private void LoadProjects()
        {
            var projects = MDMService.GetProjects();
            cbProjects.DataSource = projects;            
        }

        private void cbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbInsDatas.DataSource = null;                
            var selProj = cbProjects.SelectedItem as ProjectMDM;
            if (selProj == null) return;
            List<InsData> insDatas;
            if (!dictInsDatas.TryGetValue(selProj.Id, out insDatas))
            {
                insDatas = DbInsService.Load(selProj.Id);
                dictInsDatas.Add(selProj.Id, insDatas);
            }
            cbInsDatas.DataSource = insDatas;
        }

        private void cbInsDatas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selInsData = cbInsDatas.SelectedItem as InsData;
            if (selInsData == null) return;
            selInsData.ToExcel();
        }
    }
}
