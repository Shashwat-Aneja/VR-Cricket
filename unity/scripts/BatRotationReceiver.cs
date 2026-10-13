using System.Globalization;
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
        // Expected format: YPR, yaw, pitch, roll. Serial values use decimal points.
        if (string.IsNullOrWhiteSpace(data) || batModel == null || !data.StartsWith("YPR")) return;

        string[] parts = data.Split(',');

        if (parts.Length < 4) return;

        if (float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float yaw) &&
            float.TryParse(parts[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float pitch) &&
            float.TryParse(parts[3].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float roll))
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
