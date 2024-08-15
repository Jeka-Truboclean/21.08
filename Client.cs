using System;
using System.Net.Sockets;
using System.Text;

namespace client;
class Client
{
    static void Main()
    {
        ConnectToServer();
    }

    static void ConnectToServer()
    {
        try
        {
            Console.Write("Requast is entered (time/date): ");
            string userInput = Console.ReadLine();

            TcpClient client = new TcpClient("127.0.0.1", 8888);
            Console.WriteLine("Connecting to the server...");

            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.ASCII.GetBytes(userInput);
            stream.Write(data, 0, data.Length);

            data = new byte[256];
            StringBuilder responseData = new StringBuilder();
            int bytes;

            do
            {
                bytes = stream.Read(data, 0, data.Length);
                responseData.Append(Encoding.ASCII.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            Console.WriteLine("Server answer: " + responseData.ToString());

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
        }
    }
}
