using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMovingPlatformDirection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            JointMotor2D motor = collision.gameObject.GetComponent<SliderJoint2D>().motor;
            motor.motorSpeed *= -1;
            collision.gameObject.GetComponent<SliderJoint2D>().motor = motor;
        }
    }
}
