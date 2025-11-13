using System.IO.Ports;
using UnityEngine;

public class BatRotationReceiver : MonoBehaviour
{
    [Header("Serial Port Settings")]
    public string portName = "COM5"; // Change as needed
    public int baudRate = 115200;

    private SerialPort serial;
    private string receivedData;

    [Header("Target Object")]
    public Transform batModel; // Assign in Inspector

    void Start()
    {
        // Initialize serial communication
        serial = new SerialPort(portName, baudRate);
        serial.ReadTimeout = 15;

        try
        {
            serial.Open();
            Debug.Log("Serial Port Connected: " + portName);
        }
        catch
        {
            Debug.LogWarning("Could not open serial port.");
        }
    }

    void Update()
    {
        if (serial == null || !serial.IsOpen)
            return;

        try
        {
            receivedData = serial.ReadLine();
            ProcessData(receivedData);
        }
        catch { }
    }

    void ProcessData(string data)
    {
        // Expected format: YPR, yaw, pitch, roll
        if (!data.StartsWith("YPR")) return;

        string[] parts = data.Split(',');

        if (parts.Length < 4) return;

        if (float.TryParse(parts[1], out float yaw) &&
            float.TryParse(parts[2], out float pitch) &&
            float.TryParse(parts[3], out float roll))
        {
            // Apply rotation to bat
            batModel.localRotation = Quaternion.Euler(pitch, yaw, roll);
        }
    }

    private void OnApplicationQuit()
    {
        if (serial != null && serial.IsOpen)
        {
            serial.Close();
        }
    }
}
