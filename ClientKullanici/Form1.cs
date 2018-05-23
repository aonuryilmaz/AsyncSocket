using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientKullanici
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket = new TcpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           clientSocket.Connect("127.0.0.1", 8001);
        }
        public void msg(string message)
        {
            if (message == "Ok")
            {
                lblError.Text = "Giriş Başarılı";
                Planlama frm = new Planlama();
                this.Hide();
                frm.Show();
            }
            else
            {
                lblError.Text = "Giriş Başarısız";
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtKullanici.Text) && !String.IsNullOrEmpty(txtPassword.Text))
            {
                UserIdentityRequestModel model = new UserIdentityRequestModel();
                model.Name = txtKullanici.Text;
                model.Password = txtPassword.Text;
                NetworkStream serverStream = clientSocket.GetStream();
                string data = JsonConvert.SerializeObject(model);
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[10025];
                serverStream.Read(inStream, 0, inStream.Length);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.TrimEnd('\0');
                msg(returndata);
            }
            else
            {
                lblError.Text = "Kullanıcı adı veya \nşifre boş bırakılamaz";
            }
           
        }
    }
}
