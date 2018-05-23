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

namespace ServerSocket
{

    public partial class Form1 : Form
    {
        //Buffer
        private byte[] _buffer = new byte[3000];
        private byte[] _bufferPlan = new byte[1024];
        private byte[] _bufferIdentity = new byte[1024];



        private int IsEmriId { get; set; }
        private int MakinaId { get; set; }
        //Model List
        private List<UserIdentityModel> userList = new List<UserIdentityModel>() {
            new UserIdentityModel
            {
                Name="Onur",
                Password="123456"
            },
            new UserIdentityModel
            {
                Name="Sercan",
                Password="qwe123"
            }
        };
        /// <summary>
        /// Matching
        /// </summary>
        private List<Makina> _MakinaList { get; set; }
        private List<IsEmri> _IsEmriList { get; set; }


        //SocketList
        public List<MakinaList> _MakinaSockets { get; set; }
        public List<Socket> _ClientSockets { get; set; }
        public List<Socket> _PlanClients { get; set; }

        //SERVER SOCKET-----------------------------------------
        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Identity SOCKET-----------------------------------------
        private Socket _userIdentitySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //PLANLAMA SOCKET-----------------------------------------
        private Socket _planSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Form1()
        {
            IsEmriId = 1;
            MakinaId = 1;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _MakinaSockets = new List<MakinaList>();
            _MakinaList = new List<Makina>();
            _IsEmriList = new List<IsEmri>();
            _PlanClients = new List<Socket>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupServer();
        }
        private void SetupServer()
        {
            lblStatus.Text = "Server ayarlanıyor...";
            _userIdentitySocket.Bind(new IPEndPoint(IPAddress.Any, 8001));
            _userIdentitySocket.Listen(1);
            _userIdentitySocket.BeginAccept(new AsyncCallback(UserIdentityAcceptCallback), null);
            _planSocket.Bind(new IPEndPoint(IPAddress.Any, 8002));
            _planSocket.Listen(1);
            _planSocket.BeginAccept(new AsyncCallback(PlanAcceptCallback), null);

            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8000));
            _serverSocket.Listen(1);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }


        private void UserIdentityAcceptCallback(IAsyncResult ar)
        {
            Socket socket = _userIdentitySocket.EndAccept(ar);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(UserIdentityReceiveCallback), socket);
            _userIdentitySocket.BeginAccept(new AsyncCallback(UserIdentityAcceptCallback), null);
        }
        private void UserIdentityReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            if (socket.Connected)
            {
                int received = 0;
                try
                {
                    received = socket.EndReceive(ar);
                }
                catch (Exception)
                {

                }

                if (received != 0)
                {
                    byte[] dataBuf = new byte[received];
                    Array.Copy(_buffer, dataBuf, received);
                    string text = Encoding.ASCII.GetString(dataBuf);
                    var user = JsonConvert.DeserializeObject<UserIdentityModel>(text);
                    string response = "Fail";
                    foreach (var item in userList)
                    {
                        if (item.Name == user.Name && item.Password == user.Password)
                        {
                            response = "Ok";
                            lstProgram.Items.Add(item.Name);
                        }
                    }
                    SendData(socket, response);
                }
            }
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(UserIdentityReceiveCallback), socket);
        }

        private void PlanAcceptCallback(IAsyncResult ar)
        {
            Socket socket = _planSocket.EndAccept(ar);
            _PlanClients.Add(socket);
            ServerResponsePlanlamaClients model = new ServerResponsePlanlamaClients();
            model.IsEmriList = _IsEmriList;
            model.MakinaList = _MakinaList;
            string response = JsonConvert.SerializeObject(model);
            foreach (var item in _PlanClients)
            {
                SendData(item, response);
            }
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(PlanReceiveCallback), socket);
            _planSocket.BeginAccept(new AsyncCallback(PlanAcceptCallback), null);
        }
        private void PlanReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            if (socket.Connected)
            {
                int received = 0;
                try
                {
                    received = socket.EndReceive(ar);
                }
                catch (Exception)
                {

                    for (int i = 0; i < _PlanClients.Count; i++)
                    {
                        if (_PlanClients[i].RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            _PlanClients.RemoveAt(i);
                        }
                    }
                }
                if (received != 0)
                {
                    byte[] dataBuf = new byte[received];
                    Array.Copy(_buffer, dataBuf, received);
                    string text = Encoding.ASCII.GetString(dataBuf);
                    var emir = JsonConvert.DeserializeObject<IsEmri>(text);
                    emir.Id = IsEmriId;
                    IsEmriId++;
                    _IsEmriList.Add(emir);
                    ServerResponsePlanlamaClients model = new ServerResponsePlanlamaClients();
                    model.IsEmriList = _IsEmriList;
                    model.MakinaList = _MakinaList;
                    string response = JsonConvert.SerializeObject(model);
                    foreach (var item in _PlanClients)
                    {
                        SendData(item, response);
                    }
                    Matching();
                }
                else
                {
                    for (int i = 0; i < _PlanClients.Count; i++)
                    {
                        if (_PlanClients[i].RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            _PlanClients.RemoveAt(i);
                        }
                    }
                }
            }
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(PlanReceiveCallback), socket);
        }


        private void AcceptCallback(IAsyncResult ar)
        {
            Socket socket = _serverSocket.EndAccept(ar);
            MakinaList newMakina = new MakinaList();
            newMakina.Makina = new Makina { MakinaId = MakinaId };
            newMakina.Socket = socket;
            _MakinaSockets.Add(newMakina);
            MakinaId++;
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            if (socket.Connected)
            {
                int received = 0;
                try
                {
                    received = socket.EndReceive(ar);
                }
                catch (Exception)
                {
                    for (int i = 0; i < _MakinaSockets.Count; i++)
                    {
                        if (_MakinaSockets[i].Socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            _MakinaSockets.RemoveAt(i);
                            lblConnectionCount.Text = "Client sayısı:" + _MakinaSockets.Count.ToString();
                        }
                    }
                }
                if (received != 0)
                {
                    byte[] dataBuf = new byte[received];
                    Array.Copy(_buffer, dataBuf, received);
                    string text = Encoding.ASCII.GetString(dataBuf);
                    var Makina = JsonConvert.DeserializeObject<Makina>(text);
                    foreach (var item in _MakinaSockets)
                    {
                        if (item.Socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            item.Makina.Durum = Makina.Durum;
                            item.Makina.Isler = Makina.Isler;
                            item.Makina.MakinaAdi = Makina.MakinaAdi;
                            item.Makina.Tip = Makina.Tip;
                            item.Makina.UretimHizi = Makina.UretimHizi;
                            _MakinaList.Add(item.Makina);
                            lstClients.Items.Add(item.Makina.MakinaAdi);
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < _MakinaSockets.Count; i++)
                    {
                        if (_MakinaSockets[i].Socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            _MakinaSockets.RemoveAt(i);
                            lblConnectionCount.Text = "Client sayısı:" + _MakinaSockets.Count.ToString();
                        }
                    }
                }
            }
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }
        void SendData(Socket socket, string response)
        {
            byte[] data = Encoding.ASCII.GetBytes(response);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
        }
        private void SendCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }

        private void Matching()
        {
            foreach (var job in _IsEmriList.Where(w => w.isFinished == false))
            {
                foreach (var makina in _MakinaList.Where(w => w.Durum == MakinaStatus.EMPTY))
                {
                    if (job.Tip == makina.Tip)
                    {
                        if (makina.Isler == null)
                        {
                            makina.Isler = new List<IsEmri>();
                        }
                        makina.Isler.Add(job);
                        makina.Durum = MakinaStatus.BUSY;
                    }
                }
            }

            foreach (var makina in _MakinaList.Where(w => w.Durum == MakinaStatus.BUSY))
            {
                int makinaHız = makina.UretimHizi;
                int makinaAzalan = makina.UretimHizi;
                int jobToplam = 0;
                foreach (var job in makina.Isler)
                {
                    jobToplam += job.Miktar;
                    makinaAzalan -= job.Miktar;
                    if (makinaAzalan > job.Miktar)
                    {
                        job.isFinished = true;
                    }
                }
                if (makinaHız > jobToplam)
                {
                    makina.Durum = MakinaStatus.EMPTY;
                }

            }
            foreach (var sockets in _MakinaSockets)
            {
                foreach (var makina in _MakinaList )
                {
                    if (makina.MakinaId == sockets.Makina.MakinaId)
                    {
                        string data = JsonConvert.SerializeObject(makina);
                        SendData(sockets.Socket, data);
                    }
                }
            }
            ServerResponsePlanlamaClients model = new ServerResponsePlanlamaClients();
            model.IsEmriList = _IsEmriList;
            model.MakinaList = _MakinaList;
            string response = JsonConvert.SerializeObject(model);
            foreach (var item in _PlanClients)
            {
                SendData(item, response);
            }
        }
    }
}
