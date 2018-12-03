using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiddlerGridView.Test
{
    public partial class FormMain : Form
    {
        #region private
        private GridViewRequestInspector _inspector;

        private void btnApply_Click(object sender, EventArgs e)
        {
            _inspector.headers = new Fiddler.HTTPRequestHeaders("", new string[] { "Content-Type: " + cbContentType.Text });
            _inspector.body = Encoding.UTF8.GetBytes(txtInput.Text);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (dlgLoad.ShowDialog() == DialogResult.OK)
            {
                txtInput.Lines = File.ReadAllLines(dlgLoad.FileName);
            }
        }
        #endregion

        #region public
        public FormMain()
        {
            InitializeComponent();
            _inspector = new GridViewRequestInspector();
            _inspector.AddToTab(tabPage1);
        }
        #endregion
    }
}
