using UnityEngine;

public class MonitorStore
{
    public static string MonitorState {
        get
        {
             if(monitorState == string.Empty)
            {
                monitorState = PlayerPrefs.GetString("monitorState", "visible");
            }
             return monitorState;
        }
        set
        {
            monitorState = value;
            PlayerPrefs.SetString("monitorState", value);
        }
    }

    private static string monitorState = string.Empty;
}

