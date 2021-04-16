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
    Rigidbody body;

    Vector3 moveVec;
    float curChaAngle;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        fDown = Input.GetButton("SpeedDoping");

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

        transform.Translate(moveVec * speed * (fDown ? 1f : 0.5f) * Time.deltaTime);

        //moveVec = transform.forward * moveVec.z + transform.right * moveVec.x;

        //body.velocity = moveVec * speed * Time.fixedDeltaTime;

        //transform.position += moveVec * speed *(fDown ? 1f : 0.5f )* Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);
        anim.SetBool("isSpeedDoping",fDown);

        //if(vAxis >= 0)
        //    transform.LookAt(transform.position + moveVec);

        //if(hAxis == 0 || vAxis == 0)
        //{
        //    curChaAngle = transform.rotation.y;
        //    //transform.localRotation *= Quaternion.Euler(0, curChaAngle, 0);
        //}
    }
}