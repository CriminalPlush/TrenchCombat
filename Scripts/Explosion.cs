using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float autoDestroyTime = 1f;
    [SerializeField] private float damage;
    [SerializeField] private GameObject crater;
    bool craterSpawned = false;
    void Start()
    {
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ground")
        {
            SpawnCrater();
        }
        if (col.GetComponent<Unit>() != null)
        {
            col.GetComponent<Unit>().HP -= damage - (damage * col.GetComponent<Unit>().explosionResist);
            Destroy(this);
        }
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(autoDestroyTime);
        Destroy(gameObject);
    }
    private void SpawnCrater()
    {
        if (!craterSpawned)
        {
            craterSpawned = true;
            RaycastHit hitInfo;

            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hitInfo, 10))
            {
                if (hitInfo.collider.gameObject.tag == "Ground")
                    Instantiate(crater, hitInfo.point + new Vector3(0,0.1f,0), Quaternion.Euler(0, Random.Range(0, 359), 0));
            }
            else
            {
                Instantiate(crater, new Vector3(transform.position.x, 0.51f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 359), 0));
            }
        }
    }
}
