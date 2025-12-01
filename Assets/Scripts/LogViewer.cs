using UnityEngine;
using UnityEngine.UI;

public class LogViewer : MonoBehaviour
{
    public Text debugText; // 화면에 만든 Text 연결
    string myLog = "";

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString + "\n" + myLog;
        if (myLog.Length > 500) myLog = myLog.Substring(0, 500); // 너무 길면 자름

        if (debugText != null) debugText.text = myLog;
    }
}