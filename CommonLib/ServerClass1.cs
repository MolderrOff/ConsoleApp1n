using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1
{

    public interface IMessageSource
    {
        void Send(ChatMessage message, IPEndPoint ep);
        ChatMessage Receive(ref IPEndPoint ep);
    }
    public class UdpMessageSource : IMessageSource
    {
        private UdpClient udpClient;
        public UdpMessageSource()
        {
            udpClient = new UdpClient(12345);
        }
        public ChatMessage Receive(ref IPEndPoint ep)
        {
            byte[] receiveBytes = udpClient.Receive(ref ep);
            string receivedData = Encoding.ASCII.GetString(receiveBytes);
            return ChatMessage.FromJson(receivedData);
        }
        public void Send(ChatMessage message, IPEndPoint ep)
        {
            byte[] forwardBytes = Encoding.ASCII.GetBytes(message.ToJson());
            udpClient.Send(forwardBytes, forwardBytes.Length, ep);
        }






    }
}
