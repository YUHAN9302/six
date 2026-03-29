using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 人物位置 : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        bool positionSet = false;

        // 先檢查場景裡的所有門是否有存門前位置
        DoorTrigger[] doors = FindObjectsOfType<DoorTrigger>();
        foreach (var door in doors)
        {
            string id = string.IsNullOrEmpty(door.doorID)
                ? UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_" + door.gameObject.name
                : door.doorID;

            Vector3? doorPos = 位置紀錄.GetDoorEntryPosition(id);
            if (doorPos.HasValue)
            {
                transform.position = doorPos.Value;
                Debug.Log($"從門前位置恢復: {id} -> {doorPos.Value}");
                positionSet = true;
                break; // 找到第一個門就用它
            }
        }

        // 如果沒有門前位置，再使用全域 LastPosition
        if (!positionSet)
        {
            Vector3? savedPos = 位置紀錄.GetPosition();
            if (savedPos.HasValue)
            {
                transform.position = savedPos.Value;
                Debug.Log($"從 LastPosition 恢復: {savedPos.Value}");
            }
        }

        // 回復動畫狀態
        string lastAnim = 位置紀錄.GetAnimState();
        if (!string.IsNullOrEmpty(lastAnim) && animator != null)
        {
            animator.Play(lastAnim);
        }

        // 清除全域 LastPosition 避免重複用到
        位置紀錄.ClearPosition();
    }
    public void SaveCurrentTransform()
    {
        // 保存位置
        位置紀錄.SetPosition(transform.position);

        // 保存動畫狀態
        if (animator != null)
        {
            string currentState = animator.GetCurrentAnimatorStateInfo(0).shortNameHash.ToString();
            currentState = animator.GetCurrentAnimatorStateInfo(0).IsName("走路") ? "走路" : "待機";
            位置紀錄.SetAnimState(currentState);
        }
    }
    // Update is called once per frame
}
