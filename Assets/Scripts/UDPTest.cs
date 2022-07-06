using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UDPTest : MonoBehaviour
{
    [SerializeField]
    private string host = "raspberrypi.local";
    [SerializeField]
    private int port = 9000;
        
    private UDP _udp;

    private void Start()
    {
        _udp = new UDP(host,port);
            
        var token = this.GetCancellationTokenOnDestroy();
        WaitReceive(token).Forget();
    }

    public void Send(string sendData)
    {
        _udp.Send(sendData).Forget();
    }
        
    async UniTaskVoid WaitReceive(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            string data = await _udp.Receive(token);
            Debug.Log(data);
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
        }
    }
}