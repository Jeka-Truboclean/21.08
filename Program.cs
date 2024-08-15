using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using client;

class Server
{
    static void Main()
    {
        StartServer();
    }

    public static void StartServer()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 8888;
        TcpListener listener = new TcpListener(ipAddress, port);

        try
        {
            listener.Start();
            Console.WriteLine("Server is started...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("New client is connected.");

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();

                string response = string.Empty;

                if (request.ToLower() == "time")
                {
                    response = DateTime.Now.ToString("HH:mm:ss");
                }
                else if (request.ToLower() == "date")
                {
                    response = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    response = "Invalid request";
                }

                byte[] data = Encoding.ASCII.GetBytes(response);
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sending answer: {response}");

                client.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
        }
        finally
        {
            listener.Stop();
        }
    }
}
