using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieBG_Spawn : MonoBehaviour
{
    [SerializeField] GameObject cookies;
    [SerializeField] float secondSpawn = 3f;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;

    void Start()
    {
        StartCoroutine(CookieSpawn());
    }

    IEnumerator CookieSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(cookies,
                position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            Destroy(gameObject, 8f);
        }
    }
}
