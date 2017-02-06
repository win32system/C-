using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;


namespace HenesseyXO
{
    public class ServerApi
    {
        private TcpListener serverListener;
        private ConnectionList connList;
        
        public ServerApi()
        {
            serverListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            Thread threadConnection = new Thread(new ThreadStart(ConnectionLoop));
            threadConnection.Start();
            connList = new ConnectionList(this);
            List<Session> sessions = new List<Session>();
            RequestLoop requestLoop = new RequestLoop(this, connList, sessions);
        }

        private void ConnectionLoop()
        {
            serverListener.Start();
            while (true)
            {
                var clientSocket = serverListener.AcceptTcpClient();
                connList.AddUser(GetClient(clientSocket));
                Thread.Sleep(10);
            }
        }

        private Client GetClient(TcpClient clientSocket)
        {
            StreamReader reader = new StreamReader(clientSocket.GetStream());
            string name = String.Empty;
            while (true)
            {
                if (clientSocket.GetStream().DataAvailable)
                {
                    name = reader.ReadLine();
                    break;
                }
                Thread.Sleep(10);
            }
            return new Client(name, clientSocket);
        }
    }
}
