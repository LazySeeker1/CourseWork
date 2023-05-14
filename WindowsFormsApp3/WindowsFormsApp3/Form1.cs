using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {

        int quection_count;
        int correct_answers;
        int wrong_answers;

        string[] array;

        int correct_answers_number;
        int selected_response;

        System.IO.StreamReader Read;

        public Form1()
        {
            InitializeComponent();
        }

        void start()
        {
            var Encoding = System.Text.Encoding.GetEncoding(65001);
            try
            {
                Read = new System.IO.StreamReader(
                System.IO.Directory.GetCurrentDirectory() +
                                               @"\t.txt", Encoding);
                this.Text = Read.ReadLine(); 
               
                quection_count = 0;
                correct_answers = 0;
                wrong_answers = 0;
            
                array = new String[10];
            }
            catch (Exception)
            {  
                MessageBox.Show("Error_1");
            }
            question();
        }

        void question()
        {
            label1.Text = Read.ReadLine();
          
            radioButton1.Text = Read.ReadLine();
            radioButton2.Text = Read.ReadLine();
            radioButton3.Text = Read.ReadLine();
          
            correct_answers_number = int.Parse(Read.ReadLine());
           
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
       
            button1.Enabled = false;
           quection_count = quection_count + 1;
          
            if (Read.EndOfStream == true) button1.Text = "Complete";

        }

        void StateSwitching(object sender, EventArgs e)
        {
          
            button1.Enabled = true; button1.Focus();
            RadioButton s = (RadioButton)sender;
            var tmp = s.Name;
           
            selected_response = int.Parse(tmp.Substring(11));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
     
            if (selected_response == correct_answers_number) correct_answers =
                                               correct_answers + 1;
            if (selected_response != correct_answers_number)
            {
                wrong_answers = wrong_answers + 1;
                
                array[wrong_answers] = label1.Text;
            }
            if (button1.Text == "Start test")
            {
                button1.Text = "Next question";
              
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
              
                start(); return;
            }
            if (button1.Text == "Complete")
            {
                Read.Close();
                
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
               
                label1.Text = String.Format("Test done.\n" +
                    "Correct answers: {0} из {1}.\n" +
                    "Points: {2:F2}.", correct_answers,
                    quection_count, (correct_answers * 5.0F) / quection_count);
              
                button1.Text = "Start test";
              
                var Str = "Worng answers " +
                          ":\n\n";
                for (int i = 1; i <= wrong_answers; i++)
                    Str = Str + array[i] + "\n";

                if (wrong_answers != 0) MessageBox.Show(
                                          Str, "Test done");
            } 
            if (button1.Text == "Next question") question();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Next question";
            button2.Text = "Complete";
          
            radioButton1.CheckedChanged += new EventHandler(StateSwitching);
            radioButton2.CheckedChanged += new EventHandler(StateSwitching);
            radioButton3.CheckedChanged += new EventHandler(StateSwitching);
            start();
        }
    }
}
    