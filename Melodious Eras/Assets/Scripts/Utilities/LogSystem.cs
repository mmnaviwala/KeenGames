using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogSystem : MonoBehaviour
{

    public List<string> logList = new List<string>();

    public void NewLog(string logMessage)
    {
        if (!logList.Contains(logMessage))
            logList.Add(logMessage);
    }
}
