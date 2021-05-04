using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    //    public Vector3 offset;
    public float dist = 5.0f;
    public float height = 5.0f;
    public float smoothRotate = 5.0f;

    void LateUpdate()
    {
        float curryAngle = Mathf.LerpAngle(transform.eulerAngles.y, target.eulerAngles.y, smoothRotate * Time.deltaTime);
        
        Quaternion rot = Quaternion.Euler(0, curryAngle, 0);

        transform.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);

        transform.LookAt(target.position + new Vector3(0, 5, 0));
    }

    void Update()
    {
        //transform.position = target.position + offset;
    }
}