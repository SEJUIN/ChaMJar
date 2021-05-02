using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    //public float Speed { get { return speed; } set { speed = value; } }
    public float rotationSpeed = 3;
    float hAxis;
    float vAxis;
    bool fDown;

    public float finalMoveVecX;
    public float finalMoveVecZ;
    public float finalAngle;

    Vector3 finalMoveVec;
    public Vector3 moveVec;
    

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
            //Debug.Log(Mathf.Atan2(moveVec.x, moveVec.z));

            //Debug.Log(angle);
            if (90.0f > angle && angle > -90.0f)
            {
                angle = angle * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, angle);
                finalAngle = angle;//transform.rotation.y;
            }
        }

        if (vAxis < 0  && fDown == true) // 뒤로가는중인데 shift를 눌렀을 때
        {
            finalMoveVec = moveVec * speed * 0.5f * 0.5f * Time.deltaTime; // 이동정도
            finalMoveVecX = finalMoveVec.x; // x축의 이동정도(거리)
            finalMoveVecZ = finalMoveVec.z; // z축의 이동정도(거리)
            transform.Translate(finalMoveVec); // 이동정도를 지금 player에게 적용시켜줌
            anim.SetBool("isSpeedDoping", false); //shift를 무효화해줌
        }
        else //나머지
        {
            Debug.Log("moveVec" + moveVec);
            finalMoveVec = moveVec * speed * (fDown ? 1f : 0.5f) * (vAxis < 0 ? 0.5f : 1f) * Time.deltaTime; //이동정도
            finalMoveVecX = finalMoveVec.x; // x축의 이동정도(거리)
            Debug.Log("finalMoveVecX" + finalMoveVecX);
            finalMoveVecZ = finalMoveVec.z; // z축의 이동정도(거리)
            Debug.Log("finalMoveVecZ" + finalMoveVecZ);
            transform.Translate(finalMoveVec); // 이동정도를 지금 player에게 적용시켜줌
            Debug.Log("finalMoveVec"+finalMoveVec);
            anim.SetBool("isWalk", moveVec != Vector3.zero); // moveVec가 0이 아니라면(움직이고있다면) 걷기 애니메이션 실행
            anim.SetBool("isSpeedDoping", fDown); //fDown이 true,false인지 전달해주고 true면 애니메이션 실행
        }
    }
}