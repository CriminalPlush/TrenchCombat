using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [SerializeField] private GameObject area;
    [SerializeField] private GameObject fireShower;
    public float cooldown = 30; // used by SpecialAttackButton
    private GameObject areaInstance;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (areaInstance != null)
                Instantiate(fireShower, areaInstance.transform.position, Quaternion.identity);
                this.enabled = false;
        }   
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                areaInstance = Instantiate(area, new Vector3(hitInfo.point.x, 1, 0), Quaternion.identity);
            }
        }
        else if (Input.GetMouseButton(0) == false)
        {
            Destroy(areaInstance);
            areaInstance = null;
        }
        if (areaInstance != null)
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            areaInstance.transform.position = new Vector3(MousePos.x, 1, 0);
        }
    }
}
