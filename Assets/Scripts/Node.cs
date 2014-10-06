using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// disable hashcode warning
#pragma warning disable 0659

/**
 * Class that defines the Nodes used in the maze and the data
 * used to represent it in the game
 */ 
public class Node
{
  private Vector3 coordinate;
  private Node parent;
  private List<Node> neighbors;
  private GameObject gameObject;

  // Use this for initialization
  public Node (Vector3 coordinate, GameObject gameObject, Color color)
  {
    this.Coordinate = coordinate;
    this.Parent = this;
    this.Neighbors = new List<Node> ();
    this.GameObject = gameObject;

    // Offset the position to adjust for scaling
    this.GameObject.transform.position = this.Coordinate * Constants.POS_SCALE;

    // Scale based on constant
    this.GameObject.transform.localScale = Constants.SCALE_SCALE;

    // Illuminate the Node with a random color
    GameObject light = this.getComponent ("Light");
    light.light.range *= Constants.LIGHT_SCALE;
    light.light.color = color;
  }

  // Removes sides of the Node that correspond to the minimum span tree representing the maze
  public void UpdateSides ()
  {
    // If a Node has a neighbor, remove the side the neighbor is touching
    foreach (Node n in this.Neighbors) {
      GameObject go = null;
      if ((this.Coordinate + Vector3.left).Equals (n.Coordinate)) {
        go = this.getComponent ("X-");
      } else if ((this.Coordinate + Vector3.right).Equals (n.Coordinate)) {
        go = this.getComponent ("X+");
      } else if ((this.Coordinate + Vector3.down).Equals (n.Coordinate)) {
        go = this.getComponent ("Y-");
      } else if ((this.Coordinate + Vector3.up).Equals (n.Coordinate)) {
        go = this.getComponent ("Y+");
      } else if ((this.Coordinate + Vector3.back).Equals (n.Coordinate)) {
        go = this.getComponent ("Z-");
      } else if ((this.Coordinate + Vector3.forward).Equals (n.Coordinate)) {
        go = this.getComponent ("Z+");
      }
      go.renderer.enabled = false;
      go.collider.enabled = false;
    }
  }

  // Removes the top of the Node
  public void removeTop ()
  {
    GameObject go = this.getComponent ("Y+");
    go.renderer.enabled = false;
    go.collider.enabled = false;
  }

  // Gets the GameObject in the Node with the given string
  public GameObject getComponent (string s)
  {
    return this.GameObject.transform.FindChild (s).gameObject;
  }

  // Override the Equals method to mean if this Node occupies the same space as the given object
  public override bool Equals (System.Object obj)
  {
    // If parameter is null return false.
    if (obj == null) {
      return false;
    }
    
    // If parameter cannot be cast to Point return false.
    Node n = obj as Node;
    if ((System.Object)n == null) {
      return false;
    }
    
    // Return true if the fields match:
    return
      (this.Coordinate.x == n.Coordinate.x) &&
      (this.Coordinate.y == n.Coordinate.y) &&
      (this.Coordinate.z == n.Coordinate.z);
  }

  // Set and get
  public Node Grandparent {
    get {
      if (this.parent.Equals (this)) {
        return this;
      } else {
        return this.Parent.Grandparent;
      }
    }
    set {
      this.Grandparent = value;
    }
  }

  public Vector3 Coordinate {
    get {
      return this.coordinate;
    }
    set {
      coordinate = value;
    }
  }
  
  public Node Parent {
    get {
      return this.parent;
    }
    set {
      parent = value;
    }
  }
  
  public List<Node> Neighbors {
    get {
      return this.neighbors;
    }
    set {
      neighbors = value;
    }
  }
  
  public GameObject GameObject {
    get {
      return this.gameObject;
    }
    set {
      gameObject = value;
    }
  }
}
