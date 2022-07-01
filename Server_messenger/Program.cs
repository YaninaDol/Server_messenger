using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 8008;
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("SERVER START");

            try
            {
                socket.Bind(iPEnd);
                socket.Listen(10);


                //берем клиента
                Socket clientSocket = socket.Accept();
                int f = 0;
                do
                {
                    Console.WriteLine("SERVER CATCH");
                    int byteCount = 0;
                    byte[] buffer = new byte[256];
                    StringBuilder stringBuilder = new StringBuilder();
                    do
                    {
                        byteCount = clientSocket.Receive(buffer);
                        stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));
                    } while (clientSocket.Available > 0);
                    string rez = stringBuilder.ToString();
                    if (rez.Equals(".txt"))
                    {
                        int byteCount2 = 0;
                        byte[] buffer2 = new byte[256];
                       
                        do
                        {
                            byteCount2 = clientSocket.Receive(buffer2);
                            
                        } while (clientSocket.Available > 0);
                        File.WriteAllBytes(@"C:\Users\1\OneDrive\Рабочий стол\CopyTXT.txt", buffer2);
                    }
                    if (rez.Equals(".png"))
                    {
                        int byteCount2 = 0;
                        byte[] buffer2 = new byte[256];
                        StringBuilder sizeBuilder = new StringBuilder();
                        do
                        {
                            byteCount2 = clientSocket.Receive(buffer2);
                            sizeBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));

                        } while (clientSocket.Available > 0);
                        int size = Convert.ToInt32(sizeBuilder.ToString());

                        int byteCount3 = 0;
                        byte[] buffer3 = new byte[size];
                      
                        do
                        {
                            byteCount3 = clientSocket.Receive(buffer3);
                           
                        } while (clientSocket.Available > 0);
                        File.WriteAllBytes(@"C:\Users\1\OneDrive\Рабочий стол\CopyPNG.png", buffer3);

                    }

                }
                while (f == 0);
              
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
               
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("SERVER END");
            Console.ReadKey();
        }
    }
}
