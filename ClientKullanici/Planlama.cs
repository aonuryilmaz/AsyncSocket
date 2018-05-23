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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientKullanici
{
    public partial class Planlama : Form
    {
        private delegate void LogDelegete(ListView listView, List<ListViewItem> data);


        private LogDelegete _logDelegete;

        private void Log(ListView listView,List<ListViewItem> data)
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

        private ServerResponsePlanlamaClients responseModel { get; set; }
        public Planlama()
        {
            InitializeComponent();
            _logDelegete = Log;


            cmbİsTip.DataSource = Enum.GetValues(typeof(MakinaType));

            lstMakina.View = View.Details;
            lstMakina.GridLines = true;
            lstMakina.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstMakina.Columns.Add("Makina Adı");
            lstMakina.Columns.Add("Tür");
            lstMakina.Columns.Add("Durum");
            lstMakina.Columns.Add("İsler");

            lstIsEmir.View = View.Details;
            lstIsEmir.GridLines = true;
            lstIsEmir.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstIsEmir.Columns.Add("EmirId");
            lstIsEmir.Columns.Add("Tür");
            lstIsEmir.Columns.Add("Miktar");
        }

        private void Planlama_Load(object sender, EventArgs e)
        {
            _clientSocket.Connect(IPAddress.Loopback, 8002);
            _clientSocket.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), _clientSocket);
        }
        
        private void ReceiveData(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            int received = socket.EndReceive(ar);
            byte[] databuf = new byte[received];
            Array.Copy(receivedBuf, databuf, received);
            string data = Encoding.ASCII.GetString(databuf);
            var model = JsonConvert.DeserializeObject<ServerResponsePlanlamaClients>(data);
            responseModel = model;
            if (responseModel.IsEmriList != null)
            {
                var listItems = responseModel.IsEmriList.OrderBy(o=>o.Tip).Select(s => new ListViewItem(new string[] { s.Id.ToString(), s.Tip.ToString(), s.Miktar.ToString() })).ToList();
                Log(lstIsEmir, listItems);
            }

            if (responseModel.MakinaList != null)
            {
                var listItems = responseModel.MakinaList.OrderBy(o=>o.Tip).Select(s => new ListViewItem(new string[] { s.MakinaAdi, s.Tip.ToString(), s.Durum.ToString(),s.Isler?.Select(x=>x.Tip.ToString()).ToString()})).ToList();
                Log(lstMakina, listItems);
            }
            _clientSocket.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), _clientSocket);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            MakinaType tip;
            Enum.TryParse<MakinaType>(cmbİsTip.SelectedValue.ToString(), out tip);
            lblError.Text = "";
            if (nmrcMiktar.Value != 0)
            {
                IsEmri newEmir = new IsEmri();
                newEmir.Miktar = (int)nmrcMiktar.Value;
                newEmir.Tip = tip;
                string data = JsonConvert.SerializeObject(newEmir);
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                _clientSocket.Send(buffer);
            }
            else
            {
                lblError.Text = "Lütfen iş miktarı giriniz.";
            }
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            if (responseModel.IsEmriList != null)
            {
                lstIsEmir.Items.Clear();
                foreach (var item in responseModel.IsEmriList)
                {
                    lstIsEmir.Items.Add(new ListViewItem(new string[] { item.Id.ToString(), item.Tip.ToString(), item.Miktar.ToString() }));
                }
            }     
            if (responseModel.MakinaList != null)
            {
                lstMakina.Items.Clear();
                foreach (var item in responseModel.MakinaList)
                {
                    lstMakina.Items.Add(new ListViewItem(new string[] { item.MakinaAdi, item.Tip.ToString(), item.Durum.ToString() }));
                }
            }
        }
    }
}
