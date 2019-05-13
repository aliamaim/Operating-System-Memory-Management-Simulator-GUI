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
    public partial class Holes_Info : Form
    {
        public string method;
        public int total_size;
        public int no_of_holes;
        public int[] holes_start;
        public int[] holes_size;
        public Holes_Info(int noOfHoles, string method_s, int totalSize)
        {
            int pointX = 20;
            int pointY = 40;
            total_size = totalSize;
            method = method_s;
            no_of_holes = noOfHoles;
            holes_start = new int[noOfHoles];
            holes_size = new int[noOfHoles];
            for (int i = 0; i < no_of_holes; i++)
            {
                Label x = new Label();
                x.Text = "Hole " + (i + 1);
                x.Location = new Point(pointX, pointY);
                this.Controls.Add(x);
                TextBox a1 = new TextBox();
                a1.Name = "textBoxstart" + i;
                a1.Location = new Point(pointX + 100, pointY);
                TextBox a2 = new TextBox();
                a2.Name = "textBoxsize" + i;
                a2.Location = new Point(pointX + 220, pointY);
                this.Controls.Add(a1);
                this.Controls.Add(a2);
                pointY += 30;
            }
            InitializeComponent();
        }

        private void Holes_Info_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string search_start;
            string search_size;
            int total_hole_size = 0;
            for (int i = 0; i < no_of_holes; i++)
            {
                search_start = "textBoxstart" + i;
                search_size = "textBoxsize" + i;
                TextBox start_value = (TextBox)this.Controls.Find(search_start, false).FirstOrDefault();
                TextBox size_value = (TextBox)this.Controls.Find(search_size, false).FirstOrDefault();
                //Input error checking
                if (string.IsNullOrWhiteSpace(start_value.Text) || string.IsNullOrWhiteSpace(size_value.Text))
                {
                    System.Windows.Forms.MessageBox.Show("You can't leave any field empty, please fill all fields.");
                    return;
                }
                //////////////////////
                holes_start[i] = int.Parse(start_value.Text);
                holes_size[i] = int.Parse(size_value.Text);
                //Input error checking
                total_hole_size += holes_size[i];
                if (holes_start[i] < 0 || holes_size[i] < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Holes cannot start from a negative value & size should be positive");
                    return;
                }
                if (holes_size[i] == 0)
                {
                    System.Windows.Forms.MessageBox.Show("Why would you enter a hole that's non-existant (size = 0), are you trying to break the program?!");
                    return;
                }
                if (holes_size[i] > total_size)
                {
                    System.Windows.Forms.MessageBox.Show("Hole size cannot be bigger than the total memory size.");
                    return;
                }
                /////////////////
            }
            //Input error check
            for (int i = 0; i < no_of_holes - 1; i++)
            {
                for (int j = 0; j < no_of_holes - i - 1; j++)
                {
                    if (holes_start[j] > holes_start[j + 1])
                    {
                        int temp = holes_start[j];
                        holes_start[j] = holes_start[j + 1];
                        holes_start[j + 1] = temp;

                        temp = holes_size[j];
                        holes_size[j] = holes_size[j + 1];
                        holes_size[j + 1] = temp;
                    }
                }
            }
            for (int i = 0; i < no_of_holes - 1; i++)
            {
                if (i > 1)
                {
                    if (holes_start[i + 1] < (holes_start[i] + holes_size[i]))
                    {
                        System.Windows.Forms.MessageBox.Show("Holes sizes & start points are not matching, some holes are intersecting with other holes.");
                        return;
                    }
                }
            }
            if (total_hole_size > total_size)
            {
                System.Windows.Forms.MessageBox.Show("The sum of the holes cannot be greater than the total memory size.");
                return;
            }
            ////////////////////
            Draw draw_form = new Draw(method, total_size, no_of_holes, holes_start, holes_size);
            draw_form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            start rewind_1 = new start();
            rewind_1.Show();
            this.Hide();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            start start_rewind = new start();
            start_rewind.Show();
            this.Hide();
        }
    }
}
