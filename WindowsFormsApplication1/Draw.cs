using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Web;

namespace WindowsFormsApplication1
{
    public partial class Draw : Form
    {
        public class Process
        {
            public string name;
            public int number_of_segments;
            public string[] name_of_segments;
            public int[] start_address_segment;
            public int[] size_of_segments;
            public int starting_address;
        }

        public class Block
        {
            public int start;
            public int size;
            public Label toBeDrawn_Main;
            public Label toBeDrawn_Starting_Address;
            public Label toBeDrawn_Ending_Address;
        }
        public int backup_no_of_holes;
        public int no_of_holes;
        public string method;
        public int total_memory_size;
        int current_process_id = 1;
        int old_process_number = 1;
        Button next_segment_no;
        Button next_segment_info;
        TextBox segment_no;
        TextBox segment_name;
        TextBox segment_size;
        Label segment_no_label;
        Label segment_name_label;
        Label segment_size_label;
        Label segment_info_label;
        Process current_process;
        List<Process> old_processes = new List<Process>();
        List<Process> user_processes = new List<Process>();
        Block current_block;
        List<Block> drawn_blocks = new List<Block>();
        List<int> holesStart;
        List<int> holesSize;
        ComboBox running_processes = new ComboBox();
        public Draw(string method_s, int totalSize, int noOfHoles, int[] holes_start, int[] holes_size)
        {
            no_of_holes = noOfHoles;
            backup_no_of_holes = noOfHoles;
            method = method_s;
            total_memory_size = totalSize;
            current_process_id = 1;
            old_process_number = 1;
            
            holesStart = new List<int>();
            holesSize = new List<int>();

            holesStart.AddRange(holes_start);
            holesSize.AddRange(holes_size);

            running_processes.Location = new Point(640, 80);
            this.Controls.Add(running_processes);
            InitializeComponent();
        }

        private void Draw_Load(object sender, EventArgs e)
        {
            tabPage1.AutoScroll = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Holes_Info rewind_2 = new Holes_Info( backup_no_of_holes,  method,  total_memory_size);
            rewind_2.Show();
            this.Hide();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            start start_rewind = new start();
            start_rewind.Show();
            this.Hide();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            int pointX = 385;
            int pointY = 50;
            Add.Hide();
            segment_no_label = new Label();
            segment_no_label.Text = "Segment No.";
            segment_no_label.Location = new Point(pointX - 70, pointY);
            segment_no_label.AutoSize = true;
            segment_no = new TextBox();
            segment_no.Location = new Point(pointX, pointY);
            next_segment_no = new Button();
            next_segment_no.Text = "Next";
            next_segment_no.Location = new Point(pointX, pointY + 30);
            this.Controls.Add(segment_no_label);
            this.Controls.Add(segment_no);
            this.Controls.Add(next_segment_no);
            next_segment_no.Click += new EventHandler(this.next_segment_no_Click);
        }
        void next_segment_no_Click(object sender, System.EventArgs e)
        {
            //Input error checking
            if (string.IsNullOrWhiteSpace(segment_no.Text))
            {
                System.Windows.Forms.MessageBox.Show("You can't leave any fields empty, please fill all fields.");
                return;
            }
            if (int.Parse(segment_no.Text) <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Segment number must be greater than 0.");
                return;
            }
            ///////////////////////
            current_process = new Process();
            current_process.number_of_segments = int.Parse(segment_no.Text);
            current_process.name_of_segments = new string[current_process.number_of_segments];
            current_process.size_of_segments = new int[current_process.number_of_segments];
            current_process.start_address_segment = new int[current_process.number_of_segments];
            current_process.name = "P" + current_process_id;
            current_process_id++;
            segment_no_label.Hide();
            segment_no.Hide();
            next_segment_no.Hide();
            int pointX = 385;
            int pointY = 50;
            segment_info_label = new Label();
            segment_info_label.Text = "Segments Info";
            segment_info_label.Location = new Point(pointX, pointY);
            segment_info_label.AutoSize = true;
            segment_info_label.Font = new Font(segment_info_label.Font, FontStyle.Bold);
            pointY += 15;
            segment_name_label = new Label();
            segment_name_label.Text = "Name";
            segment_name_label.Location = new Point(pointX - 40, pointY);
            segment_name_label.AutoSize = true;
            segment_size_label = new Label();
            segment_size_label.Text = "Size";
            segment_size_label.Location = new Point(pointX + 80, pointY);
            segment_size_label.AutoSize = true;
            this.Controls.Add(segment_info_label);
            this.Controls.Add(segment_name_label);
            this.Controls.Add(segment_size_label);
            for (int j = 0; j < current_process.number_of_segments; j++)
            {
                pointY += 30;
                segment_name = new TextBox();
                segment_name.Name = "textBoxname" + current_process_id;
                segment_name.Name += j;
                segment_name.Location = new Point(pointX - 60, pointY);
                segment_size = new TextBox();
                segment_size.Name = "textBoxsize" + current_process_id;
                segment_size.Name += j;
                segment_size.Location = new Point(pointX + 60, pointY);
                this.Controls.Add(segment_name);
                this.Controls.Add(segment_size);
            }
            pointY += 30;
            next_segment_info = new Button();
            next_segment_info.Location = new Point(pointX, pointY);
            next_segment_info.Text = "Next";
            this.Controls.Add(next_segment_info);
            next_segment_info.Click += new EventHandler(this.next_segment_info_Click);
        }

        void next_segment_info_Click(object sender, System.EventArgs e)
        {
            segment_info_label.Hide();
            segment_name_label.Hide();
            segment_size_label.Hide();
            string search_name, search_size, search_name_label, search_size_label;
            for (int j = 0; j < current_process.number_of_segments; j++)
            {
                search_name = "textBoxName" + current_process_id;
                search_name += j;
                search_size = "textBoxSize" + current_process_id;
                search_size += j;
                search_name_label = "segment_name_label" + j;
                search_size_label = "segment_size_label" + j;
                TextBox name_value = (TextBox)this.Controls.Find(search_name, false).FirstOrDefault();
                TextBox size_value = (TextBox)this.Controls.Find(search_size, false).FirstOrDefault();
                //Input error checking
                if (string.IsNullOrWhiteSpace(name_value.Text) || string.IsNullOrWhiteSpace(size_value.Text))
                {
                    System.Windows.Forms.MessageBox.Show("You can't leave any field empty, please fill all fields.");
                    name_value.Hide();
                    size_value.Hide();
                    name_value = (TextBox)this.Controls.Find(search_name.Substring(0, search_name.Length - 1), false).FirstOrDefault();
                    size_value = (TextBox)this.Controls.Find(search_size.Substring(0, search_size.Length - 1), false).FirstOrDefault();
                    while (name_value != null && size_value != null)
                    {
                        name_value = (TextBox)this.Controls.Find(search_name.Substring(0, search_name.Length - 1), false).FirstOrDefault();
                        size_value = (TextBox)this.Controls.Find(search_size.Substring(0, search_size.Length - 1), false).FirstOrDefault();
                        name_value.Hide();
                        size_value.Hide();
                    }
                    next_segment_info.Hide();
                    return;
                }
                if (int.Parse(size_value.Text) <= 0)
                {
                    System.Windows.Forms.MessageBox.Show("Segments size must be greater than 0.");
                    name_value.Hide();
                    size_value.Hide();
                    name_value = (TextBox)this.Controls.Find(search_name.Substring(0, search_name.Length - 1), false).FirstOrDefault();
                    size_value = (TextBox)this.Controls.Find(search_size.Substring(0, search_size.Length - 1), false).FirstOrDefault();
                    while (name_value != null && size_value != null)
                    {
                        name_value = (TextBox)this.Controls.Find(search_name.Substring(0, search_name.Length - 1), false).FirstOrDefault();
                        size_value = (TextBox)this.Controls.Find(search_size.Substring(0, search_size.Length - 1), false).FirstOrDefault();
                        name_value.Hide();
                        size_value.Hide();
                    }
                    next_segment_info.Hide();
                    return;
                }
                ///////////////////////
                current_process.name_of_segments[j] = name_value.Text;
                current_process.size_of_segments[j] = int.Parse(size_value.Text);
                name_value.Hide();
                size_value.Hide();
            }

            next_segment_info.Hide();
            Add.Show();
            if (method == "First Fit")
            {
                for (int i = 0; i < current_process.number_of_segments; i++)
                {
                    for (int j = 0; j < no_of_holes; j++)
                    {
                        if (current_process.size_of_segments[i] < holesSize[j])
                        {
                            drawrectangle(holesStart[j], (current_process.name + ":" + current_process.name_of_segments[i]), current_process.size_of_segments[i]);
                            current_process.start_address_segment[i] = holesStart[j];
                            holesStart[j] = holesStart[j] + current_process.size_of_segments[i];
                            holesSize[j] = holesSize[j] - current_process.size_of_segments[i];
                            merge_holes();
                            i++;
                            break;
                        }
                        else if (current_process.size_of_segments[i] == holesSize[j])
                        {
                            drawrectangle(holesStart[j], (current_process.name + ":" + current_process.name_of_segments[i]), current_process.size_of_segments[i]);
                            current_process.start_address_segment[i] = holesStart[j];
                            holesStart.Remove(holesStart[j]);
                            holesSize.Remove(holesSize[j]);
                            no_of_holes--;
                            i++;
                            break;
                        }
                        else if (no_of_holes <= 0 || (current_process.size_of_segments[i] > holesSize[j] && j == (no_of_holes - 1)))
                        {
                            for(int k = 0; k < i; k++)
                            {
                                holesStart.Add(current_process.start_address_segment[k]);
                                holesSize.Add(current_process.size_of_segments[k]);
                                no_of_holes++;
                            }
                            sort_start(ref holesStart, ref holesSize);
                            merge_holes();
                            System.Windows.Forms.MessageBox.Show("No hole fits the " + current_process.name_of_segments[i] + " of process " + current_process.name);
                            return;
                        }
                    }

                    if(no_of_holes <= 0 && i < current_process.number_of_segments)
                    {
                        for (int k = 0; k < i; k++)
                        {
                            holesStart.Add(current_process.start_address_segment[k]);
                            holesSize.Add(current_process.size_of_segments[k]);
                            no_of_holes++;
                        }
                        sort_start(ref holesStart, ref holesSize);
                        merge_holes();
                        System.Windows.Forms.MessageBox.Show("No hole fits the " + current_process.name_of_segments[i] + " of process " + current_process.name);
                        return;
                    }
                    i--;

                }
                running_processes.Items.Add(current_process.name);
                user_processes.Add(current_process);
            }
            else if (method == "Best Fit")
            {
                int fittest_hole = 0;
                int found_hole = 0;
                for (int i = 0; i < current_process.number_of_segments; i++)
                {
                    found_hole = 0;
                    fittest_hole = 0;
                    sort_size(ref holesStart, ref holesSize);
                    for (int j = 0; j < no_of_holes; j++)
                    {
                        if (current_process.size_of_segments[i] < holesSize[j])
                        {
                            found_hole = 1;
                            if (holesSize[j] < holesSize[fittest_hole] || current_process.size_of_segments[i] > holesSize[fittest_hole])
                            {
                                fittest_hole = j;
                            }
                        }
                        else if (current_process.size_of_segments[i] == holesSize[j])
                        {
                            found_hole = 2;
                            fittest_hole = j;
                            break;
                        }
                    }
                   
                    if (found_hole == 1)
                    {
                        drawrectangle(holesStart[fittest_hole], (current_process.name + ":" + current_process.name_of_segments[i]), current_process.size_of_segments[i]);
                        current_process.start_address_segment[i] = holesStart[fittest_hole];
                        holesStart[fittest_hole] = holesStart[fittest_hole] + current_process.size_of_segments[i];
                        holesSize[fittest_hole] = holesSize[fittest_hole] - current_process.size_of_segments[i];
                        drawrectangle(holesStart[fittest_hole], "Hole", holesSize[fittest_hole]);
                        sort_start(ref holesStart, ref holesSize);
                    }
                    else if (found_hole == 2)
                    {
                        drawrectangle(holesStart[fittest_hole], (current_process.name + ":" + current_process.name_of_segments[i]), current_process.size_of_segments[i]);
                        current_process.start_address_segment[i] = holesStart[fittest_hole];
                        holesStart.Remove(holesStart[fittest_hole]);
                        holesSize.Remove(holesSize[fittest_hole]);
                        no_of_holes--;
                    }
                    else
                    {
                        for (int k = 0; k < i; k++)
                        {
                            holesStart.Add(current_process.start_address_segment[k]);
                            holesSize.Add(current_process.size_of_segments[k]);
                            no_of_holes++;
                        }
                        sort_start(ref holesStart, ref holesSize);
                        merge_holes();
                        System.Windows.Forms.MessageBox.Show("No hole fits the " + current_process.name_of_segments[i] + " of process " + current_process.name);
                        return;
                    }
                }
                running_processes.Items.Add(current_process.name);
                user_processes.Add(current_process);
            }
            else if (method == "Worst Fit")
            {
                int unfittest_hole;
                int found_hole;
                for (int i = 0; i < current_process.number_of_segments; i++)
                {
                    found_hole = 0;
                    unfittest_hole = 0;
                    sort_size_desc(ref holesStart, ref holesSize);
                    for (int j = 0; j < no_of_holes; j++)
                    {
                        if (current_process.size_of_segments[i] < holesSize[j])
                        { 
                            if (holesSize[j] > holesSize[unfittest_hole] || found_hole == 0) 
                            {
                                found_hole = 1;
                                unfittest_hole = j;
                            }
                        }
                        else if (current_process.size_of_segments[i] == holesSize[j] && found_hole != 1)
                        {
                            found_hole = 2;
                            unfittest_hole = j;
                            break;
                        }
                    }
                    if (found_hole == 1)
                    {
                        drawrectangle(holesStart[unfittest_hole], (current_process.name + ":" + current_process.name_of_segments[i]), current_process.size_of_segments[i]);
                        current_process.start_address_segment[i] = holesStart[unfittest_hole];
                        holesStart[unfittest_hole] = holesStart[unfittest_hole] + current_process.size_of_segments[i];
                        holesSize[unfittest_hole] = holesSize[unfittest_hole] - current_process.size_of_segments[i];
                        drawrectangle(holesStart[unfittest_hole], "Hole", holesSize[unfittest_hole]);
                    }
                    else if (found_hole == 2)
                    {
                        drawrectangle(holesStart[unfittest_hole], (current_process.name + ":" + current_process.name_of_segments[i]), current_process.size_of_segments[i]);
                        current_process.start_address_segment[i] = holesStart[unfittest_hole];
                        holesStart.Remove(holesStart[unfittest_hole]);
                        holesSize.Remove(holesSize[unfittest_hole]);
                        no_of_holes--;
                    }
                    else
                    {
                        for (int k = 0; k < i; k++)
                        {
                            holesStart.Add(current_process.start_address_segment[k]);
                            holesSize.Add(current_process.size_of_segments[k]);
                            no_of_holes++;
                        }
                        sort_start(ref holesStart, ref holesSize);
                        merge_holes();
                        System.Windows.Forms.MessageBox.Show("No hole fits the " + current_process.name_of_segments[i] + " of process " + current_process.name);
                        return;
                    }
                }
                running_processes.Items.Add(current_process.name);
                user_processes.Add(current_process);
            }
        }

        private void drawrectangle(int start, string name, int size)
        {
            current_block = new Block();
            current_block.start = start;
            current_block.size = size;
            //Initializing the main label and setting it's properties
            current_block.toBeDrawn_Main = new Label();
            current_block.toBeDrawn_Main.Text = name;
            current_block.toBeDrawn_Main.Size = new Size(110, size);
            current_block.toBeDrawn_Main.Location = new Point(100, start + 50);
            current_block.toBeDrawn_Main.ForeColor = Color.Black;
            current_block.toBeDrawn_Main.BackColor = Color.LightBlue;
            current_block.toBeDrawn_Main.Font = new Font("Arial", 12);
            current_block.toBeDrawn_Main.BorderStyle = BorderStyle.FixedSingle;
            
            //Initializing the starting address label and setting it's properties
            current_block.toBeDrawn_Starting_Address = new Label();
            current_block.toBeDrawn_Starting_Address.Text = start.ToString();
            current_block.toBeDrawn_Starting_Address.Size = new Size(50, 15);
            current_block.toBeDrawn_Starting_Address.Location = new Point(45, start + 50);
            current_block.toBeDrawn_Starting_Address.ForeColor = Color.Black;
            current_block.toBeDrawn_Starting_Address.BackColor = Color.White;
            current_block.toBeDrawn_Starting_Address.BorderStyle = BorderStyle.None;

            //Initializing the ending address label and setting it's properties
            int end = start + size;
            current_block.toBeDrawn_Ending_Address = new Label();
            current_block.toBeDrawn_Ending_Address.Text = end.ToString();
            current_block.toBeDrawn_Ending_Address.Size = new Size(50, 15);
            current_block.toBeDrawn_Ending_Address.Location = new Point(45, start + size + 50);
            current_block.toBeDrawn_Ending_Address.ForeColor = Color.Black;
            current_block.toBeDrawn_Ending_Address.BackColor = Color.White;
            current_block.toBeDrawn_Ending_Address.BorderStyle = BorderStyle.None;

            for (int i = 0; i < drawn_blocks.Count(); i++)
            {
                if (drawn_blocks[i].start >= start && drawn_blocks[i].start < (start + size))
                {
                    tabPage1.Controls.Remove(drawn_blocks[i].toBeDrawn_Main);
                    tabPage1.Controls.Remove(drawn_blocks[i].toBeDrawn_Starting_Address);
                    tabPage1.Controls.Remove(drawn_blocks[i].toBeDrawn_Ending_Address);
                    drawn_blocks.Remove(drawn_blocks[i]);
                    i--;
                }
            }

            drawn_blocks.Add(current_block);
            tabPage1.Controls.Add(current_block.toBeDrawn_Main);
            tabPage1.Controls.Add(current_block.toBeDrawn_Starting_Address);
            tabPage1.Controls.Add(current_block.toBeDrawn_Ending_Address);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Init_Click(object sender, EventArgs e)
        {
            merge_holes();
            Init.Hide();
            for (int i = 0; i < no_of_holes; i++)
            {
                if(i == 0 && holesStart[0] > 0)
                {
                    Process current_old_process = new Process();
                    current_old_process.starting_address = 0;
                    current_old_process.number_of_segments = holesStart[0];
                    current_old_process.name = "OldProcess" + old_process_number;
                    drawrectangle(current_old_process.starting_address, current_old_process.name, current_old_process.number_of_segments);
                    running_processes.Items.Add(current_old_process.name);
                    old_processes.Add(current_old_process);
                    old_process_number++;
                }
                if (i != no_of_holes - 1 && ((holesStart[i] + holesSize[i]) < holesStart[i + 1]))
                {
                    Process current_old_process = new Process();
                    current_old_process.starting_address = holesStart[i] + holesSize[i];
                    current_old_process.number_of_segments = holesStart[i + 1] - (holesStart[i] + holesSize[i]);
                    current_old_process.name = "OldProcess" + old_process_number;
                    drawrectangle(current_old_process.starting_address, current_old_process.name, current_old_process.number_of_segments);
                    running_processes.Items.Add(current_old_process.name);
                    old_processes.Add(current_old_process);
                    old_process_number++;
                }
                else if (i == no_of_holes - 1)
                {
                    if ((holesStart[i] + holesSize[i]) < total_memory_size)
                    {
                        Process current_old_process = new Process();
                        current_old_process.starting_address = holesStart[i] + holesSize[i];
                        current_old_process.number_of_segments = total_memory_size - (holesStart[i] + holesSize[i]);
                        current_old_process.name = "OldProcess" + old_process_number;
                        drawrectangle(current_old_process.starting_address, current_old_process.name, current_old_process.number_of_segments);
                        running_processes.Items.Add(current_old_process.name);
                        old_processes.Add(current_old_process);
                        old_process_number++;
                    }
                }
                
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dealloc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(running_processes.Text))
            {
                System.Windows.Forms.MessageBox.Show("Please choose a process to deallocate.");
                return;
            }

            if (running_processes.Text[0] == 'O')
            {
                for (int i = 0; i < old_process_number; i++)
                {
                    if (old_processes[i].name == running_processes.Text)
                    {
                        running_processes.Items.Remove(running_processes.Text);
                        holesStart.Add(old_processes[i].starting_address);
                        holesSize.Add(old_processes[i].number_of_segments);
                        no_of_holes++;
                        old_processes.Remove(old_processes[i]);
                        sort_start(ref holesStart, ref holesSize);
                        merge_holes();
                        old_process_number--;
                        return;
                    }
                }
            }
            else
            {
                for(int i = 0; i < user_processes.Count; i++)
                {
                    if(user_processes[i].name == running_processes.Text)
                    {
                        running_processes.Items.Remove(running_processes.Text);
                        for(int j = 0; j < user_processes[i].number_of_segments; j++)
                        {
                            holesStart.Add(user_processes[i].start_address_segment[j]);
                            holesSize.Add(user_processes[i].size_of_segments[j]);
                            no_of_holes++;
                        }
                        user_processes.Remove(user_processes[i]);      
                    }
                }
                sort_start(ref holesStart, ref holesSize);
                merge_holes();
            }
        }

        private void sort_size(ref List<int> start, ref List<int> size)
        {
            for (int i = 0; i < no_of_holes - 1; i++)
            {
                for (int j = 0; j < no_of_holes - i - 1; j++)
                {
                    if (size[j] > size[j + 1])
                    {
                        int temp = start[j];
                        start[j] = start[j + 1];
                        start[j + 1] = temp;

                        temp = size[j];
                        size[j] = size[j + 1];
                        size[j + 1] = temp;
                    }
                }
            }
        }

        private void sort_size_desc(ref List<int> start, ref List<int> size)
        {
            for (int i = 0; i < no_of_holes - 1; i++)
            {
                for (int j = 0; j < no_of_holes - i - 1; j++)
                {
                    if (size[j] < size[j + 1])
                    {
                        int temp = start[j];
                        start[j] = start[j + 1];
                        start[j + 1] = temp;

                        temp = size[j];
                        size[j] = size[j + 1];
                        size[j + 1] = temp;
                    }
                }
            }
        }

        private void sort_start(ref List<int> start, ref List<int> size)
        {
            for (int i = 0; i < no_of_holes - 1; i++)
            {
                for (int j = 0; j < no_of_holes - i - 1; j++)
                {
                    if (start[j] > start[j + 1])
                    {
                        int temp = start[j];
                        start[j] = start[j + 1];
                        start[j + 1] = temp;

                        temp = size[j];
                        size[j] = size[j + 1];
                        size[j + 1] = temp;
                    }
                }
            }
        }

        void merge_holes()
        {
            for (int i = 0; i < no_of_holes - 1; i++)
            {
                if ((holesStart[i] + holesSize[i]) == holesStart[i + 1])
                {
                    holesSize[i] += holesSize[i + 1];
                    no_of_holes--;
                    holesSize[i + 1] = -1;
                    holesStart[i + 1] = -1;
                    holesSize.Remove(holesSize[i + 1]);
                    holesStart.Remove(holesStart[i + 1]);
                    i--;
                }
            }

            for(int i = 0; i < no_of_holes; i++)
            {
                drawrectangle(holesStart[i], "Hole", holesSize[i]);
            }
        }
    }
}
