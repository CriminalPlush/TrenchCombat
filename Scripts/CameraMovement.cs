using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public float limitationLeft;
    public float limitationRight;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0 & transform.position.x < limitationRight){
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * Input.GetAxis("Horizontal"), transform.position.y,transform.position.z);
        }
        else if(Input.GetAxis("Horizontal") < 0 & transform.position.x > limitationLeft){
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * Input.GetAxis("Horizontal"), transform.position.y,transform.position.z);
        }
        
    }
}
