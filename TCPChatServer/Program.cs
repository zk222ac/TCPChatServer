using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPChatServer
{
    class Program
    {

        private static TcpClient _connectionSocket = null;
        private static TcpListener _serverSocket = null;
        private static IPAddress _ip = IPAddress.Parse("127.0.0.1");
        private static int _portNumber = 6789;
        private static Stream _nstream = null;
        private static StreamWriter _sWriter = null;
        private static StreamReader _sReader = null;
        private static string _msgFromClient = null;


        static void Main(string[] args)
        {
            try
            {
                // Step no: 2..............................................
                // create handshake , then welcoming server socket 
                _serverSocket = new TcpListener(_ip, _portNumber);
                // Start listening incoming request from client 
                _serverSocket.Start();
                Console.WriteLine("Server is being start");
                Console.WriteLine("Ready for Handshake Call from Client");
                using (_connectionSocket = _serverSocket.AcceptTcpClient())
                {
                    Console.WriteLine("Client connected with IP:" + ((IPEndPoint)_connectionSocket.Client.RemoteEndPoint).Address);
                    Console.WriteLine("Server is activated");

                    // Step no : 4 ...........................................
                    // Server recieved (byte of data) from client , Server perform read opertion
                    _nstream = _connectionSocket.GetStream();
                    _sReader = new StreamReader(_nstream);

                    _msgFromClient = _sReader.ReadLine();
                    Console.WriteLine("..............................................");
                    Console.WriteLine("Client Msg:" + _msgFromClient);
                    Console.WriteLine("..............................................");

                    // Step no: 5 ........................................
                    // Server modify or new message (client Message) sent back to client 
                    // perform write operation 
                    _sWriter = new StreamWriter(_nstream) { AutoFlush = true };
                    Console.WriteLine("Enter the message here for sending to client");
                    string modifyingClientMsgBackToServer = Console.ReadLine();
                    _sWriter.WriteLine(modifyingClientMsgBackToServer);
                   
                } // connection socket close automatically here 
                // STEP no : 7 
                //  TCP Listener stop 
                Console.WriteLine("Listener not listening anymore! STOP");
                _serverSocket.Stop();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
    }
}
