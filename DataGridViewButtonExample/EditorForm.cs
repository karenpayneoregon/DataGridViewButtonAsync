using SqlDataOperations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SqlDataOperations.Classes;

namespace DataGridViewButtonExample
{
    /// <summary>
    /// There are several different ways to handle pushing and popping
    /// data to this form, this is simply one of them that is easy 
    /// to follow.
    /// 
    /// There is no validation e.g. is a field empty or perhaps
    /// in your app there may be constraints e.g. no duplicates so
    /// that would be out of scope of this code sample which focuses
    /// on the DataGridViewButton.
    /// </summary>
    public partial class EditorForm : Form
    {
        private readonly DataRow _row;
        public DataRow dataRow => _row;

        public EditorForm()
        {
            InitializeComponent();
        }
        public EditorForm(DataRow row, List<ContactType> contactList)
        {
            InitializeComponent();

            _row = row;

            cboContactTitle.DataSource = contactList;

            cboContactTitle.SelectedIndex = cboContactTitle
                .FindString(_row.Field<string>("ContactTitle"));

            txtCompanyName.Text = dataRow.Field<string>("CompanyName");
            txtContactName.Text = dataRow.Field<string>("ContactName");
            txtCity.Text = dataRow.Field<string>("City");
            txtCountry.Text = dataRow.Field<string>("Country");

        }
        /// <summary>
        /// No validation done
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            _row.SetField("CompanyName", txtCompanyName.Text);
            _row.SetField("ContactName", txtContactName.Text);
            _row.SetField("ContactTitle", cboContactTitle.Text);

            _row.SetField("ContactTypeIdentifier", 
                ((ContactType)cboContactTitle.SelectedItem).ContactTypeIdentifier);

            _row.SetField("City", txtCity.Text);
            _row.SetField("Country", txtCountry.Text);
        }
    }
}
