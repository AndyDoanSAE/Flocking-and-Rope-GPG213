using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public VerletState state;

    public Node(Vector2 startPos)
    {
        this.state.pos = startPos;
        this.state.prevPos = startPos;
    }
}

//public class Constraint
//{
//    public Node nodeOne;
//    public Node nodeTwo
//    {
//        get
//        {
//            return nodeTwo;
//        }
//        set
//        {
//            if (nodeTwo == null)
//            {
//                nodeOne = value;
//            }
//        }
//    }
//    public float distance;
//}

public class RopeManager : MonoBehaviour
{
    [SerializeField] private Node[] nodes;

    //[SerializeField] private Constraint constraint;

    [SerializeField] private int cycles;
    [SerializeField] private int numOfNodes;
    [SerializeField] private float spacing;
    [SerializeField] private float gravity;

    [SerializeField] private bool fixedOrigin;

    private LineRenderer lineRenderer;

    private void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        nodes = new Node[numOfNodes];

        Vector2 pos = transform.position;

        for (int i = 0; i < numOfNodes; i++)
        {
            nodes[i] = new Node(pos);

            pos.y -= spacing;
        }

        lineRenderer.positionCount = nodes.Length;
    }

    private void FixedUpdate()
    {
        Simulate();

        for (int i = 0; i < cycles; i++)
        {
            ApplyConstraints();
        }
    }

    private void Simulate()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            Node currentNode = nodes[i];

            currentNode.state.addForce(gravity * Vector2.down);
            currentNode.state.integrate();

            lineRenderer.SetPosition(i, currentNode.state.pos);
        }
    }

    private void ApplyConstraints()
    {
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            Node nodeOne = nodes[i];
            Node nodeTwo = nodes[i + 1];

            if (i == 0 && Input.GetMouseButton(0))
                nodeOne.state.pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float nodeDistance = Vector2.Distance(nodeOne.state.pos, nodeTwo.state.pos);
            float diffX = nodeOne.state.pos.x - nodeTwo.state.pos.x;
            float diffY = nodeOne.state.pos.y - nodeTwo.state.pos.y;
            float difference = 0;

            if (nodeDistance > 0)
                difference = (spacing - nodeDistance) / nodeDistance;

            Vector2 translate = new Vector2(diffX, diffY) * difference / 2;

            nodeOne.state.pos += translate;
            nodeTwo.state.pos -= translate;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        for (int i = 0; i < nodes.Length - 1; i++)
        {
            if (i % 2 == 0)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.white;
            }

            Gizmos.DrawLine(nodes[i].state.pos, nodes[i + 1].state.pos);
        }
    }
}


