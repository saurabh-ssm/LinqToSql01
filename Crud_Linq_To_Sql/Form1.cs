using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_Linq_To_Sql
{
    public partial class Form1 : Form
    {
        EmployeeInfoDataContext db;

        public Form1()
        {
            InitializeComponent();
        }
        private void ClearTExtBoxes()
        {
            foreach (Control ctr  in this.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox txt = ctr as TextBox;
                    txt.Clear();
                }
            }
            NAMEtextBox.Focus();
        }
        //private static void getRadio()
        //{
        //    string value = "";
        //    if (radioButton1.Checked) value = radioButton1.Text;
        //    else if(radioButton2.Checked) value = radioButton2.Text;
        //    else value=radioButton3.Text;

        //    return value;
        //}

        private string GetGender()
        {
            if (radioButton1.Checked)
            {
                return radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                return radioButton2.Text;
            }
            else
            {
                return radioButton3.Text;
            }
        }

        private void INSERTbutton_Click(object sender, EventArgs e)
        {
            if (NAMEtextBox.Text == "" || AGEtextBox.Text == "" || EMPSALtextBox.Text == "")
            {
                MessageBox.Show("All Field Are Requires", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                db = new EmployeeInfoDataContext();
                EmpInfo einfo = new EmpInfo();

                einfo.name = NAMEtextBox.Text;
                einfo.gender = GetGender();
                einfo.age = int.Parse(AGEtextBox.Text);
                einfo.empsal = int.Parse(EMPSALtextBox.Text);
                db.EmpInfos.InsertOnSubmit(einfo);
                db.SubmitChanges();
                MessageBox.Show("Data has been inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTExtBoxes();
                BindGridView();
            }
        }
        private void BindGridView()
        {
            db = new EmployeeInfoDataContext();
            dataGridView1.DataSource = db.EmpInfos;

        }
        private void CLEARbutton_Click(object sender, EventArgs e)
        {
            ClearTExtBoxes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {


            if (dataGridView1.SelectedRows.Count > 0)
            {
                NAMEtextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                //GENDERtextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                AGEtextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                EMPSALtextBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            else
            {
                MessageBox.Show("Please Select A Proper Row", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //try
            //{

            //    NAMEtextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            //    //GENDERtextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            //    AGEtextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //    EMPSALtextBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Cannot click here");
            //}
        }

        private void UPDATEbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                db = new EmployeeInfoDataContext();
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                EmpInfo einfo = db.EmpInfos.FirstOrDefault(s => s.id == id);
                einfo.name = NAMEtextBox.Text;
               // einfo.gender = GENDERtextBox.Text;
                einfo.age = int.Parse(AGEtextBox.Text);
                einfo.empsal = int.Parse(EMPSALtextBox.Text);
                db.SubmitChanges();
                MessageBox.Show("Data has been Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTExtBoxes();
                BindGridView();
            }
            else
            {
                MessageBox.Show("Please Select a Row","Warning",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void DELETEbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Are you sure to delete data ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    db = new EmployeeInfoDataContext();
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    EmpInfo einfo = db.EmpInfos.FirstOrDefault(s => s.id == id);
                    db.EmpInfos.DeleteOnSubmit(einfo);
                    db.SubmitChanges();
                    MessageBox.Show("Data has been Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTExtBoxes();
                    BindGridView();
                }           
            }
            else
            {
                MessageBox.Show("Please Select a Row", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EXITbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
