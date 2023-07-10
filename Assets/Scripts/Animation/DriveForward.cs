using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveForward : MonoBehaviour
{
    private Rigidbody myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    public void Go()
    {
        StartCoroutine(AccelerationCoroutine());
    }

    private IEnumerator AccelerationCoroutine()
    {
        int framesToAccelerateFor = 90;
        int counter = 0;
        while (counter != framesToAccelerateFor)
        {
            float lerp = counter / (float)framesToAccelerateFor;
            lerp = EaseInSine(lerp);
            myRigidBody.AddRelativeForce(Vector3.forward * (20.0f * lerp));
            counter++;
            yield return new WaitForEndOfFrame();
        }
    }

    private float EaseInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }
}
