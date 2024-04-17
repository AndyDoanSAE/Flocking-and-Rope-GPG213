using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boid : MonoBehaviour
{
    // Basic properties of a Boid, used for Euler Integration
    public Vector2 vel;
    public Vector2 pos;
    public Vector2 force;

    float seperationStrength;
    float cohesionStrength;
    float alignmentStrength;

    [SerializeField] bool enableAlignment;
    [SerializeField] bool enableCohesion;
    [SerializeField] bool enableSeperation;



    // Accumulate force
    // Every update the BoidManager uses the Boid's force then wipes it to zero
    public void AddForce(Vector2 f)
    {
        force += f;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Store the Boid's transform position into it's pos
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Find all nearby Boids
        var nearby = BoidManager.instance.FindBoidsInRange(this, pos, BoidManager.instance.boidSightRange);
        var boidsToAvoid = BoidManager.instance.FindBoidsToAvoid(this, pos, BoidManager.instance.boidAvoidanceRange);

        
        seperationStrength = BoidManager.instance.boidStrengthSeparation;
        cohesionStrength = BoidManager.instance.boidStrengthCohesion;
        alignmentStrength = BoidManager.instance.boidStrengthAlignment;

        enableAlignment = BoidManager.instance.boidEnableAlignment;
        enableCohesion = BoidManager.instance.boidEnableCohesion;
        enableSeperation = BoidManager.instance.boidEnableSeparation;

        // If there are nearby Boids
        if (nearby.Count > 0)
        {
            // Do flocking processing here

            //Alignment
            if (enableAlignment)
            {
                //Add all points together and average
                Vector2 alignmentMove = Vector2.zero;
                foreach (var b in nearby)
                {
                    alignmentMove += (Vector2)b.transform.position;
                }

                alignmentMove /= nearby.Count;

                //Create steering by minusing current Vector
                alignmentMove -= this.vel;

                //Add alignment force to boid
                AddForce(alignmentMove);
            }

            //Cohesion
            if (enableCohesion)
            {
                
                //Add all points together and average
                Vector2 cohesionMove = Vector2.zero;
                foreach (var b in nearby)
                {
                    cohesionMove += (Vector2)b.transform.position;
                }
                cohesionMove /= nearby.Count;

                //Create steering by minusing current position
                cohesionMove -= this.pos;
                cohesionMove -= this.vel;

                AddForce(cohesionMove);
                
            }

            
            //Seperation
            if (enableSeperation)
            {
                /*
                Vector2 difference = Vector2.zero;
                float distance;
                Vector2 seperationMove = Vector2.zero;
                foreach (var b in boidsToAvoid)
                {
                    distance = Vector2.Distance(b.transform.position, this.pos);
                    difference = (this.pos - (Vector2)b.transform.position);
                    difference /= distance;
                    seperationMove += difference;

                }
                seperationMove /= boidsToAvoid.Count;

                AddForce(seperationMove);
                */
            }
            



        }

    }

}
