using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Humanoid/Pose")]
public class HumanoidPose : ScriptableObject
{
    public List<LimbPose> Poses = new List<LimbPose>();
}

[System.Serializable]
public class LimbPose
{
    public string LimbName;
    public Vector3 LocalPosition;
    public float Angle;

    public LimbPose(Limb limb)
    {
        LimbName = limb.name;
        LocalPosition = limb.transform.localPosition;
        Angle = limb.transform.localEulerAngles.z;
    }
}
