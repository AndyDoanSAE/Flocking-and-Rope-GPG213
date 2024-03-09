using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public VerletState state;
}

[System.Serializable]
public class Constraint
{
    public Node firstNode, secondNode;
    public float length, spacing;
}

public class RopeManager : MonoBehaviour
{
    [SerializeField] private GameObject nodeObj;

    [SerializeField] private Transform startNode;
    [SerializeField] private Transform endNode;

    [SerializeField] private int repetition;
    [SerializeField] private int numOfNodes;
    [SerializeField] private float gravity;
    [SerializeField] private float length;
    [SerializeField] private float nodeSpacing;

    [SerializeField] private bool fixedStartNode;
    [SerializeField] private bool fixedEndNode;

    [SerializeField] private Node[] nodes;
    //[SerializeField] private Constraint[] constraints;

    private void Start()
    {
        nodes = new Node[numOfNodes];
        //constraints = new Constraint[numOfNodes - 1];

        for (int i = 0; i < numOfNodes - 1; i++)
        {
            Instantiate(nodeObj);
        }

        //for (int i = 0; i < numOfNodes - 1; i++)
        //{
        //    length += Vector2.Distance(nodes[i].state.pos, nodes[i + 1].state.pos);
        //}

        //nodeSpacing = length / nodes.Length;
    }

    private void LateUpdate()
    {
        nodeObj.transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        //nodes[0].state.pos = startNode.position;

        for (int i = 0; i < nodes.Length; i++)
        {
            bool fixedNode = (i == 0 && fixedStartNode) || (i == nodes.Length - 1 && fixedEndNode);

            if (!fixedNode)
            {
                nodes[i].state.addForce(gravity * Vector2.down);
                nodes[i].state.integrate();
            }
        }

        for (int i = 0; i < repetition; i++)
        {
            ConstraintConfig();
        }
    }

    private void ConstraintConfig()
    {
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            Vector2 center = (nodes[i].state.pos + nodes[i + 1].state.pos) / 2;
            Vector2 offset = nodes[i].state.pos - nodes[i + 1].state.pos;
            float length = offset.magnitude;
            Vector2 dir = offset / length;

            if (i != 0 || !fixedStartNode)
                nodes[i].state.pos = center + dir * nodeSpacing / 2;

            if (i + 1 != nodes.Length - 1 || !fixedEndNode)
                nodes[i + 1].state.pos = center - dir * nodeSpacing / 2;
        }
    }
}

//    public List<Node> nodes = new List<Node>();
//    public List<Constraint> constraints = new List<Constraint>();


//    private void Start()
//    {
//        if (nodes == null)
//            nodes = new List<Node>();

//        if (constraints == null)
//            constraints = new List<Constraint>();
//    }

//    private void Update()
//    {
//        Simulate();
//    }

//    private int AddNode(Vector2 pos, float mass, bool isFixed)
//    {
//        //int corner1 = AddNode(new Vector2(0, 0), 1, false);
//        //int corner2 = AddNode(new Vector2(1, 0), 1, false);
//        //int corner3 = AddNode(new Vector2(1, 1), 1, false);
//        //int corner4 = AddNode(new Vector2(0, 1), 1, false);

//        //AddConstraint(corner1, corner2, 1.0f);
//        //AddConstraint(corner2, corner3, 1.0f);
//        //AddConstraint(corner3, corner4, 1.0f);
//        //AddConstraint(corner4, corner1, 1.0f);

//        //return corner1;

//        int newNode = AddNode(new Vector2(0, 0), 1, false);

//        return newNode;
//    }

//    private void AddConstraint(int node1, int node2, float desired, float compensate1 = 0.5f, float compensate2 = 0.5f)
//    {

//    }

//    private void FixedUpdate()
//    {

//    }

//    private void Simulate()
//    {
//        foreach (Node node in nodes)
//        {
//            if (!node.isFixed)
//            {
//                Vector2 posBeforeUpdate = node.pos;

//                node.pos += node.pos - node.prevPos;
//                node.pos += Vector2.down * 9.81f * Time.deltaTime * Time.deltaTime;
//                node.prevPos = posBeforeUpdate;
//            }
//        }

//        for (int i = 0; i < 10; i++)
//        {
//            foreach (Constraint constraint in constraints)
//            {
//                Vector2 constraintCenter = (constraint.firstNode.pos + constraint.secondNode.pos) / 2;
//                Vector2 constraintDir = (constraint.firstNode.pos + constraint.secondNode.pos).normalized;

//                if (!constraint.firstNode.isFixed)
//                    constraint.firstNode.pos = constraintCenter + constraintDir * constraint.spacing / 2;

//                if (!constraint.secondNode.isFixed)
//                    constraint.secondNode.pos = constraintCenter + constraintDir * constraint.spacing / 2;
//            }
//        }
//    }

