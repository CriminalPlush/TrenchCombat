using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    private float limitationLeft;
    private float limitationRight;
    private Vector2 startPos;
    private float swipeSpeed = 0.009f;
    private Vector2 lastTouchPosition;

    void Start()
    {
        limitationLeft = GameObject.FindGameObjectWithTag("StartPoint").transform.position.x + 11.5f;
        limitationRight = GameObject.FindGameObjectWithTag("EndPoint").transform.position.x - 11.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isMobilePlatform)
        {
            if (Input.GetAxis("Horizontal") > 0 & transform.position.x < limitationRight)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * Input.GetAxis("Horizontal"), transform.position.y, transform.position.z);
            }
            else if (Input.GetAxis("Horizontal") < 0 & transform.position.x > limitationLeft)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * Input.GetAxis("Horizontal"), transform.position.y, transform.position.z);
            }
        }
        else
        {
            // Track a single touch as a direction control.
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        // Record initial touch position.
                        startPos = touch.position;
                        break;

                    //Determine if the touch is a moving touch
                    case TouchPhase.Moved:
                        // Determine direction by comparing the current touch position with the initial one
                        transform.position -= new Vector3(swipeSpeed * (touch.position - startPos).x, 0, 0);
                        if (transform.position.x < limitationLeft) transform.position = new Vector3(limitationLeft, transform.position.y, transform.position.z);
                        if (transform.position.x > limitationRight) transform.position = new Vector3(limitationRight, transform.position.y, transform.position.z);
                        startPos = touch.position;
                        break;

                    case TouchPhase.Ended:
                        break;
                }
            }
        }

    }
}
