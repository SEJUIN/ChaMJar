using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    float hAxis;
    float vAxis;
    bool fDown;

    Vector3 moveVec;
    float curChaAngle;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetInput();
        Move();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        fDown = Input.GetButton("SpeedDoping");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis);

        if (moveVec != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveVec.x, moveVec.z) * Mathf.Rad2Deg;

            if (90.0f > angle && angle > -90.0f)
            {
                angle = angle * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, angle);
            }
        }

        if (vAxis < 0 && fDown == true)
        {
            transform.Translate(moveVec * speed * 0.5f * (vAxis < 0 ? 0.5f : 1f) * Time.deltaTime);
            anim.SetBool("isSpeedDoping", false);
        }
        else
        {
            transform.Translate(moveVec * speed * (fDown ? 1f : 0.5f) * (vAxis < 0 ? 0.5f : 1f) * Time.deltaTime);

            anim.SetBool("isWalk", moveVec != Vector3.zero);
            anim.SetBool("isSpeedDoping", fDown);
        }
    }
}