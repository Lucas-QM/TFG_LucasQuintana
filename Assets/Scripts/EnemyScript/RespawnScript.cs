using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public float timeToRespawn;
    public bool isRespawning;
    private GameObject enemyToRespawn;
    // Start is called before the first frame update
    void Start()
    {
        enemyToRespawn = transform.GetChild(0).gameObject;
    }

    public IEnumerator RespawnEnemy()
    {
        enemyToRespawn.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        enemyToRespawn.SetActive(true);

        enemyToRespawn.GetComponent<Enemy>().hp = enemyToRespawn.GetComponent<EnemyHealth>().originalHealth;
        enemyToRespawn.GetComponent<SpriteRenderer>().material = enemyToRespawn.GetComponent<Blink>().original;
        enemyToRespawn.GetComponent<EnemyHealth>().isDamaged = false;

        yield return RespawnAnim();
    }

    IEnumerator RespawnAnim()
    {
        isRespawning = true;
        enemyToRespawn.GetComponent<Animator>().SetBool("isRespawning", true);
        yield return new WaitForSeconds(0.4f);
        enemyToRespawn.GetComponent<Animator>().SetBool("isRespawning", false);
        isRespawning = false;
    }
}
