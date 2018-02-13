using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;



namespace Asg2_sxr178830
{

    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        List<string> list1 = new List<string>();// creation of a global list
        public object result { get => result; set => result = value; }
        public int SelectedItemIndex { get; private set; }
        

        private void button1_Click(object sender, EventArgs e)
        {
            
            DateTime localDate = DateTime.UtcNow;      // this is used to have a default value in the date field

            svtm.Text = localDate.ToString("HH:mm:ss");
            //the following commands are used to save the data entered  by the user
            list1.Add(fn.Text);
            list1.Add(mi.Text);
            list1.Add(ln.Text);
            list1.Add(phn.Text);
            list1.Add(ad1.Text);
            list1.Add(ad2.Text);
            list1.Add(ct.Text);
            list1.Add(st.Text);
            list1.Add(zip.Text);
            list1.Add(eid.Text);
            list1.Add(proof.Text);
            list1.Add(dtrecv.Text);
            list1.Add(sttm.Text);
            list1.Add(svtm.Text);

            filesaving();// a new method of the class has been called for saving of the newly entered data into the text file
            
            var result = String.Join("  ", list1.ToArray());

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this is the loading of the different elements in gui when the form is loaded
            this.AcceptButton = button1;
            DateTime localDate = DateTime.Now;
            dtrecv.Text = localDate.Date.ToString("d");     //this sets the date received by default to today
            ListViewItem item = new ListViewItem(new[] { "1", "2" });
            ListViewItem.ListViewSubItem list = new ListViewItem.ListViewSubItem();
            listView1.Items.Add(item);
            string[] readText = File.ReadAllLines("CS6326Asg2.txt");// reads the text file and inputs the data of the text file to the list view

            foreach (string s in readText)
            {
                if (s != "\r\n")
                {
                    char[] delimiterChars = { '\t' };
                    string name = "", phone = "";

                    string[] words = s.Split(delimiterChars);

                    name = words[0] + " " + words[1] + " " + words[2];
                    phone = words[3];
                    listView1.Items.Add(name).SubItems.Add(phone);

                }
                else
                {
                    continue;
                }

            }
            button2.Enabled = false;    //disabling of the clear and delete buttons
            button3.Enabled = false;




        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string xyz = listView1.SelectedItems[0].SubItems[0].Text;
                char[] delimiterChars = { ' ', '\t' };
                string[] words = xyz.Split(delimiterChars);
                string random = words[0];
                searchingfromfile(random);// getting the first name as the key value and then searching the file for the details
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearTbs(this.Controls);// this is the clear button which when clicked call the method 
            DateTime localDate = DateTime.UtcNow;      
            sttm.Text = localDate.ToString("HH:mm:ss");
        }


        public void ClearTbs(Control.ControlCollection cc)
        {
            foreach (Control ctrl in cc)
            {
                TextBox tbs = ctrl as TextBox;
                if (tbs != null)
                    tbs.Text = "";
                else
                    ClearTbs(ctrl.Controls);
            }
            DateTime localDate = DateTime.Now;
            dtrecv.Text = localDate.Date.ToString("d");

        }

        public void button3_Click(object sender, EventArgs e)
        {

            // this is the delete button 
            String path = "CS6326Asg2.txt";
            string search = fn.Text;
            ClearTbs(this.Controls);// clearing of all the fields take place
            listView1.Items.Remove(listView1.SelectedItems[0]);// removing the item from the list view 
            StreamReader sr = File.OpenText(path);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(search))
                {
                    line = "";

                    break;
                }

            }
            sr.Close();
            File.WriteAllText(path, line);// making the relevant change and writing the file

        }
        public void filesaving()
        {
            // saving of the file takes place in this functiom
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"CS6326Asg2.txt", true);
            
            file.Write(fn.Text + "\t");
            file.Write(mi.Text + "\t");
            file.Write(ln.Text + "\t");
            file.Write(phn.Text + "\t");
            file.Write(ad2.Text + "\t");
            file.Write(ad1.Text + "\t");
            file.Write(ct.Text + "\t");
            file.Write(st.Text + "\t");
            file.Write(zip.Text + "\t");
            file.Write(eid.Text + "\t");
            file.Write(proof.Text + "\t");
            file.Write(dtrecv.Text + "\t");
            file.Write(sttm.Text + "\t");
            file.Write(svtm.Text + "\r\n");
            statusbar.Text = "File Saved";
            showinlistview();// after the file is saved, the newly obtained data is shown in the list view as well


            file.Close();
        }



        public void searchingfromfile(string randomvar)
        {
            // searching the file for the selected value by the user and then getting updating them in the various fields
            char[] delimiterChars = { ' ', '\t' };
            String path = "CS6326Asg2.txt";
            StreamReader sr = File.OpenText(path);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(randomvar))
                {
                    //testbox.Text = line;
                    string[] temp = line.Split(delimiterChars);
                    fn.Text = temp[0];
                    mi.Text = temp[1];
                    ln.Text = temp[2];
                    phn.Text = temp[3];
                    ad1.Text = temp[4];
                    ad2.Text = temp[5];
                    ct.Text = temp[6];
                    st.Text = temp[7];
                    zip.Text = temp[8];
                    eid.Text = temp[9];
                    proof.Text = temp[10];
                    dtrecv.Text = temp[11];
                    sttm.Text = temp[12];
                    svtm.Text = temp[13];
                    break;
                }
            }

            sr.Close();
        }

        public void showinlistview()
        {

            ListViewItem.ListViewSubItem list = new ListViewItem.ListViewSubItem();

            string name = fn.Text + " " + mi.Text + " " + ln.Text;
            string phone = phn.Text;
            listView1.Items.Add(name).SubItems.Add(phone);

        }
        private void fn_TextChanged(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.UtcNow;      // this is used to store the time when the user starts to input text

            sttm.Text = localDate.ToString("HH:mm:ss");
            button2.Enabled = true;
            button3.Enabled = true;

        }

        private void eid_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$");// regular expression for validating email ids
            if (!pattern.IsMatch(eid.Text))
            {
                MessageBox.Show("Invalid Email Id");
                eid.Select();
            }

        }

        private void phn_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex(@"^-*[0-9,\.]+$");// regular expressions for validating phone numbers
            if (!pattern.IsMatch(phn.Text))
            {
                MessageBox.Show("Invalid Phone Number");
                phn.Select();
            }
        }

        private void zip_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^\\d{5}(?:[-\\s]\\d{4})?$");//  regular expressions for validating zip code
            if (!pattern.IsMatch(zip.Text))
            {
                MessageBox.Show("Invalid Zip Code");
                zip.Select();
            }

        }

        private void fn_Leave(object sender, EventArgs e)
        {
            

        }
    }
}
