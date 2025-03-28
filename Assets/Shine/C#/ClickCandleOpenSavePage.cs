using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ClickCandleOpenSavePage : MonoBehaviour
{
    public GameObject SavePage;
    public GameObject AniUI2;
    public GameObject AniUI;
    public AnimationClip anim;
    public GameObject PlayerAniUI;
    public Light2D[] candleLights;
    
    // Start is called before the first frame update
    void Start()
    {
        AniUI2.SetActive(true);
        foreach (Light2D light in candleLights)
        {
            if (light != null)
            {
                light.enabled = false;
            }
        }
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
        AniUI2.SetActive(false);
        AniUI.SetActive(true);
        foreach (Light2D light in candleLights)
        {
            if (light != null)
            {
                light.enabled = true;
            }
        }        
        StartCoroutine(Wait());
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(anim.length);
        SavePage.SetActive(true);
        PlayerAniUI.SetActive(false);
    }
}
