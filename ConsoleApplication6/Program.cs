using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Timers;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ConsoleApplication1
{
    class Program
    {
        public static object Screen { get; private set; }

       static  public void Show()
        {
            Console.WriteLine(@"1.Start
2.Set time out
3.Send to email
4.Hide 
5.Exit");
        }//хайд - скрыть окно консоли
        static public void Menu(ConsoleKey key)
        {
            
            switch (key)
            {       
                case ConsoleKey.D1:
                    //Stop();
                    break;
                case ConsoleKey.D2:
                    SetTimeOff();
                    break;
                case ConsoleKey.D3:
                    //SendMail()
                    break;
                case ConsoleKey.D4:
                    //Hide();
                    break;
                case ConsoleKey.D5:
                    break;
                default:
                    break;
            }
        }
        static public void SetTimeOff()
        {
            int num = 0;
            TimerCallback tm = new TimerCallback(Count);
            System.Threading.Timer timer = new System.Threading.Timer(tm, num, 0, 60000);
            Console.WriteLine();
            Console.ReadLine();
        }
        public static void Count(object obj)
        {
            Console.WriteLine("Count");
        }       
        public static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null)
        {
            
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(mailto));
            mail.Subject = caption;
            mail.Body = message;
            if (!string.IsNullOrEmpty(attachFile))
                mail.Attachments.Add(new Attachment(attachFile));
            SmtpClient client = new SmtpClient();
            client.Host = smtpServer;
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(from.Split('@')[0], password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            mail.Dispose();
            
        }

        [DllImport("user32.dll")]
        public extern static IntPtr GetDesktopWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern UInt64 BitBlt
             (IntPtr hDestDC,
             int x, int y, int nWidth, int nHeight,
             IntPtr hSrcDC,
             int xSrc, int ySrc,
             System.Int32 dwRop);

        [STAThread]

        static void DoScreen()
        {
            Image myImage = new System.Drawing.Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            Graphics gr1 = Graphics.FromImage(myImage);

            IntPtr dc1 = gr1.GetHdc();
            IntPtr dc2 = GetWindowDC(GetDesktopWindow());

            BitBlt(dc1, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, dc2, 0, 0, 13369376);

            gr1.ReleaseHdc(dc1);

            myImage.Save("screenshot.jpg", ImageFormat.Jpeg);
        }

        static void Main(string[] args)
        {
            Show();
            ConsoleKey key;
            key = Console.ReadKey().Key;
            Menu(key);



        }
    }
}
