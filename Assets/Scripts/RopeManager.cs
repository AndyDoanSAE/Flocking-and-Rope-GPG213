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

public class RopeManager : MonoBehaviour
{
    [SerializeField] private Node[] _nodes;

    [SerializeField] private int cycles;
    [SerializeField] private int numOfNodes;
    [SerializeField] private float spacing;
    [SerializeField] private float gravity;

    [SerializeField] private bool fixedOrigin;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        // Get the line renderer component
        if (_lineRenderer == null)
            _lineRenderer = GetComponent<LineRenderer>();

        // Create the nodes
        _nodes = new Node[numOfNodes];
        
        // Set the line renderer's position
        Vector2 pos = transform.position;
        for (int i = 0; i < numOfNodes; i++)
        {
            _nodes[i] = new Node(pos);

            pos.y -= spacing;
        }

        _lineRenderer.positionCount = _nodes.Length;
    }

    private void FixedUpdate()
    {
        Simulate();
        
        for (int i = 0; i < cycles; i++)
        {
            ApplyConstraints();
        }
    }

    // Simulate the rope
    private void Simulate()
    {
        // add force to each node
        for (int i = 0; i < _nodes.Length; i++)
        {
            Node currentNode = _nodes[i];

            currentNode.state.addForce(gravity * Vector2.down);
            currentNode.state.integrate();

            _lineRenderer.SetPosition(i, currentNode.state.pos);
        }
    }

    // Apply constraints to the rope
    private void ApplyConstraints()
    {
        
        for (int i = 0; i < _nodes.Length - 1; i++)
        {
            Node nodeOne = _nodes[i];
            Node nodeTwo = _nodes[i + 1];

            // if this is the first node and the mouse is pressed
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

        for (int i = 0; i < _nodes.Length - 1; i++)
        {
            if (i % 2 == 0)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.white;
            }

            Gizmos.DrawLine(_nodes[i].state.pos, _nodes[i + 1].state.pos);
        }
    }
}


