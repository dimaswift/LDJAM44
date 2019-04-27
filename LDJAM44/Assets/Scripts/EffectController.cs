using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public Effect BloodEffect;

    public static EffectController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

   
}
