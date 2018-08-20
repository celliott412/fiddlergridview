using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiddlerGridView.Test
{
    public partial class FormMain : Form
    {
        private GridViewRequestInspector _inspector;

        public FormMain()
        {
            InitializeComponent();
            _inspector = new GridViewRequestInspector();
            _inspector.AddToTab(tabPage1);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            _inspector.headers = new Fiddler.HTTPRequestHeaders("", new string[] { "Content-Type: " + cbContentType.Text });
            _inspector.body = Encoding.UTF8.GetBytes(txtInput.Text);
        }
    }
}
