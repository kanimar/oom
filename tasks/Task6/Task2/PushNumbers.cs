using System;
using static System.Console;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Reactive.Linq;

namespace Task2
{
    public class Window : Form
    {
        public static void Run()
        {
            Application.Run(new Window());
        }

        public Window()
        {
            Text = "PushNumbers";
            Width = 800;
            Height = 600;

            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    var l = new Label();
                    int length = 75;
                    int k = (j - 1) * 9 + i % 10;
                    l.Name = Convert.ToString(k);
                    l.Text = Convert.ToString(k);
                    l.Location = new Point(length * i, length * j);
                    l.Size = new Size(length, length);
                    l.BorderStyle = BorderStyle.FixedSingle;
                    l.TextAlign = ContentAlignment.MiddleCenter;

                    var mouseDown = Observable.FromEventPattern<MouseEventArgs>(l, "MouseDown")
                        .Where(e => e.EventArgs.Button == MouseButtons.Left);
                    mouseDown
                       .Sample(TimeSpan.FromSeconds(1))
                       .Subscribe(args => { onMouseDown(args.Sender, args.EventArgs);
                           WriteLine(args.Sender);
                       })
                       ;

                    Controls.Add(l);
                }
            }    
        }

        private void onMouseDown(object sender, EventArgs e)
        {
            Control subc = sender as Control;
            subc.Font = new Font(subc.Font, FontStyle.Bold);
        }
    }
}

