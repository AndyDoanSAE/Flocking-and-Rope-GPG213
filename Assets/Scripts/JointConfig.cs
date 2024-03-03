using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointConfig : MonoBehaviour
{
    public float SpringConstant = 50f;
    public float Dampening = 50f;

    public float TimeStep = 1f / 30f;
    public float Gravity = 9.81f;
}
