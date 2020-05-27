using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public UIFunctionality ui_Functionality;

    // basic assets like cubes and plane
    public GameObject planePrefab;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject entitiePrefab;

    public Camera camera;

    private List<GameObject> entities = new List<GameObject>();

    public GameObject target { get; set; }

    public int width { get; set; }
    public int height { get; set;}

    private const string GROUND_NAME = "ground";
    private const int HEIGHT_ABOVE_GROUND = 5;

    public Graph graph;

    // variables for setting base
    public GameObject basePrefab;
    GameObject baseBuilding;

    // variables for setting trees
    public GameObject treePrefab;
    private int amountTrees = 30;
    private List<GameObject> listOfTrees = new List<GameObject>();
    private const string NAME_LIST_OF_TREES = "listOfTrees";

    // variables for setting stones
    public GameObject stonePrefab;
    private int amountStones = 20;
    private List<GameObject> listOfStone = new List<GameObject>();
    private const string NAME_LIST_OF_STONE = "listOfStone";

    // variables for levelgenerator
    public Material materialGround;
    public Material materialGroundLight;

    private float xPosition = 0;
    private float zPosition = 0;
    private float yPosition = 0;

    private float widthGroundBlock;
    private float heighGroundBlock;

    private float noiseConditionGround;

    private float perlinNumberX;
    private float perlinNumberZ;

    private List<GameObject> GroundList = new List<GameObject>();

    private int amountCubesX = 25;
    private int amountCubesY = 25;

    // variables needed for GoaldrivenBehaviour
    public int amountWood { get; set; }
    public int amountStone { get; set; }
    private int amountAgents = 4;


    public void Start()
    {
        // create level
        CreateFloor();

        // create base
        CreateBase();

        // create trees
        createTrees();

        // create stone
        CreateStone();

        // CreateGraph(); // TODO: fix noded.getlength(0)

        Populate();

        // Check ui to set colours correctly
        ui_Functionality.ToggleColours();
    }

    private void CreateFloor()
    {
        var renderer = cubePrefab.GetComponent<Renderer>();

        // get height : only need one side since it is a cube
        widthGroundBlock = renderer.bounds.size.x;

        noiseConditionGround = RandomNumber(0.20f, 0.24f);

        //  The x coordinate
        for (int x = 0; x < amountCubesX; x++)
        {
            zPosition = 0;
            //  the Z coordinate
            for (int z = 0; z < amountCubesY; z++)
            {
                xPosition = widthGroundBlock * x;
                zPosition = widthGroundBlock * z;

                perlinNumberX = RandomNumber(4, 15);
                perlinNumberZ = RandomNumber(4, 15);

                float noise = Mathf.PerlinNoise(x / perlinNumberX, z / perlinNumberZ);

                if (noise > noiseConditionGround)
                {
                    GameObject _floorCube;

                    _floorCube = Instantiate(cubePrefab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity, this.transform);
                   
                    // change the name so it is easier to find in the hierachy
                    _floorCube.name = GROUND_NAME;

                    //  randomly change the material of the block
                    int randomNumber = (int)RandomNumber(0, 100);

                    if (randomNumber % 2 == 0)
                    {
                        _floorCube.GetComponent<MeshRenderer>().material = materialGround;
                    }
                    if (randomNumber % 4 == 0)
                    {
                        _floorCube.GetComponent<MeshRenderer>().material = materialGroundLight;
                    }
                    else
                    {
                        _floorCube.GetComponent<MeshRenderer>().material = materialGround;
                    }

                    GroundList.Add(_floorCube);
                }
            }
            xPosition += 1;
        }
    }

    private void CreateWorldObjects(int amount, string listName, GameObject prefab)
    {
        for (int i = 0; i < amount; i++)
        {
            // select a groundCube
            GameObject tempGroundBlock = GroundList[(int)RandomNumber(0, GroundList.Count)];

            // remove that spot from the list
            GroundList.Remove(tempGroundBlock);

            // instantiate tree
            GameObject worldObject = Instantiate(
                prefab,
                new Vector3(tempGroundBlock.transform.position.x,
                            tempGroundBlock.transform.position.y + HEIGHT_ABOVE_GROUND,
                            tempGroundBlock.transform.position.z),
                Quaternion.identity,
                this.transform  // to make the object a child of the gameObject which this script is attached to so it doesn't clutter the editor
            );

            // add to the list
            if (listName == NAME_LIST_OF_STONE)
            {
                listOfStone.Add(worldObject);
            }
            else if(listName == NAME_LIST_OF_TREES)
            {
                listOfTrees.Add(worldObject);
            }

        }
    }

    private void CreateStone()
    {
        CreateWorldObjects(amountStones, NAME_LIST_OF_STONE, stonePrefab);
    }

    private void createTrees()
    {
        CreateWorldObjects(amountTrees, NAME_LIST_OF_TREES, treePrefab);
    }


    private void CreateBase()
    {
        // get a random spot from the list
        GameObject tempGroundBlock = GroundList[ (int)RandomNumber(0, GroundList.Count) ];

        // remove that spot from the list
        GroundList.Remove(tempGroundBlock);

        // instantiate building
        baseBuilding = Instantiate(
            basePrefab,
            new Vector3(tempGroundBlock.transform.position.x,
                        tempGroundBlock.transform.position.y + HEIGHT_ABOVE_GROUND,
                        tempGroundBlock.transform.position.z),
            Quaternion.identity
        );
    }

    private void Populate()
    {
        // target: empty cube (for now)
        target = Instantiate(cubePrefab, new Vector3(20, HEIGHT_ABOVE_GROUND, 0), Quaternion.identity);
        target.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // downsize the cube with half

        // create enitites
        for (int i = 0; i < amountAgents; i++)
        {
            // get a random spot from the list
            GameObject tempGroundBlock = GroundList[(int)RandomNumber(0, GroundList.Count)];

            // remove that spot from the list
            GroundList.Remove(tempGroundBlock);

            // create entitiy and give it a random value 
            GameObject tempObject = Instantiate(entitiePrefab,
                new Vector3(tempGroundBlock.transform.position.x,
                        tempGroundBlock.transform.position.y + HEIGHT_ABOVE_GROUND,
                        tempGroundBlock.transform.position.z),
                Quaternion.identity
                );

            // give behaviour
            tempObject.GetComponent<MovingEntity>().SB = new GoalDrivenBehaviour(
                tempObject
                );

            // give variables
            tempObject.GetComponent<MovingEntity>().SetVariables(this); // sent reference to this script: world

            // set id
            tempObject.GetComponent<MovingEntity>().GetComponent<MovingEntity>().id = i + 1; // plus one so there is no id = 0

            // add the entitie to the list of entities
            entities.Add(tempObject);
        }

    }

    public void Update()
    {

        // Mouse input
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }

        // update moving entities
        foreach (GameObject entitie in entities)
        {
            entitie.GetComponent<MovingEntity>().UpdateEntity();
        }
        //DrawGraph();

        //update UI text
        ui_Functionality.SetResourcesText(amountWood, amountStone, GroundList.Count);
    }

    /// <summary>
    /// OnClick is called when the left mouse button is pressed
    /// OnClick checks the surface which it hits and it it
    /// was ground than the cursor is placed there
    /// </summary>
    public void OnClick()
    {
        // raycast from the middle of the camera
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // check if it hitted ground
            if (hit.transform.gameObject.name == GROUND_NAME)
            {
                // place target
                target.transform.position = new Vector3(hit.point.x, HEIGHT_ABOVE_GROUND, hit.point.z);
            }
        }
    }

    public void CreateBuilding(Vector3 location)
    {
        Instantiate(basePrefab,
            new Vector3(location.x, location.y + HEIGHT_ABOVE_GROUND, location.z),
            Quaternion.identity
        );
    }

    public void CreateGraph()
    {
        var nodes = Graph.NodeList;
        double size = 50;

        int i, j; //ints for looping

        //ints for positions
        double x = size / 2;
        double y = size / 2;

        //create all the nodes
        for (i = 0; i < nodes.GetLength(0); i++)
        {
            for (j = 0; j < nodes.GetLength(1); j++)
            {
                nodes[i, j] = new Node(new Vector2( (float)x, (float)y) );
                x += size;//move x to the right
            }
            y += size; //move y down
            x = size / 2; //reset x to the left side
        }


        for (i = 0; i < nodes.GetLength(0); i++)
        {
            for (j = 0; j < nodes.GetLength(1); j++)
            {
                //check 4 adjacent nodes for possible edges
                //north-east corner
                if (_exists(i - 1, j + 1))
                {
                    nodes[i, j].AddEdge(nodes[i - 1, j + 1], Math.Sqrt(size * size * 2)); //add edge to edge list of node
                    nodes[i - 1, j + 1].AddEdge(nodes[i, j], Math.Sqrt(size * size * 2)); //backwards edge for the destination node
                }
                //east side
                if (_exists(i, j + 1))
                {
                    nodes[i, j].AddEdge(nodes[i, j + 1], size);
                    nodes[i, j + 1].AddEdge(nodes[i, j], size);
                }
                //south east corner
                if (_exists(i + 1, j + 1))
                {
                    nodes[i, j].AddEdge(nodes[i + 1, j + 1], Math.Sqrt(size * size * 2));
                    nodes[i + 1, j + 1].AddEdge(nodes[i, j], Math.Sqrt(size * size * 2));
                }
                //south side
                if (_exists(i + 1, j))
                {
                    nodes[i, j].AddEdge(nodes[i + 1, j], size);
                    nodes[i + 1, j].AddEdge(nodes[i, j], size);
                }
            }
        }
    }

    public void DrawGraph()
    {
        foreach (var node in Graph.NodeList)
        {
            foreach (var adjacentNode in node.Adjacents)
            {
                if (!node.Scratch)
                {
                    Vector3 start = new Vector3(node.Position.x, 1, node.Position.y);
                    Vector3 end = new Vector3(adjacentNode.Destination.Position.x, 1, adjacentNode.Destination.Position.y);
                    Debug.DrawLine(start, end, Color.yellow);
                    //g.DrawLine(
                    //    pen,
                    //    (int)node.Position.x,
                    //    (int)node.Position.y,
                    //    (int)adjacentNode.Destination.Position.x,
                    //    (int)adjacentNode.Destination.Position.y
                    //);
                }
                else
                {
                    Vector3 start = new Vector3(node.Position.x, 1, node.Position.y);
                    Vector3 end = new Vector3(adjacentNode.Destination.Position.x, 1, adjacentNode.Destination.Position.y);
                    Debug.DrawLine(start, end, Color.black);
                    //g.DrawLine(
                    //    penScr,
                    //    (int)node.Position.x,
                    //    (int)node.Position.y,
                    //    (int)adjacentNode.Destination.Position.x,
                    //    (int)adjacentNode.Destination.Position.y
                    //);
                }

            }
        }

    }

    public static float RandomNumber(float lowest, float highest)
    {
        float randomNumber = UnityEngine.Random.Range(lowest, highest);
        return randomNumber;
    }

    public Vector3 GetBaseLocation()
    {
        return baseBuilding.transform.position;
    }

    public List<GameObject> GetListOfTrees()
    {
        return listOfTrees;
    }

    public List<GameObject> GetListOfStones()
    {
        return listOfStone;
    }

    public List<GameObject> GetListOfLocations()
    {
        return GroundList;
    }

    public List<GameObject> GetListofEntities()
    {
        return entities;
    }

    private bool _exists(int i, int j)
    {
        if (i >= 0 && i < Graph.NodeList.GetLength(0) && j >= 0 && j < Graph.NodeList.GetLength(1))
            return true;
        else
            return false;
    }

}   // class

