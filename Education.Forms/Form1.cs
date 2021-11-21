using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Education.Forms
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) {
            var button = (Button)sender;
            await Task.Factory.StartNew(() => {
                button.Text = "changed";
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            await Task.Delay(500);
            button.Text = "ch";
        }
    }
}
