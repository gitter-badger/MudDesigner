using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//MudEngine
using MUDEngine.FileSystem;
using MUDEngine.Objects;
using MUDEngine.Objects.Environment;

namespace CurrencyEditor
{
    public partial class frmMain : Form
    {
        Currency _Currency;

        public frmMain()
        {
            InitializeComponent();
            _Currency = new Currency();
            propertyGrid1.SelectedObject = _Currency;
            foreach (string currency in System.IO.Directory.GetFiles(MUDEngine.Engine.GetDataPath(MUDEngine.Engine.SaveDataTypes.Currency), "*.xml"))
            {
                lstCurrencies.Items.Add(System.IO.Path.GetFileNameWithoutExtension(currency));
            }
        }

        private void btnNewCurrency_Click(object sender, EventArgs e)
        {
            _Currency = new Currency();
            propertyGrid1.SelectedObject = _Currency;
        }

        private void btnSaveCurrency_Click(object sender, EventArgs e)
        {
            if (lstCurrencies.Items.Contains(_Currency.Name))
            {
                MessageBox.Show("Currency already exists!", "Currency Creation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileSystem.Save(Application.StartupPath + @"\Data\Currency\" + _Currency.Name + ".xml", _Currency);
            lstCurrencies.Items.Add(_Currency.Name);
        }

        private void lstCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nothing selected.
            if (lstCurrencies.SelectedIndex == -1)
                return;

            _Currency = (Currency)FileSystem.Load(Application.StartupPath + @"\Data\Currency\" + lstCurrencies.SelectedItem.ToString() + ".xml", _Currency);
            propertyGrid1.SelectedObject = _Currency;
        }

        private void btnDeleteCurrency_Click(object sender, EventArgs e)
        {
            //Check if a currency is selected.
            if (lstCurrencies.SelectedIndex == -1)
            {
                MessageBox.Show("Select a currency to delete first!", "Currency Deletion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Ask if its ok to delete first.
            DialogResult result = MessageBox.Show("Are you sure you want to delete " + _Currency.Name + "?",
                "Currency Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            //Delete the files and remove from the list.
            System.IO.File.Delete(Application.StartupPath + @"\Data\Currency\" + lstCurrencies.SelectedItem.ToString() + ".xml");
            lstCurrencies.Items.Remove(lstCurrencies.SelectedItem);

            //Re-instance the currency and set it within the propertygrid.
            _Currency = new Currency();
            propertyGrid1.SelectedObject = _Currency;
        }
    }
}
