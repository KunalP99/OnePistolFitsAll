using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    public Animator pistolAnim;

    public AudioSource batNoise;

    public GameObject introBat;

    // Update is called once per frame
    void Update()
    {
        pistolAnim.SetTrigger("Cutscene2");

        StartCoroutine(Wait(1));

        pistolAnim.SetTrigger("Cutscene3");

        StartCoroutine(Wait(1));

        pistolAnim.SetTrigger("Cutscene4");

        StartCoroutine(Wait(1));

        StartCoroutine(Bat());

    }

    IEnumerator Wait(int waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
    }

    IEnumerator Bat()
    {
        yield return new WaitForSeconds(14.5f);

        introBat.SetActive(true);
    }
}
