using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShower : MonoBehaviour
{
    [SerializeField] private float cooldown = 1;
    [SerializeField] private GameObject projectile;

    void Start()
    {
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Fire()
    {
        Instantiate(projectile, new Vector3(Random.Range
        (gameObject.transform.position.x - gameObject.transform.localScale.x,
        gameObject.transform.position.x + gameObject.transform.localScale.x),
        1, Random.Range
        (gameObject.transform.position.z - gameObject.transform.localScale.z,
        gameObject.transform.position.z + gameObject.transform.localScale.z)), Quaternion.identity);
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(Fire());
    }
}
