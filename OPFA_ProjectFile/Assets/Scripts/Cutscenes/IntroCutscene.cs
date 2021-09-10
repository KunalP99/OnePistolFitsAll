using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    public Animator transitionAnim;
    public Animator pistolAnim;
    public Animator emAnim;

    public AudioSource batNoise;

    public GameObject introBat;
    public GameObject introBat2;
    public GameObject introBat3;

    public GameObject iText;
    public GameObject hateText;
    public GameObject batsText;

    // Update is called once per frame
    void Update()
    {
        pistolAnim.SetTrigger("Cutscene2");
        pistolAnim.SetTrigger("Cutscene3");
        pistolAnim.SetTrigger("Cutscene4");

        StartCoroutine(Bat());

        pistolAnim.SetTrigger("Cutscene5");
        pistolAnim.SetTrigger("Cutscene6");

        StartCoroutine(EndText());
    }

    IEnumerator Bat()
    {
        yield return new WaitForSeconds(14.5f);

        introBat.SetActive(true);
        introBat2.SetActive(true);
        introBat3.SetActive(true);
    }

    IEnumerator EndText()
    {
        yield return new WaitForSeconds(24f);

        iText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        hateText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        batsText.SetActive(true);

        yield return new WaitForSeconds(3f);

        transitionAnim.SetTrigger("start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Skip()
    {
        transitionAnim.SetTrigger("start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SkipCutscene()
    {
        StartCoroutine(Skip());
    }
}
