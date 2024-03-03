using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public VerletState state;
    public float mass;
    public bool isFixed;
}

public class Constraint
{
    public int node1;
    public int node2;
    public float compensate1;
    public float compensate2;
    public float desiredDistance;
}

public class RopeManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public List<Constraint> constraints = new List<Constraint>();

    private int AddNode(Vector2 pos, float mass, bool isFixed)
    {
        //int corner1 = AddNode(new Vector2(0, 0), 1, false);
        //int corner2 = AddNode(new Vector2(1, 0), 1, false);
        //int corner3 = AddNode(new Vector2(1, 1), 1, false);
        //int corner4 = AddNode(new Vector2(0, 1), 1, false);

        //AddConstraint(corner1, corner2, 1.0f);
        //AddConstraint(corner2, corner3, 1.0f);
        //AddConstraint(corner3, corner4, 1.0f);
        //AddConstraint(corner4, corner1, 1.0f);

        //return corner1;

        int newNode = AddNode(new Vector2(0, 0), 1, false);

        return newNode;
    }

    private void AddConstraint(int node1, int node2, float desired, float compensate1 = 0.5f, float compensate2 = 0.5f)
    {

    }

    private void FixedUpdate()
    {
        foreach (Node node in nodes)
        {
            Vector2 gravity = new Vector2(0, -9.81f);

            node.state.addForce(gravity);
            node.state.integrate();
        }

        for (int i = 0; i < 10; i++)
        {
            
        }
    }
}
