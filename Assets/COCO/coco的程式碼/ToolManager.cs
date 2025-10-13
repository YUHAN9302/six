using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance;

    public TargetItem target;     // 娃娃
    public ToolDrag needle;       // 針
    public ToolDrag thread;       // 線
    public ToolDrag needleThread; // 針線

    void Awake()
    {
        Instance = this;
    }

    public void CheckDrop(ToolDrag tool)
    {
        string name = tool.toolName;

        if (name == "針" || name == "線" || name == "針線" || name == "鈕扣" || name == "布料")
        {
            target.OnToolDropped(tool);
        }
        else
        {
            tool.ResetPosition();
        }
    }
}
