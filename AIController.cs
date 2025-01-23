using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private float minSpawnCooldown = 10f;
    [SerializeField]
    private float maxSpawnCooldown = 25f;
    [SerializeField]
    private float minCommandCooldown = 10f;
    [SerializeField]
    private float maxCommandCooldown = 25f;
    [SerializeField]
    private GameObject[] units;
    void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(Command());
    }

    // Update is called once per frame
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnCooldown, maxSpawnCooldown));
        GameObject unit = Instantiate(units[Random.Range(0, units.Length)], GameObject.FindGameObjectsWithTag("EnemySpawner")[0].transform.position, Quaternion.identity);
        unit.tag = "Enemy";
        unit.GetComponent<Unit>().isEnemy = true;
        StartCoroutine(Spawn());
    }
    private IEnumerator Command()
    {
        yield return new WaitForSeconds(Random.Range(minCommandCooldown, maxCommandCooldown));
        UnitMovement[] units = FindObjectsOfType<UnitMovement>();
        foreach (UnitMovement x in units)
        {
            if (x.gameObject.tag == "Enemy")
            {
                x.commandQueue.Add("Move");
            }
        }
        StartCoroutine(Command());
    }
}
