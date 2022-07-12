using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using MR.ConsoleClient;

public class ConsoleApp
{
    public static void Main()
    {
        //var client = new ClientHandler(new ConsoleApp());
        //string ipAddrPort = "127.0.0.1:4530";

        //Console.WriteLine($"Connecting to server at {ipAddrPort} using TCP");

        //client.Connect(ipAddrPort, ClientHandler.Protocol.TCP);


        Form1 form1 = new Form1();
        form1.ShowDialog();

        //while (true)
        //{
        //    if (!client.isConnected) continue;

        //    // read input
        //    string buffer = Console.ReadLine();

        //    // send to server
        //    var parameters = new Dictionary<byte, object> { { OperationCode.Broadcast, buffer } };
        //    client.peer.OpCustom(OperationCode.Broadcast, parameters, true);
        //}
    }

}