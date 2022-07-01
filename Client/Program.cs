using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 8008;
            const string IP_ADDR = "127.0.0.1";
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP_ADDR), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("CLIENT START");
            try
            {
                socket.Connect(iPEnd);
                do
                {
                    Console.WriteLine(" Enter text: ");
                    string msg = Console.ReadLine();

                    byte[] data = Encoding.Unicode.GetBytes(msg);
                    socket.Send(data);
                    Console.WriteLine("CLIENT SEND DATA");
                    if (msg.Equals(".png"))
                    {
                        Console.WriteLine(" Enter size: ");
                        string sizepath = Console.ReadLine();

                        byte[] datasize = Encoding.Unicode.GetBytes(sizepath);
                        socket.Send(datasize);
                        Console.WriteLine("CLIENT SEND SIZE");
                    }
                    Console.WriteLine(" Enter path: ");
                    string filepath = Console.ReadLine();

                    byte[] datafile = File.ReadAllBytes(filepath);
                    socket.Send(datafile);
                    Console.WriteLine("CLIENT SEND FILE");
                }
                while (true);

                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            Console.WriteLine("CLIENT END");
            Console.ReadKey();
        }
    }
}
