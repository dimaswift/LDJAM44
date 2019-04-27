using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static void AddTorqueTowards(this Rigidbody2D body, float angle, float torque)
    {
     //   angle = Mathf.DeltaAngle(body.transform.eulerAngles.z, angle);
        body.AddTorque((Mathf.DeltaAngle(body.transform.eulerAngles.z, angle)) * torque - body.angularVelocity);
    }
    public static void AddTorqueTowardsLocal(this Rigidbody2D body, float angle, float torque)
    {
        angle = Mathf.MoveTowardsAngle(body.transform.localEulerAngles.z, angle, 1000f);
        body.AddTorque((body.transform.localEulerAngles.z - angle) * -torque - body.angularVelocity);
    }
    public static void AddTorqueTowards(this Rigidbody2D body, float angle, float gainConstant, int direction)
    {
        angle = To180(angle);
        direction = Mathf.Clamp(direction, -1, 1);
        body.AddTorque((Mathf.Abs(body.rotation - angle) * direction) * -gainConstant - body.angularVelocity);
    }


    public static float GetAngle(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 90;
    }
    public static float To180(this float angle)
    {
        while (angle < -180.0f) angle += 360.0f;
        while (angle >= 180.0f) angle -= 360.0f;
        return angle;
    }

    public static T Random<T>(this IList<T> array)
    {
        return array[UnityEngine.Random.Range(0, array.Count)];
    }
}
