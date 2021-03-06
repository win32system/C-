﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XOClient
{
    public class ClientApi
    {
        public TcpClient Client { private set; get; }
        public event EventHandler FreePlayersListChanged;
        public event EventHandler InviteOccur;
        public event EventHandler SuccessOccur;

        public ClientApi(EventHandler freePlayersHandler, EventHandler inviteOccurHandler, EventHandler successOccur)
        {
            Client = new TcpClient("localhost", 8888);

            FreePlayersListChanged += freePlayersHandler;
            InviteOccur += inviteOccurHandler;
            SuccessOccur += successOccur;
            Thread threadListener = new Thread(new ThreadStart(ListenLoop));
            threadListener.Start();
        }
        
        private void ListenLoop()
        {
            var netStream = Client.GetStream();
            var reader = new StreamReader(Client.GetStream());
            while (true)
            {
                if (netStream.DataAvailable)
                {
                    string message = reader.ReadLine();
                    if (message.StartsWith("broadcast:"))
                    {
                        message = message.Replace("broadcast:", "");
                        FreePlayersListChanged(message.Split(','), null);
                    }
                    else if (message.StartsWith("invite:"))
                    {
                        message = message.Replace("invite:", "");
                        InviteOccur(message, null);
                    }
                    else if (message.StartsWith("success:"))
                    {
                        message = message.Replace("success:", "");
                        SuccessOccur(message, null);
                        if (message.Equals("yes"))
                        {
                            break;
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        public void InviteResponse(string response)
        {
            WriteToStream(response);
        }

        public void SendInviteRequest(string request)
        {
            WriteToStream(request);
        }

        public void SendClientName(string name)
        {
            WriteToStream(name);
        }

        private void WriteToStream(string message)
        {
            var ns = Client.GetStream();
            while (ns == null) { }
            var writer = new StreamWriter(Client.GetStream());
            writer.WriteLine(message);
            writer.Flush();
        }
    }
}
