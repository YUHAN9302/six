using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 用餐劇情觸發 : MonoBehaviour
{
    public string 用餐劇情;  // 場景名稱
    public GameObject transitionAnimationObject;  // 放過場動畫物件（要有 Animator）
    public float animationDuration = 1f;  // 過場動畫長度(秒)

    private bool triggered = false;
    private static bool alreadyTriggered = false; // 靜態變數，跨場景保留狀態

    //[Header("開場物件")]
   // public 客廳開頭 開場物件;  // 指向開場物件

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && !alreadyTriggered && collision.CompareTag("Player"))
        {
           
            triggered = true;
            alreadyTriggered = true; // 記錄已觸發

            // ⭐ 刪除開場物件
            //if (開場物件 != null)
                //開場物件.DestroySelf(); // 使用客廳開場物件內的方法，會順便記錄 PlayerPrefs

            // 切換場景前保存位置與動畫
            FindObjectOfType<人物位置>().SaveCurrentTransform();

            StartCoroutine(PlayTransitionAndLoad());
        }
    }

    private IEnumerator PlayTransitionAndLoad()
    {
        if (transitionAnimationObject != null)
        {
            transitionAnimationObject.SetActive(true);
            Animator animator = transitionAnimationObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Start");  // 你的 Animator 需要有名為 Start 的 Trigger
                yield return new WaitForSeconds(animationDuration);
            }
            else
            {
                yield return new WaitForSeconds(animationDuration);
            }
        }
        else
        {
            yield return new WaitForSeconds(animationDuration);
        }

        SceneManager.LoadScene(用餐劇情);
    }
}
