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
    public bool isHit = false;
    public float x, y;

    private void Start()
    {
        _udp = new UDP(host,port);
            
        var token = this.GetCancellationTokenOnDestroy();
        WaitReceive(token).Forget();
        SendFewSecond(token).Forget();
    }

    public void Send(string sendData)
    {
        _udp.Send(sendData).Forget();
    }
        
    async UniTaskVoid WaitReceive(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            CalcXY(await _udp.Receive(token));
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
        }
    }
    
    async UniTaskVoid SendFewSecond(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Send(isHit ? "true" : "False");
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
        }
    }

    void CalcXY(string data)
    {
        string[] dataString = data.Split(" ");
        x = int.Parse(dataString[0]) * 0.002f;
        y = int.Parse(dataString[1]) * 0.002f;
        Debug.Log("x: " + x + " y: " + y);
    }
}