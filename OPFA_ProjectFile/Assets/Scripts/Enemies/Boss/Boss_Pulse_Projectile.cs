using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Pulse_Projectile : MonoBehaviour
{
    private Boss_Shoot bossProjectile;
    public Boss_Health bossHealthScript;

    public int randomSeconds;
    public int randomProjectile1;
    public int randomProjectile2;
    public int randomProjectile3;

    public int randomSpreadProjectile;

    // Start is called before the first frame update
    void Start()
    {
        GameObject boss = GameObject.Find("Boss");
        bossProjectile = boss.GetComponent<Boss_Shoot>();
    }

    public IEnumerator Pulse()
    {
        randomSeconds = Random.Range(7, 12);

        randomProjectile1 = Random.Range(10, 15);
        randomProjectile2 = Random.Range(20, 25);
        randomProjectile3 = Random.Range(30, 35);

        bossProjectile.SpawnProjectile(randomProjectile1);

        yield return new WaitForSeconds(0.5f);

        bossProjectile.SpawnProjectile(randomProjectile2);

        yield return new WaitForSeconds(0.5f);

        bossProjectile.SpawnProjectile(randomProjectile3);

        yield return new WaitForSeconds(randomSeconds);

        // Start coroutine again once it ends in the Boss_Health script
        bossHealthScript.isProjectileRunning = false;

    }

    public IEnumerator RandomSpread()
    {
        randomSpreadProjectile = Random.Range(10, 20);

        bossProjectile.RandomSpreadProjectile(10);

        yield return new WaitForSeconds(2f);

        bossHealthScript.isSpreadProjectileRunning = false;
    }
}
