using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
     DialogueManager dialogueManager; // �qExcelŪ�������
    public Text speakerText;
    public Text contentText;
    public Button nextButton;

    public int currentLine = 0;
    [Header("��ܵ��� �@�P�n����������")]
    public GameObject CloseObj;
    void Start()
    {
        dialogueManager = this.GetComponent<DialogueManager>();
        nextButton.onClick.AddListener(ShowNextLine);
        ShowNextLine(); // �@�}�l��ܲĤ@�y
    }

    void ShowNextLine()
    {
        if (currentLine < dialogueManager.dialogueLines.Count)
        {
            var line = dialogueManager.dialogueLines[currentLine];
            speakerText.text = line.speaker;
            contentText.text = line.content;
            currentLine++;
        }
        else
        {
           gameObject.SetActive(false);
            if(CloseObj!=null)
            CloseObj.SetActive(false);
        }
    }
    public void Reset()
    {
        currentLine = 0;
        var line = dialogueManager.dialogueLines[currentLine];
        speakerText.text = line.speaker;
        contentText.text = line.content;
    }
}
