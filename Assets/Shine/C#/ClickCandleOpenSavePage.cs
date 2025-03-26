using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCandleOpenSavePage : MonoBehaviour
{
    public GameObject SavePage;
    public GameObject AniUI;
    public AnimationClip anim;
    public GameObject PlayerAniUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SavePage.active)
            GetComponent<Collider2D>().enabled = false;
        else
            GetComponent<Collider2D>().enabled = true;

    }
    private void OnMouseDown()
    {
        AniUI.SetActive(true);
        StartCoroutine(Wait());
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(anim.length);
        SavePage.SetActive(true);
        PlayerAniUI.SetActive(false);
    }
}
