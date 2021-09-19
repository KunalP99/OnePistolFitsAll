using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{
    public Animator anim;
    public Animator anim2;

    public GameObject text1;

    public GameObject fadeToBlack;
    public GameObject fadeToBackground;
    public GameObject fadeInRoad;
    public GameObject pistol2;
    public GameObject bat1;
    public GameObject bat2;

    public GameObject iText;
    public GameObject amText;
    public GameObject freeText;
    public GameObject creditsText;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartCutscenes());
    }

    IEnumerator StartCutscenes()
    {
        yield return new WaitForSeconds(3f);

        text1.SetActive(true);

        yield return new WaitForSeconds(4.5f);

        anim.SetTrigger("cutscene1");

        yield return new WaitForSeconds(7f);

        fadeToBlack.SetActive(true);

        yield return new WaitForSeconds(6f);

        fadeToBackground.SetActive(true);
        fadeInRoad.SetActive(true);

        yield return new WaitForSeconds(2f);

        pistol2.SetActive(true);
        anim2.SetTrigger("cutscene2");

        // Play music here

        yield return new WaitForSeconds(5f);

        iText.SetActive(true);
        amText.SetActive(true);
        freeText.SetActive(true);

        yield return new WaitForSeconds(8f);

        anim2.SetTrigger("cutscene3");

        yield return new WaitForSeconds(0.5f);

        creditsText.SetActive(true);

        yield return new WaitForSeconds(5f);

        bat1.SetActive(true);

        yield return new WaitForSeconds(15f);

        bat2.SetActive(true);
    }
}
