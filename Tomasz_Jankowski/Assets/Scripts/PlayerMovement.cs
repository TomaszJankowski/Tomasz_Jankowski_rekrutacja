using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject leftArm;
    public float speed = 1;
    AreaEffector2D lArmEffector;

    float timer, horizontal;
    bool push, goingLeft, rotated;
    private void Start()
    {
        if(leftArm != null)
        {
            lArmEffector = leftArm.GetComponent<AreaEffector2D>();
        }
    }
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 0.3f)
        {
            push = Input.GetButton("Fire1");
            horizontal = Input.GetAxis("Horizontal");
        }
        InputHandler();
        Rotate();
    }

    void InputHandler()
    {

        if (push)
        {
            if (leftArm.transform.localPosition.x <= 0.6f)
            {
                leftArm.transform.localPosition += new Vector3(7f * Time.deltaTime, 0, 0);
                leftArm.GetComponent<BoxCollider2D>().enabled = true;
                timer = 0f;
            }
        }
        else
        {
            if (leftArm.transform.localPosition.x >= 0.2f)
            {
                leftArm.GetComponent<BoxCollider2D>().enabled = false;
                leftArm.transform.localPosition -= new Vector3(7f * Time.deltaTime, 0, 0);
            }
        }

        if(Mathf.Abs(horizontal) > 0 && !push)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0) * horizontal;
        }
    }

    void Rotate()
    {

        if (horizontal > 0)
        {
            goingLeft = false;
        }
        else if (horizontal < 0)
        {
            goingLeft = true;
        }

        if (goingLeft)
        {
            if (!rotated)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                lArmEffector.forceAngle = 90;
                rotated = true;
            }
        }
        else
        {
            if (rotated)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                lArmEffector.forceAngle = -90;
                rotated = false;
            }
        }
    }
}
