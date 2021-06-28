using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotarcubito : MonoBehaviour
{
    public GameObject punto1;
    public GameObject punto2;
    private float a;
    Matrix4x4 matrix4 = new Matrix4x4();
    Quaternion ba;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.rotation.To;
            //print(a[1]);
            //print(b[1]);
            //print(a.eulerAngles);
            //print(b.eulerAngles);
            //print(a.normalized);
            //print(b.normalized);
            //print(a.Equals(b));
            //print(b.Equals(a));
            //a.Normalize();
            //b.Normalize();
            //a.Set(0,0,0,0);
            //b.Set(0,0,0,0);
            //a = new Quaternion(10, 0, 0, 0);
            //a.Normalize();
            //b.Normalize();
            //print(Quaternion.kEpsilon);
           // print(MyQuaternion.kEpsilon);
        }

        //Quaternion.Normalize(a);
        //MyQuaternion.Normalize(a);
        //Quaternion.Angle(a, b);
        //MyQuaternion.Angle(a, b);
        //Quaternion.AngleAxis(0, Vector3.up);
        //MyQuaternion.AngleAxis(0, Vector3.up);
        //Quaternion.Dot(a, b);
        //Quaternion.Euler(Vector3.up);
        //Quaternion.FromToRotation(Vector3.zero, Vector3.up);
        //Quaternion.Inverse(a);
        //Quaternion.Lerp(a, b, 10);
        //Quaternion.LerpUnclamped(a, b, 10);
        //Quaternion.LookRotation(Vector3.forward, Vector3.up);
        //Quaternion.RotateTowards(a, b, 10);
        //Quaternion.Slerp(a, b, 10);
        //Quaternion.SlerpUnclamped(a, b, 10);
        
    }
}
