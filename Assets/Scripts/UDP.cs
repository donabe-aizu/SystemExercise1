using System.Text;
using System.Threading;
using System.Net.Sockets;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UDP
{
    private UdpClient _client;
        
    public UDP(string ip, int port)
    {
        _client = new UdpClient();
        _client.Connect(ip, port);
    }

    public async UniTaskVoid Send(string data, CancellationToken token = default)
    {
        byte[] message = Encoding.UTF8.GetBytes(data);
        await _client.SendAsync(message, message.Length);
        Debug.Log("Send: " + data);
    }

    public async UniTask<string> Receive(CancellationToken token = default)
    {
        var result = await _client.ReceiveAsync();
        byte[] getByte = result.Buffer;
        string data = Encoding.UTF8.GetString(getByte);
        Debug.Log("Receive: " + data);
        return data;
    }
}