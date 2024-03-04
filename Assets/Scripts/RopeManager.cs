using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2 pos, prevPos;
    public bool isFixed;
}

[System.Serializable]
public class Constraint
{
    public Node firstNode, secondNode;
    public float compensateOne, compensateTwo, spacing;
}

public class RopeManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public List<Constraint> constraints = new List<Constraint>();


    private void Start()
    {
        if (nodes == null)
            nodes = new List<Node>();

        if (constraints == null)
            constraints = new List<Constraint>();
    }

    private void Update()
    {
        Simulate();
    }

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

    }

    private void Simulate()
    {
        foreach (Node node in nodes)
        {
            if (!node.isFixed)
            {
                Vector2 posBeforeUpdate = node.pos;

                node.pos += node.pos - node.prevPos;
                node.pos += Vector2.down * 9.81f * Time.deltaTime * Time.deltaTime;
                node.prevPos = posBeforeUpdate;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            foreach (Constraint constraint in constraints)
            {
                Vector2 constraintCenter = (constraint.firstNode.pos + constraint.secondNode.pos) / 2;
                Vector2 constraintDir = (constraint.firstNode.pos + constraint.secondNode.pos).normalized;

                if (!constraint.firstNode.isFixed)
                    constraint.firstNode.pos = constraintCenter + constraintDir * constraint.spacing / 2;

                if (!constraint.secondNode.isFixed)
                    constraint.secondNode.pos = constraintCenter + constraintDir * constraint.spacing / 2;
            }
        }
    }
}
