using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShower : MonoBehaviour
{
    [SerializeField] private float cooldown = 1;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float duration = 10f;

    void Start()
    {
        StartCoroutine(Fire());
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Fire()
    {
        Instantiate(projectile, new Vector3(Random.Range
        (gameObject.transform.position.x - (gameObject.transform.localScale.x / 2),
        gameObject.transform.position.x + (gameObject.transform.localScale.x / 2)),
        1, Random.Range
        (gameObject.transform.position.z - (gameObject.transform.localScale.z/2),
        gameObject.transform.position.z + (gameObject.transform.localScale.z/2))), Quaternion.identity);
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(Fire());
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
