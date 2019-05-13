using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class start : Form
    {
        public static int totalMemorySize;
        public static string method;
        public static int noOfHoles;
        public start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //Input error checking
            if (string.IsNullOrWhiteSpace(total_memory.Text) || string.IsNullOrWhiteSpace(method_box.Text) 
                || string.IsNullOrWhiteSpace(no_of_holes.Text))
            {
                System.Windows.Forms.MessageBox.Show("You can't leave any field empty, please fill all fields.");
                return;
            }

            totalMemorySize = int.Parse(total_memory.Text);
            method = method_box.Text;
            noOfHoles = int.Parse(no_of_holes.Text);

            if (totalMemorySize < 0 || noOfHoles < 0)
            {
                System.Windows.Forms.MessageBox.Show("Total memory size & number of holes must be greater than 0");
                return;
            }
            if (totalMemorySize == 0)
            {
                System.Windows.Forms.MessageBox.Show("This is a memory management simulator, are you sure the memory size is 0?");
                return;
            }
            //////////////////////
            Holes_Info  next_form = new Holes_Info(noOfHoles, method, totalMemorySize);
            next_form.Show();
            this.Hide();

        }

        private void start_Load(object sender, EventArgs e)
        {

        }

        private void features_Click(object sender, EventArgs e)
        {
            Features next_form = new Features();
            next_form.Show();
            this.Hide();
        }
    }
}
