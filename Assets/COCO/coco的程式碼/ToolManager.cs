using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance;

    public TargetItem target;     // ����
    public ToolDrag needle;       // �w
    public ToolDrag thread;       // �u
    public ToolDrag needleThread; // �w�u
    public ToolDrag scissor;      // �ŤM

    void Awake()
    {
        Instance = this;
    }

    public void CheckDrop(ToolDrag tool)
    {
        string name = tool.toolName;

        if (name == "�w" || name == "�u" || name == "�w�u" || name == "�s��" || name == "����" || name == "�ŤM")
        {
            target.OnToolDropped(tool);
        }
        else
        {
            tool.ResetPosition();
        }
    }
}
