using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RenameEverything
{
    public partial class Form1 : Form
    {
        private RenameRecursive renameRecursive;
        public Form1()
        {
            InitializeComponent();
            renameRecursive = new RenameRecursive();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //search for a directly
            //put the output into textbox1
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                textBoxDirectory.Text = fbd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string directory = textBoxDirectory.Text;
            string from = textBoxFrom.Text;
            string to = textBoxTo.Text;

            //verify we have a valid directory and a keyword
            if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                textBoxOutput.AppendText("Directory and Key Words are required.");
                return;
            }

            if(Directory.Exists(directory) == false)
            {
                textBoxOutput.AppendText("Directory does not exist.");
                return;
            }

            //preform the main event, rename
            List<string> output = renameRecursive.Execute(directory, from, to);

            foreach(var item in output)
            {
                textBoxOutput.AppendText(item);
                textBoxOutput.AppendText(Environment.NewLine);
            }
        }
    }
}
