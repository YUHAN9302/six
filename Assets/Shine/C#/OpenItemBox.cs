using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenItemBox : MonoBehaviour
{
    [Tooltip("5 �ӹϤ����]�C�檺�� 0 �Ӥl����|�Q���/���á^")]
    public GameObject[] itemImages;   // 5 �� UI �e��
    [Tooltip("�����C�檺��T����/�������O")]
    public GameObject[] itemInfo;     // 5 �Ӹ�T���O

    private const int SLOT_COUNT = 5;

    private void OnEnable()
    {
        // �򥻨��b
        if (itemImages == null || itemImages.Length == 0)
            return;

        // ����ܦs�ɡG��������
        if (SetAndGetSaveData.SelectID == 0)
        {
            ToggleAllSlots(false);
            return;
        }

        // Ū�������s�ɪ��D��
        var saveMgr = FindObjectOfType<SaveManager>();
        if (saveMgr == null)
        {
            ToggleAllSlots(false);
            Debug.LogWarning("[OpenItemBox] �䤣�� SaveManager�C");
            return;
        }

        // �s�� API�G��m�B���סB�ɶ��B�D��
        var (_, _, _, items) = saveMgr.LoadPlayerState(SetAndGetSaveData.SelectID);

        if (items == null)
            items = new List<string>();

        // �T�O���צܤ� 5�]�����ɪŦr��^
        while (items.Count < SLOT_COUNT)
            items.Add(string.Empty);

        // �v���s���
        for (int i = 0; i < itemImages.Length; i++)
        {
            // �p�G UI �}�C�� 5 �h�Τ֡A�]���|�z
            bool hasItem = (i < items.Count) && !string.IsNullOrEmpty(items[i]);

            var slot = itemImages[i];
            if (slot == null) continue;

            // �� 0 �Ӥl��������u���~�ϥܡv
            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null)
                    icon.SetActive(hasItem);
            }
        }
    }

    /// <summary>
    /// �I���Y��A�Y�Ӯ榳���~�h���}��������T���O
    /// </summary>
    public void OpenItemInfo(int id)
    {
        if (itemImages == null || itemInfo == null) return;
        if (id < 0 || id >= itemImages.Length || id >= itemInfo.Length) return;

        var slot = itemImages[id];
        if (slot == null) return;

        // �ˬd�� 0 �Ӥl����]���~�ϥܡ^�ثe�O�_���
        if (slot.transform.childCount > 0)
        {
            var icon = slot.transform.GetChild(0).gameObject;
            if (icon != null && icon.activeSelf)
            {
                if (itemInfo[id] != null)
                    itemInfo[id].SetActive(true);
            }
        }
    }

    /// <summary>
    /// ��Ҧ���l���ϥ������]�Υ��}�^
    /// </summary>
    private void ToggleAllSlots(bool enable)
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            var slot = itemImages[i];
            if (slot == null) continue;

            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null) icon.SetActive(enable);
            }
        }
    }
}
