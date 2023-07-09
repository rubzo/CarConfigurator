using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    public float speedRPM;

    void Update()
    {
        // meaning fraction of a full revolution - we can multiply this by speedRPM to achieve the desired RPM
        float angleFraction = (Time.deltaTime / 60.0f) * 360.0f;

        Vector3 oldAngle = transform.localRotation.eulerAngles;
        oldAngle.y = (oldAngle.y + angleFraction * speedRPM) % 360.0f;

        transform.localRotation = Quaternion.Euler(oldAngle.x, oldAngle.y, oldAngle.z);
    }
}
