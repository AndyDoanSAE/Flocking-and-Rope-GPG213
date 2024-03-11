using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Minion : MonoBehaviour 
{
	protected Vector3 velocity = Vector3.zero;
	protected List<Minion> minions;

	protected MinionConfig Config;
	
	public virtual Vector3 Velocity 
	{
		get 
		{
			return velocity;
		}
		set 
		{
			velocity = value;
		}
	}

	// Use this for initialization
	protected virtual void Start () 
	{
		// retreive all minions
		minions = new List<Minion>(FindObjectsOfType<Minion>());	

		// retrieve the minion config
		Config = FindObjectOfType<MinionConfig>();
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		// Calculate and apply the flocking vector
		velocity = (velocity + Flocking_Update()).normalized * Config.Speed;

		// apply the velocity
		transform.position += velocity * Time.deltaTime;
	}

	protected Vector3 Flocking_Update() 
	{
		// Calculate each of the vectors
		Vector3 cohesionVector = Flocking_CalculateCohesion(minions) * Config.CohesionStrength;
		Vector3 separationVector = Flocking_CalculateSeparation(minions) * Config.SeparationStrength;
		Vector3 alignmentVector = Flocking_CalculateAlignment(minions) * Config.AlignmentStrength;

		Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + cohesionVector, Color.red);
		Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + separationVector, Color.green);
		Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + alignmentVector, Color.blue);

		// Calculate the flocking vector
		Vector3 flockingVector = (cohesionVector + separationVector + alignmentVector) * Config.Speed;

		return flockingVector;
	}

	Vector3 Flocking_CalculateCohesion(List<Minion> flock) 
	{
		// My location = transform.position
		// Entire flock - flock

		Vector3 cohesion = Vector3.zero;
		Vector3 cohesionOfMinions = Vector3.zero;

		// count of minion = average of minion positions
		// cohesion = position of other minion - position of self

		for (int i = 0; i < flock.Count; i++)
		{
			var minionVar = flock[i];
			var minionPosition = minionVar.transform.position;

			cohesionOfMinions += minionPosition;
		}
		cohesionOfMinions /= flock.Count;

		cohesion = cohesionOfMinions - this.transform.position;

		return cohesion.normalized;
	}

	Vector3 Flocking_CalculateSeparation(List<Minion> flock)
	{
		// My location = this.transform.position
		// Entire flock - flock

		Vector3 separation = Vector3.zero;		// The separation vector
		Vector3 othersToSelf = Vector3.zero;	// The vector of other minions

		// separation = average of other vectors to self

		for (int i = 0; i < flock.Count; i++)
		{
			var minionVar = flock[i];
			if (minionVar == this)
            {
				continue; // If the minion in the flock is this minion, skip it
            }
			else
            {
				var minionPosition = minionVar.transform.position;
				othersToSelf += this.transform.position - minionPosition;
			}
		}

		separation = othersToSelf / (flock.Count - 1);

		return separation.normalized;
	}

	Vector3 Flocking_CalculateAlignment(List<Minion> flock) 
	{
		// My location = transform.position
		// My velocity = this.Velocity
		// Entire flock - flock

		Vector3 alignment = Vector3.zero;

        // alignment = average of velocity vector for each memeber
        //			 = (add the velocity vectors of all of the flock) / number of velocity vectors
        // number of velocity vectors we added = number of minions in flock = flock.Count

        for (int i = 0; i < flock.Count; i++)
        {
			var minionVar = flock[i];
			var minionVelocity = minionVar.Velocity;

			alignment += minionVelocity;
        }

		alignment /= flock.Count;

		return alignment.normalized;
	}
}
