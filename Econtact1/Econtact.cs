using Econtact1.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Econtact1
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();


        


        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            //GET value from input fields
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact Successfully Inserted");

            }
            else
            {
                MessageBox.Show("Failed to add New Contact.Try Again.");

            }
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            //load data on DataGridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Clear()
        {
            txtBoxFirstName.Text = "";
            txtBoxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
            txtBoxContactID.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ////Get data

            c.ContactID = int.Parse(txtBoxContactID.Text);
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;


            bool success = c.Update(c);
            if(success==true)
            {
                MessageBox.Show("Contact has been successfully updated");

                //load data on DataGridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call Clear Method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Update  Contact.Try Again.");

            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get data from data grid view load it into texboxes
            //identify rows

            int rowIndex = e.RowIndex;
            txtBoxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call CLear Method Here
            Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {

            //Get data from datagridview from the Application


            c.ContactID = Convert.ToInt32(txtBoxContactID.Text);
            bool success = c.Delete(c);

            if (success == true)
            {    //successfully deleted
                MessageBox.Show(" Contact Successfully deleted");
                //load data on DataGridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // Call clear Method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete Contact.Try Again.");

            }
            
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

            //GET Value from textbox

            string keyword = txtBoxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE'%" + keyword + "%'OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;

        }
    }
}
