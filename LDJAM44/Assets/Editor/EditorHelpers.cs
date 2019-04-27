using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorHelpers : MonoBehaviour
{
    [MenuItem("CONTEXT/BoxCollider2D/Adjust")]
    static void AdjustCollider(MenuCommand command)
    {
        var coll = command.context as BoxCollider2D;
        if (coll)
        {
            var mesh = coll.transform.GetChild(0);
            if (mesh)
            {
                coll.size = mesh.localScale;
                coll.offset = mesh.localPosition;
            }
         
        }
    }

    [MenuItem("CONTEXT/Humanoid/Save Pose")]
    static void SaveHumanoidPose(MenuCommand command)
    {
        var humanoid = command.context as Humanoid;
        if (humanoid)
        {
            var pose = humanoid.SavePose();
            var path = EditorUtility.SaveFilePanelInProject("Save pose", "New Pose", "asset", "Save Pose", "Assets/Configs/Poses/");
            if(string.IsNullOrEmpty(path))
            {
                return;
            }
            AssetDatabase.CreateAsset(pose, path);
            AssetDatabase.Refresh();
        }
    }

}
