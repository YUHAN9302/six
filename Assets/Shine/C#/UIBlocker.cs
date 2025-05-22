using UnityEngine;
using UnityEngine.EventSystems;

public class UIBlocker : MonoBehaviour
{
    //public GameObject blockPanel;         // ���e���z���B�n�]UI�� Image + CanvasGroup�^
    public MonoBehaviour[] scriptsToDisable; // �A�n�ȮɸT�Ϊ��}���]�p���a����^

    void OnEnable()
    {
        // �ҥξB�n
       // if (blockPanel != null)
       //     blockPanel.SetActive(true);

        // ��ܷƹ�
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        // �T�Ϋ��w�}��
        foreach (var script in scriptsToDisable)
        {
            if (script != null)
                script.enabled = false;
        }

        //�]�i��^�Ȱ��ɶ�
        // Time.timeScale = 0;
    }

    void OnDisable()
    {
        // �����B�n
        //if (blockPanel != null)
         //   blockPanel.SetActive(false);

        // ���÷ƹ�
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // �٭�}��
        foreach (var script in scriptsToDisable)
        {
            if (script != null)
                script.enabled = true;
        }

        //�]�i��^��_�ɶ�
        // Time.timeScale = 1;
    }
}
