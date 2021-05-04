using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;
    //public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField]
    float rotationSpeed = 3;
    float hAxis;
    float vAxis;
    bool fDown;

    Vector3 moveVec;   

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

        if (moveVec != Vector3.zero) //움직이고있다면
        {
            float angle = Mathf.Atan2(moveVec.x, moveVec.z) * Mathf.Rad2Deg;

            if (90.0f > angle && angle > -90.0f)
            {
                angle = angle * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, angle);
            }
        }

        if (vAxis < 0  && fDown == true) // 뒤로가는중인데 shift를 눌렀을 때
        {
            transform.Translate(moveVec * speed * 0.5f * 0.5f * Time.deltaTime);
            anim.SetBool("isSpeedDoping", false); //shift를 무효화해줌
        }
        else //나머지
        {
            transform.Translate(moveVec * speed * (fDown ? 1f : 0.5f) * (vAxis < 0 ? 0.5f : 1f) * Time.deltaTime);
            anim.SetBool("isWalk", moveVec != Vector3.zero); // moveVec가 0이 아니라면(움직이고있다면) 걷기 애니메이션 실행
            anim.SetBool("isSpeedDoping", fDown); //fDown이 true,false인지 전달해주고 true면 애니메이션 실행
        }
    }
}