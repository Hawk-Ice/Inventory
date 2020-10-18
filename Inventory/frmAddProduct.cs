﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        BindingSource showProductList;
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;
        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }
        

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"   
            };

            foreach (string category in ListOfProductCategory) {
                cbCategory.Items.Add(category);
            }
        }
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new StringFormatException("Invalid Product Name");
            }
            return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            {
                throw new NumberFormatException("Invalid quantity");
            }
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Invalid price");
            }
            return Convert.ToDouble(price);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
             catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
    [Serializable]
    class NumberFormatException : Exception
    {
        public NumberFormatException()
        {

        }

        public NumberFormatException(string number)
            : base(String.Format("Invalid Quantity: {0}", number))
        {

        }

    }
    [Serializable]
    class StringFormatException : Exception
    {
        public StringFormatException()
        {

        }

        public StringFormatException(string name)
            : base(String.Format("Invalid Product Name: {0}", name))
        {

        }

    }
    [Serializable]
    class CurrencyFormatException : Exception
    {
        public CurrencyFormatException()
        {

        }

        public CurrencyFormatException(string currency)
            : base(String.Format("Invalid Currency: {0}", currency))
        {

        }

    }
}
