using SoxSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Test
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }


    void sox_OnRawErrorOutput(object sender, string e)
    {
      if (e != null)
        Invoke((MethodInvoker)delegate { textBox1.AppendText(e + "\r\n"); });
    }


    void sox_OnRawStandardOutput(object sender, string e)
    {
      if (e != null)
        Invoke((MethodInvoker)delegate { textBox1.AppendText(e + "\r\n"); });
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Task.Factory.StartNew(() =>
      {
        using (Sox sox = new Sox())
        {
          sox.OnProgress += sox_OnProgress;

          sox.Buffer = 1024;
          sox.Output.Type = FileType.MP3;
          sox.Output.Comment = "Test";

          string fileInfo = sox.GetInfo("test.wav").ToString();
          Invoke((MethodInvoker)delegate { textBox1.AppendText(fileInfo + "\r\n"); });

          sox.Process("test.wav", "test.mp3");
        }
      });
    }

    void sox_OnProgress(object sender, ProgressEventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        Text = String.Format("{0} ({1}% completed)",e.Processed.ToString(@"hh\:mm\:ss\.ff"), e.Progress);

        progressBar1.Value = e.Progress;
        progressBar1.Invalidate();
      });      
    }
  }
}
