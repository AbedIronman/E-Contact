using EContact.econtactClasses;
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

namespace EContact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        ContactClass c = new ContactClass();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // get the value from the input field
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            // insert data into database using the method we created in ContactClass
            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact Successfully Inserted");
                // after this message box, i want to clear the textfields
                Clear();
            }
            else
                MessageBox.Show("Failed to add new contact. Try again.");

            // Load data on data GridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }
        

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // method to clear fields
        public void Clear() 
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
            textboxContactID.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // get the data from text boxes
            c.ContactID =  int.Parse(textboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            // now update data in database
            bool success = c.Update(c);
            if (success == true) 
            {
                MessageBox.Show("Contact has been updated!");
                // load data on data grid view
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // call for clear method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact. Please try again.");
            }

        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // get the data from data grid view and load it to textboxes

            //1- identify the row which the mouse is clicked on
            int rowIndex = e.RowIndex;
            textboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Call clear method
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(textboxContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Delete Successfull");
                // after delete refresh data gridview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Delete Unsuccessful. Please try again!");
            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        
        private void textboxSearch_TextChanged(object sender, EventArgs e)
        {
            // first get value from the text box
            string keyword = textboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE " +
                "FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" +
                keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt; ;

        }
    }
}
