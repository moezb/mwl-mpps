using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Edgeteks.MWLServer
{
    public partial class worklistdbForm : Form
    {
        public worklistdbForm()
        {
            InitializeComponent();
        }

        private void examsScheduledBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.examsScheduledBindingSource.EndEdit();
            this.examsScheduledTableAdapter.Update(this.workListDBDataSet.ExamsScheduled);

        }

        private void worklistdbForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'workListDBDataSet.ExamsScheduled' table. You can move, or remove it, as needed.
            this.examsScheduledTableAdapter.Fill(this.workListDBDataSet.ExamsScheduled);
            // TODO: This line of code loads data into the 'workListDBDataSet.ExamsScheduled' table. You can move, or remove it, as needed.
            this.examsScheduledTableAdapter.Fill(this.workListDBDataSet.ExamsScheduled);

        }
    }
}