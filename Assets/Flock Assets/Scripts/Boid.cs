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
    
    // Accumulate force
    // Every update the BoidManager uses the Boid's force then wipes it to zero
    private void AddForce(Vector2 f)
    {
        force += f;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Store the Boid's transform position into it's pos
        pos = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Find all nearby Boids
        var nearby = BoidManager.instance.FindBoidsInRange(this, pos, BoidManager.instance.boidSightRange);

        Vector2 steering = Vector2.zero;
        
        float speed = BoidManager.instance.boidSpeed;
        
        float separationStrength = BoidManager.instance.boidStrengthSeparation;
        float cohesionStrength = BoidManager.instance.boidStrengthCohesion;
        float alignmentStrength = BoidManager.instance.boidStrengthAlignment;
        
        bool enableAlignment = BoidManager.instance.boidEnableAlignment;
        bool enableCohesion = BoidManager.instance.boidEnableCohesion;
        bool enableSeparation = BoidManager.instance.boidEnableSeparation;

        // If there are nearby Boids
        if (nearby.Count > 0)
        {
            // Do flocking processing here

            // Alignment
            if (enableAlignment)
            {
                // Find the average velocity of nearby Boids
                foreach (var b in nearby)
                {
                    steering += b.vel;
                }
                
                steering /= nearby.Count;                                           // Average velocity
                steering = steering.normalized * speed;                             // Normalized average velocity
                steering -= this.vel;                                               // Steering vector
                steering = Vector2.ClampMagnitude(steering, alignmentStrength);     // Limit to maximum steering force
                
                AddForce(steering);                                                 // Apply steering force
            }

            // Cohesion
            if (enableCohesion)
            {
                // Find the average position of nearby Boids
                foreach (var b in nearby)
                {
                    steering += b.pos;
                }
                
                steering /= nearby.Count;                                           // Average position
                steering -= this.pos;                                               // Steering vector
                steering = steering.normalized * speed;                             // Normalized average position
                steering -= this.vel;                                               // Steering vector
                steering = Vector2.ClampMagnitude(steering, cohesionStrength);      // Limit to maximum steering force
                
                AddForce(steering);                                                 // Apply steering force
            }

            
            // Separation
            if (enableSeparation)
            {
                // Find the average distance between this Boid and nearby Boids
                foreach (var b in nearby)
                {
                    Vector2 difference = this.pos - b.pos;
                    difference /= Vector2.Distance(this.pos, b.pos);
                    steering += difference;
                }
                
                steering /= nearby.Count;                                           // Average distance
                steering = steering.normalized * speed;                             // Normalized average distance
                steering -= this.vel;                                               // Steering vector
                steering = Vector2.ClampMagnitude(steering, separationStrength);    // Limit to maximum steering force
                
                AddForce(steering);                                                 // Apply steering force
            }
        }
    }
}
