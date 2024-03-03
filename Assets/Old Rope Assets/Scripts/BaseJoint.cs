using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseJoint
{
	public NodeScript Node1;
	public NodeScript Node2;
	protected JointConfig Config;

	// BEGIN

	protected float initialDistance;
	protected float currentDistance;
	protected float forceMagnitude;
	protected Vector3 force;

	// END

	public BaseJoint(NodeScript _node1, NodeScript _node2, JointConfig _config)
	{
		Node1 = _node1;
		Node2 = _node2;
		Config = _config;

		// BEGIN
		// Storing our initial distance

		initialDistance = Vector3.Distance(_node1.CurrentPosition, _node2.CurrentPosition);

		// END
	}

	public virtual void Update()
	{
		// BEGIN
		// Calculate current distance
		// Calculate force
		// Add to the force on Node1 and Node2

		currentDistance = Vector3.Distance(Node1.CurrentPosition, Node2.CurrentPosition);
		forceMagnitude = -Config.SpringConstant * (currentDistance - initialDistance);
		force = (Node2.CurrentPosition - Node1.CurrentPosition).normalized * forceMagnitude;
		force -= ((Node2.CurrentPosition - Node2.oldPos) - (Node1.CurrentPosition - Node1.oldPos)) * Config.Dampening;
		Node2.force += force;
		Node1.force += -force;

		// END
	}
}
