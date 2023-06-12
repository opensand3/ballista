using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingController : MonoBehaviour
{
    public float power;
    public Vector2 minPower;
    public Vector2 maxPower;
    public Camera cam;

    private Vector2 force;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // getting rigidbody component and attaching it to the player
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector2 touchPos = touch.position;
        Vector3 touchPos3 = new Vector3(touchPos.x, touchPos.y);


        // mouse button down logic
        if (touch.phase == TouchPhase.Began)
        {
            // programming the start point of the click
            startPoint = cam.ScreenToWorldPoint(touchPos3);
            startPoint.z = 15f;
        }

        // mouse button held logic
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            // calculating the current point to which the mouse is being dragged down to
            // don't need z value (only on the xy plane)
            Vector3 currentPoint = cam.ScreenToWorldPoint(touchPos3);
            currentPoint.z = 15f;
        }

        // mouse button release logic
        if (touch.phase == TouchPhase.Ended)
        {
            // calculating the end point
            endPoint = cam.ScreenToWorldPoint(touchPos3);
            endPoint.z = 15f;

            // force which will be applied based on how much the user has dragged the mouse
            force = new Vector3(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

            // using the rigidbody we add an impulse force
            rigidBody.AddForce(force * power, ForceMode2D.Impulse);          
        }
    }
}
