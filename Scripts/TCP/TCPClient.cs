using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPClient : MonoBehaviour
{
    private readonly string targetIp = "192.168.5.5"; 
    [SerializeField]
    private int targetPort;

    private TcpClient client;
    private NetworkStream stream;
    private Queue<string> messageQueue = new();
    private bool isProcessing = false;

   /* private string logFilePath;

    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        // 设置日志文件路径
        logFilePath = Path.Combine(Application.persistentDataPath, "log.txt");

        // 订阅 logMessageReceived 事件
        Application.logMessageReceived += LogMessageToFile;
    }

    void OnDestroy()
    {
        // 取消订阅 logMessageReceived 事件
        Application.logMessageReceived -= LogMessageToFile;
    }

    // 将日志消息写入文件
    private void LogMessageToFile(string logString, string stackTrace, LogType type)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}] {logString}\n{stackTrace}\n";
        File.AppendAllText(logFilePath, logEntry);
    }*/

    public void SendMsg(string msg)
    {
        messageQueue.Enqueue(msg);
        if (!isProcessing)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        while (messageQueue.Count > 0)
        {
            isProcessing = true;
            string msg = messageQueue.Dequeue();
            yield return SendMessage(msg);
        }
        isProcessing = false;
    }

    private new IEnumerator SendMessage(string msg)
    {
        client = new TcpClient();
        var connect = client.BeginConnect(targetIp, targetPort, null, null);

        // 等待连接完成
        while (!connect.IsCompleted)
            yield return null;

   
            client.EndConnect(connect);
            Debug.Log("Connected to server.");
            stream = client.GetStream();

            // 发送初始消息
            yield return SendMessageWithoutConnect(msg);
    

    }

    // 封装发送消息的功能为协程
    private IEnumerator SendMessageWithoutConnect(string message)
    {
        if (stream != null)
        {
            byte[] dataToSend = Encoding.UTF8.GetBytes(message);

            // 异步发送消息
            var asyncSend = stream.BeginWrite(dataToSend, 0, dataToSend.Length, null, null);
            while (!asyncSend.IsCompleted)
                yield return null;
  
                stream.EndWrite(asyncSend);

                Debug.Log("Sent: " + message);

                // 开始接收响应
                yield return ReceiveMessage();

        }
        else
        {
            Debug.Log("Stream is not available.");
        }
    }

    // 接收消息的协程
    private IEnumerator ReceiveMessage()
    {
        byte[] receivedBytes = new byte[1024];

        var asyncReceive = stream.BeginRead(receivedBytes, 0, receivedBytes.Length, null, null);
        while (!asyncReceive.IsCompleted)
            yield return null;
        try
        {
            int bytesRead = stream.EndRead(asyncReceive);
            string receivedMessage = Encoding.UTF8.GetString(receivedBytes, 0, bytesRead);
            Debug.Log("Received: " + receivedMessage);

            // 关闭连接
            stream.Close();
            client.Close();
            Debug.Log("Connection closed.");
        }
        catch (Exception e)
        {
            Debug.Log("Failed to receive message: " + e.Message);
        }
    }
}
