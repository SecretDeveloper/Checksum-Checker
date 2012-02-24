using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChecksumLib;

namespace ChecksumChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {                
                if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                string filePath = openFileDialog.FileName;
                RunHash(filePath, GetChecksumTypeFromString(this.comboBox1.SelectedItem.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private ChecksumType GetChecksumTypeFromString(string typeString)
        {
            switch (typeString.ToUpper())
            {
                case "SHA1":
                    return ChecksumType.SHA1;
                case "SHA256":
                    return ChecksumType.SHA256;
                case "MD5":
                    return ChecksumType.MD5;
            }
            return ChecksumType.SHA1;
        }

        private void RunHash(string filePath, ChecksumType type)
        {
            this.lblResult.Text = "";
            this.txtSource.Text = filePath;

            Checker ch = new Checker();
            this.txtChecksum.Text = ch.GetChecksum(filePath, type);

            if (txtCompare.Text.Length > 0)
                lblResult.Text = CompareHashes(txtChecksum.Text, txtCompare.Text);
        }

        private void txtCompare_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(txtCompare.Text.Length >0)
                    lblResult.Text = CompareHashes(txtChecksum.Text, txtCompare.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string CompareHashes(string generated, string pasted)
        {
            if (generated.Trim().Replace("-", "").Equals(pasted.Trim().Replace("-", ""), StringComparison.InvariantCultureIgnoreCase))
                return "Hashes match";

            return "Hashes do not match";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblResult.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string filePath = openFileDialog.FileName;
                
                RunHash(filePath, GetChecksumTypeFromString(this.comboBox1.SelectedItem.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
