using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Used to generate the actual maze using Kruskal's algorithm
 */ 
public class MazeGen : MonoBehaviour
{
	public int mazeSizeX;
	public int mazeSizeZ;
	private int mazeSizeY;
	private Dictionary<Vector3, Node> nodes;
	private List<Edge> edges;

	// Use this for initialization
	void Start ()
	{
		this.MazeSizeY = Constants.MAZE_HEIGHT;
		this.Nodes = new Dictionary<Vector3, Node> ();
		this.Edges = new List<Edge> ();

		this.InitNodesAndEdges ();
		this.Edges.Sort (new EdgeComp ());
		this.CreateMaze ();
		this.UpdateNodes ();
	}

	// Create all of the nodes and edges in the maze
	public void InitNodesAndEdges ()
	{
		// For each Node that should be generated
		for (int x = 0; x < this.MazeSizeX; x++) {
			for (int y = 0; y < this.MazeSizeY; y++) {
				for (int z = 0; z < this.MazeSizeZ; z++) {
					// Load the Node resource
					GameObject go = Instantiate (Resources.Load ("Node")) as GameObject;
					// Set the Node's position and assign a random color
					Vector3 coordinate = new Vector3 (x, y, z);
					Node n = new Node (coordinate, go, RandomColor.GetColor ());
					// Add the node to the dictionary
					this.Nodes.Add (coordinate, n);

					// Create edges on the x axis
					if (x > 0) {
						Node left = this.Nodes [new Vector3 (n.Coordinate.x - 1, n.Coordinate.y, n.Coordinate.z)];
						Edge e = new Edge (n, left, Random.Range (0, 10));
						this.Edges.Add (e);
					}

					// Create edges on the y axis
					if (y > 0) {
						Node down = this.Nodes [new Vector3 (n.Coordinate.x, n.Coordinate.y - 1, n.Coordinate.z)];
						Edge e = new Edge (n, down, Random.Range (0, 10));
						this.Edges.Add (e);
					}

					// Create edges on the z axis
					if (z > 0) {
						Node back = this.Nodes [new Vector3 (n.Coordinate.x, n.Coordinate.y, n.Coordinate.z - 1)];
						Edge e = new Edge (n, back, Random.Range (0, 10));
						this.Edges.Add (e);
					}

					// Create an exit in the top Node
					if (x == this.mazeSizeX - 1 && y == this.mazeSizeY - 1 && z == this.mazeSizeZ - 1) {
						n.removeTop ();
					}
				}
			}
		}
	}

	// Use kruskal's to create the maze
	public void CreateMaze ()
	{
		// Add an Edge if it does not make a cycle and make the root of each Node the same
		foreach (Edge e in this.Edges) {
			if (!e.MakesCycle ()) {
				e.Unite ();
			}
		}
	}
	
	// Updates all the Nodes so they represent the generated maze
	public void UpdateNodes ()
	{
		foreach (Node n in this.Nodes.Values) {
			n.UpdateSides ();
		}
	}

	// Set and get
	int MazeSizeX {
		get {
			return this.mazeSizeX;
		}
		set {
			mazeSizeX = value;
		}
	}

	int MazeSizeZ {
		get {
			return this.mazeSizeZ;
		}
		set {
			mazeSizeZ = value;
		}
	}

	int MazeSizeY {
		get {
			return this.mazeSizeY;
		}
		set {
			mazeSizeY = value;
		}
	}

	Dictionary<Vector3, Node> Nodes {
		get {
			return this.nodes;
		}
		set {
			nodes = value;
		}
	}

	List<Edge> Edges {
		get {
			return this.edges;
		}
		set {
			edges = value;
		}
	}
}