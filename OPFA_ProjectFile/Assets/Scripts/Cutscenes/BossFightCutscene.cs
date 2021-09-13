using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightCutscene : MonoBehaviour
{
    public GameObject explosionEffect;

    public GameObject whatText;
    public GameObject wasText;
    public GameObject thatText;

    public Animator transitionAnim;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Explosion());

        StartCoroutine(EndText());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(7f);
        explosionEffect.SetActive(true);
    }

    IEnumerator EndText()
    {
        yield return new WaitForSeconds(15f);

        whatText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        wasText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        thatText.SetActive(true);

        yield return new WaitForSeconds(3f);

        transitionAnim.SetTrigger("start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
