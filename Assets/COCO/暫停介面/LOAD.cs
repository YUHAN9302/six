using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOAD : MonoBehaviour
{
    public GameObject Load;
    public GameObject �Ȱ�����;

    private bool isLoadActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoadActive)
        {
            
            Debug.Log("ESC Pressed - Close or Go Back to Game");
            // �o�̥i�H����h�X�C�����N�X�Ψ�L�ާ@
        }
        else
        {
            // �p�G�O "�s�ɤ���"�A�h��^ "Load ����"
            ReturnToLoadUI();
        }


    }
    public void SwitchToSaveUI()
    {
        if (Load != null && �Ȱ����� != null)
        {

            Load.SetActive(true);
            �Ȱ�����.SetActive(false);
        }
    }
    public void ReturnToLoadUI()
    {
        if (Load != null && �Ȱ����� != null)
        {
            Load.SetActive(false);  // Hide Save UI
            �Ȱ�����.SetActive(true); // Show LOADs UI
        }
    }
}
