using System;
using System.IO;
using System.Text;

namespace OMSTaskScheduler.Util
{
    public class ConsoleWriter : TextWriter
    {
        private System.Windows.Forms.RichTextBox rTextBox;
        public ConsoleWriter(System.Windows.Forms.RichTextBox textBox)
        {
            this.rTextBox = textBox;
        }

        public override void WriteLine(string str)
        {
            this.rTextBox.Invoke((Action)delegate { this.rTextBox.AppendText(str + "\n"); });
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
