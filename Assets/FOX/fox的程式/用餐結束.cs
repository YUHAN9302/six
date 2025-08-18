using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 用餐結束 : MonoBehaviour
{
    public GameObject dialogueCanvas;               // 對話框 Canvas
    public string originalSceneName = "MainScene"; // 要跳回的場景
    public GameObject transitionAnimationObject;   // 過場動畫物件（需有 Animator）
    public float animationDuration = 1f;           // 過場動畫長度(秒)

    private bool triggered = false;

    void Update()
    {
        // 如果尚未觸發，且 Canvas 不存在或被關閉
        if (!triggered && dialogueCanvas != null && !dialogueCanvas.activeSelf)
        {
            triggered = true;
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
                animator.SetTrigger("Start"); // 你的 Animator 需要有名為 Start 的 Trigger
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

        // ★ 在跳回 A 場景前，標記餐桌動畫已播過
        判定餐桌動畫.TableAnimPlayed = true;

        SceneManager.LoadScene(originalSceneName);
    }
}
