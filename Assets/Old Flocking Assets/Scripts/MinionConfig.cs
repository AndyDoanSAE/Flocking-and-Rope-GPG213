using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionConfig : MonoBehaviour
{
	public float Speed = 5f;

    [Range(0f, 1f)]
	public float CohesionStrength = 0.3f;

    [Range(0f, 1f)]
	public float SeparationStrength = 0.4f;

    [Range(0f, 1f)]
	public float AlignmentStrength = 0.3f;

    void Update()
    {
        // normalise the strengths if they need to be
        float strengthSum = CohesionStrength + SeparationStrength + AlignmentStrength;

        // normalise the values if needed
        if (Mathf.Abs(strengthSum - 1f) > float.Epsilon)
        {
            CohesionStrength /= strengthSum;
            SeparationStrength /= strengthSum;
            AlignmentStrength /= strengthSum;
        }
    }
}
