using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{
    public Animator anim;
    public Animator anim2;
    public Animator roadAnim;

    public GameObject text1;

    public GameObject fadeToBlack;
    public GameObject fadeToBackground;
    public GameObject fadeInRoad;
    public GameObject pistol2;
    public GameObject bat1;
    public GameObject bat2;
    public GameObject road2;
    public GameObject fadeToBlackEnd;
    public GameObject fadeToBlackEnd2;

    public GameObject iText;
    public GameObject amText;
    public GameObject freeText;
    public GameObject creditsText;
    public GameObject specialText;
    public GameObject enjoyText;
    public GameObject titleText;

    public MainMenu menu;

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

        yield return new WaitForSeconds(5f);

        iText.SetActive(true);
        amText.SetActive(true);
        freeText.SetActive(true);

        yield return new WaitForSeconds(8f);

        anim2.SetTrigger("cutscene3");

        yield return new WaitForSeconds(0.5f);

        creditsText.SetActive(true);
        specialText.SetActive(true);

        yield return new WaitForSeconds(5f);

        bat1.SetActive(true);

        yield return new WaitForSeconds(15f);

        bat2.SetActive(true);

        yield return new WaitForSeconds(10f);

        road2.SetActive(true);

        yield return new WaitForSeconds(22f);

        roadAnim.SetTrigger("fadeOutRoad");
        fadeToBlackEnd.SetActive(true);

        yield return new WaitForSeconds(9f);

        fadeToBlackEnd2.SetActive(true);

        yield return new WaitForSeconds(2f);

        enjoyText.SetActive(true);

        yield return new WaitForSeconds(5f);

        titleText.SetActive(true);

        yield return new WaitForSeconds(5.5f);

        menu.BackToMenu();
    }
}
