using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
	public NodeScript Previous;
	public NodeScript Next;

	protected List<BaseJoint> joints = new List<BaseJoint>();
	protected JointConfig Config;

	// BEGIN

	protected Vector3 newPos;
	public Vector3 oldPos;
	public Vector3 force;
	public float mass;

	// END

	public Vector3 CurrentPosition
	{
		get
		{
			return transform.position;
		}
		set
		{
			if (Previous != null)
				transform.position = value;
		}
	}

	private void Start()
	{
		Config = FindObjectOfType<JointConfig>();

		// BEGIN
		// set our previous position to current position
		// reset of the forces

		oldPos = CurrentPosition;
		force = Config.Gravity * Vector3.down;

		// END

		// does this node have no previous? if so then it is the root node so create the joints
		if (Previous == null)
		{
			CreateJoints();
		}
	}

	private void FixedUpdate()
	{
		// if this is the root node then tell the joints to update
		foreach (BaseJoint joint in joints)
		{
			joint.Update();
		}
	}

	private void LateUpdate()
	{
		// BEGIN
		// Calculating new pos
		// Update old pos
		// Applying new pos (ie. updating current pos)
		// reset the forces

		var currentVerlet = CurrentPosition - oldPos;
		var acceleration = force / mass;

		newPos = CurrentPosition + currentVerlet + acceleration * Config.TimeStep * Mathf.Pow(Config.TimeStep, 2);
		oldPos = CurrentPosition;
		CurrentPosition = newPos;
		force = Config.Gravity * Vector3.down;

		// END
	}

	private void CreateJoints()
	{
		// traverse the chain of nodes
		NodeScript current = this;
		while (current != null)
		{
			// if there is a next node then create the joint
			if (current.Next != null)
			{
				joints.Add(new BaseJoint(current, current.Next, Config));
			}

			current = current.Next;
		}
	}
}
