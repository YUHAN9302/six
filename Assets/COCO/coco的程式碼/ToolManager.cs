using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance;

    public TargetItem target;     // «½«½
    public ToolDrag needle;       // °w
    public ToolDrag thread;       // ½u
    public ToolDrag needleThread; // °w½u
    public ToolDrag scissor;      // °Å¤M

    void Awake()
    {
        Instance = this;
    }

    public void CheckDrop(ToolDrag tool)
    {
        string name = tool.toolName;

        if (name == "°w" || name == "½u" || name == "°w½u" || name == "¶s¦©" || name == "¥¬®Æ" || name == "°Å¤M")
        {
            target.OnToolDropped(tool);
        }
        else
        {
            tool.ResetPosition();
        }
    }
}
