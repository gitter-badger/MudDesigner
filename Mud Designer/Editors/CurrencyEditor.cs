using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.Editors
{
    public partial class CurrencyEditor : Form
    {
        Currency _Currency;

        public CurrencyEditor()
        {
            InitializeComponent();
            _Currency = new Currency();
            propertyGrid1.SelectedObject = _Currency;
            foreach (string currency in System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Currency), "*.xml"))
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

            string currencyPath = FileManager.GetDataPath(SaveDataTypes.Currency);
            string currencyFile = System.IO.Path.Combine(currencyPath, _Currency.Filename);
            FileManager.Save(currencyFile, _Currency);
            lstCurrencies.Items.Add(_Currency.Name);
        }

        private void lstCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nothing selected.
            if (lstCurrencies.SelectedIndex == -1)
                return;
            
            string filePath = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Currency), lstCurrencies.SelectedItem.ToString() + ".xml");
            _Currency = (Currency)FileManager.Load(filePath, _Currency);
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
            string filePath = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Currency), lstCurrencies.SelectedItem.ToString() + ".xml");
            System.IO.File.Delete(filePath);
            lstCurrencies.Items.Remove(lstCurrencies.SelectedItem);

            //Re-instance the currency and set it within the propertygrid.
            _Currency = new Currency();
            propertyGrid1.SelectedObject = _Currency;
        }
    }
}
