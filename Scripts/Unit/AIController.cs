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
    private GetOutCommand[] getOutCommand;
    void Start()
    {
        getOutCommand = FindObjectsOfType<GetOutCommand>();
        StartCoroutine(Spawn());
        StartCoroutine(Command());
    }

    // Update is called once per frame
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnCooldown, maxSpawnCooldown));
        GameObject unit = Instantiate(units[Random.Range(0, units.Length)], GameObject.FindGameObjectsWithTag("EnemySpawner")[0].transform.position, Quaternion.identity);
        unit.GetComponent<Unit>().isEnemy = true;
        StartCoroutine(Spawn());
    }
    private IEnumerator Command()
    {
        yield return new WaitForSeconds(Random.Range(minCommandCooldown, maxCommandCooldown));
        foreach (var command in getOutCommand)
        {
            command.GetOutEnemy();
            Debug.Log("Sigmus");
        }
        Debug.Log("Something Happened!");
        StartCoroutine(Command());
    }
}
