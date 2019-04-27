using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour, IThrowable
{
    public HumanoidPose DefaultPose;

    List<Limb> limbs = new List<Limb>();

    public Limb Body;

    Dictionary<string, Limb> limbDict;

    public float Rotation { get => transform.eulerAngles.z; set => transform.eulerAngles = new Vector3(0, 0, value); }

    public Vector3 Position { get => transform.position; set => transform.position = value; }

    void Awake()
    {
        Init();

    }



    private void Start()
    {
        SetDefaultPose();
    }

    void Init()
    {
        GetComponentsInChildren(limbs);
        limbDict = new Dictionary<string, Limb>();
        foreach (var limb in limbs)
        {
            limbDict.Add(limb.name, limb);
        }
    }

    public void SetStatic(bool isStatic)
    {
        foreach (var limb in limbs)
        {
            limb.Body.bodyType = isStatic ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        }
    }

    public void SetDefaultPose()
    {
        Init();
        SetPose(DefaultPose);
    }

    public void AddForce(Vector2 force)
    {
        foreach (var limb in limbs)
        {
            limb.Body.AddForce(force);
        }
    }

    public void AddTorque(float torque)
    {
        Body.Body.AddTorque(torque);
    }

    public void SetPose(HumanoidPose pose)
    {
        foreach (var p in pose.Poses)
        {
            var limb = limbDict[p.LimbName];
            limb.transform.localEulerAngles = new Vector3(0, 0, p.Angle);
            limb.transform.localPosition = p.LocalPosition;
            limb.Body.velocity = Vector2.zero;
            limb.Body.angularVelocity = 0;
        }
    }

    public HumanoidPose SavePose()
    {
        Init();
        var pose = ScriptableObject.CreateInstance<HumanoidPose>();
        pose.name = "New Pose";
        foreach (var limb in limbs)
        {
            pose.Poses.Add(new LimbPose(limb));
        }
        return pose;
    }

    void SetDirty<T>(T target) where T : Component
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(target);
#endif
    }

    void DestroyComponents<T>(GameObject target) where T : Component
    {
        foreach (var c in target.GetComponentsInChildren<T>(true))
        {
            DestroyImmediate(c);
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void ResetPosition()
    {
        SetDefaultPose();
    }
}
