using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Server
{
    internal class Manager
    {
        ListView Log;
        TextBox RoomCnt;
        TextBox UserCnt;

        public Manager(ListView log, TextBox room_count, TextBox user_count)
        {
            this.Log = log;
            this.RoomCnt = room_count;
            this.UserCnt = user_count;
        }

        public void WriteToLog(string line)
        {
            if (Log.InvokeRequired)
            {
                Log.Invoke(new Action(() =>
                {
                    Log.Items.Add(string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm"), line));
                }));
            }
            else
            {
                Log.Items.Add(string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm"), line));
            }
        }

        public void UpdateRoomCount(int num)
        {
            if (RoomCnt.InvokeRequired)
            {
                RoomCnt.Invoke(new Action(() =>
                {
                    RoomCnt.Text = num.ToString();
                }));
            }
            else
            {
                RoomCnt.Text = num.ToString();
            }
        }

        public void UpdateUserCount(int num)
        {
            if (UserCnt.InvokeRequired)
            {
                UserCnt.Invoke(new Action(() =>
                {
                    UserCnt.Text = num.ToString();
                }));
            }
            else
            {
                UserCnt.Text = num.ToString();
            }
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string GetLocalIPv4(NetworkInterfaceType type)
        {
            string localIPv4 = string.Empty;
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            localIPv4 = ip.Address.ToString();
                        }
                    }
                }
            }
            return localIPv4;
        }
    }
}
