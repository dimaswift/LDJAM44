using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public float Torque;
    public Rigidbody2D Body { get; private set; }
    public HingeJoint2D Joint { get; private set; }

    public Rigidbody2D Bone;

    public float Inertia;

    public bool ApplyTorque = true;

    public float BreakForce;
    public float BreakTorque;
    public ParticleSystem SelfBloodFlow;
    public ParticleSystem ParentBloodFlow;


    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        Joint = GetComponent<HingeJoint2D>();

        //   Body.centerOfMass = Vector2.zero;

        Inertia = Body.inertia;
    }

    public static float To180Angle(float angle)
    {
        while (angle < -180.0f) angle += 360.0f;
        while (angle >= 180.0f) angle -= 360.0f;
        return angle;
    }

    private void FixedUpdate()
    {
        if (Joint != null && (Joint.reactionForce.magnitude > BreakForce || Joint.reactionTorque > BreakTorque))
        {
            RipOff();
        }
    }

    public LimbPose SavePose()
    {
        return new LimbPose(this);
    }

    public void RipOff()
    {
        SelfBloodFlow.gameObject.SetActive(true);
        ParentBloodFlow.gameObject.SetActive(true);
        Destroy(Joint);
        transform.SetParent(null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var c = collision.GetContact(0);
        if(c.normalImpulse > 100)
            EffectController.Instance.BloodEffect.Play(c.point, Helper.GetAngle(c.point, c.point + c.normal), c.normalImpulse / 1000);
    }

    public void MoveTowardBone(float torqueMultiplier)
    {
        //if (!ApplyTorque)
        //    return;
        //var angVelocity = Body.angularVelocity;
        //var current = transform.eulerAngles.z;
        //var desired = Bone.transform.eulerAngles.z;
        ////current = To180Angle(current);
        ////desired = To180Angle(desired);
        //Body.AddTorque(Mathf.DeltaAngle(current, desired) * (Torque * torqueMultiplier) - angVelocity);

    }
}
