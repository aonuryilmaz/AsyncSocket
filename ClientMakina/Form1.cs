using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientMakina
{
    public partial class Form1 : Form
    {
        private delegate void LogDelegete(ListView listView, List<ListViewItem> data);


        private LogDelegete _logDelegete;

        private void Log(ListView listView, List<ListViewItem> data)
        {
            if (listView.InvokeRequired)
            {
                LogDelegete logDelegete = Log;
                Invoke(logDelegete, listView, data);
            }
            else
            {
                listView.BeginUpdate();
                listView.Items.Clear();
                listView.Items.AddRange(data.ToArray());
                listView.EndUpdate();
            }
        }
        private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        byte[] receivedBuf = new byte[1024];
        private Makina receivedMakina { get; set; }
        public Form1()
        {
            InitializeComponent();
            _logDelegete = Log;
            cmbTip.DataSource = Enum.GetValues(typeof(MakinaType));

            lstGecmis.View = View.Details;
            lstGecmis.GridLines = true;
            lstGecmis.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstGecmis.Columns.Add("EmirId");
            lstGecmis.Columns.Add("Tür");
            lstGecmis.Columns.Add("Miktar");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _clientSocket.Connect(IPAddress.Loopback, 8000);
            _clientSocket.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), _clientSocket);
        }
        private void ReceiveData(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            int received = socket.EndReceive(ar);
            byte[] databuf = new byte[received];
            Array.Copy(receivedBuf, databuf, received);
            string data = Encoding.ASCII.GetString(databuf);
            receivedMakina = JsonConvert.DeserializeObject<Makina>(data);
            if (receivedMakina.Isler != null)
            {
                var listItems = receivedMakina.Isler.OrderBy(o => o.Tip).Select(s => new ListViewItem(new string[] { s.Id.ToString(), s.Tip.ToString(), s.Miktar.ToString() })).ToList();
                Log(lstGecmis, listItems);
            }         
            _clientSocket.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), _clientSocket);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MakinaType tip;
            Enum.TryParse<MakinaType>(cmbTip.SelectedValue.ToString(), out tip);
            string MakinaAdı = txtMakinaAdı.Text.ToString();
            int Hız = Convert.ToInt32(nmrcHiz.Value);
            Makina newMakina = new Makina();
            newMakina.Durum = MakinaStatus.EMPTY;
            newMakina.MakinaAdi = MakinaAdı;
            newMakina.Tip = tip;
            newMakina.UretimHizi =Hız;
            string data = JsonConvert.SerializeObject(newMakina);
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            lblStatus.Text = newMakina.Durum.ToString();
            button1.Visible = false;
            _clientSocket.Send(buffer);
        }
    }
}
