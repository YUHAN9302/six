using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SETTING : MonoBehaviour
{
    public GameObject setting;
    public GameObject �Ȱ�����;

    private bool isSettingActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSettingActive)
        {
            
            Debug.Log("ESC Pressed - Close or Go Back to Game");
            // �o�̥i�H����h�X�C�����N�X�Ψ�L�ާ@
        }
        else
        {
            // �p�G�O "�s�ɤ���"�A�h��^ "SETTING ����"
            ReturnToSettingUI();
        }
    }
    public void SwitchToSaveUI()
    {
        if (setting != null && �Ȱ����� != null)
        {

            setting.SetActive(true);
            �Ȱ�����.SetActive(false);
        }
    }
    public void ReturnToSettingUI()
    {
        if (setting != null && �Ȱ����� != null)
        {
            setting.SetActive(false);  // Hide Save UI
            �Ȱ�����.SetActive(true); // Show Settings UI
        }
    }
}
