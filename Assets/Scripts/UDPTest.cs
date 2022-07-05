using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UDPTest : MonoBehaviour
{
    [SerializeField]
    private string _sendData = "test";
    [SerializeField]
    private string host = "192.168.1.8";
    [SerializeField]
    private int port = 9000;
        
    private UDP _udp;

    private void Start()
    {
        _udp = new UDP(host,port);
            
        var token = this.GetCancellationTokenOnDestroy();
        WaitReceive(token).Forget();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _udp.Send(_sendData).Forget();
        }
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