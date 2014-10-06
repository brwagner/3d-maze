using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Edges used in generating the maze
 * Each Edge contains two nodes and a weight (cost)
 */
public class Edge
{
  private Node n1;
  private Node n2;
  private int weight;

  // Constructor
  public Edge (Node n1, Node n2, int weight)
  {
    this.N1 = n1;
    this.N2 = n2;
    this.Weight = weight;
  }

  // Check if this contains the given Node
  bool Contains (Node n)
  {
    return this.N1.Equals (n) || this.N2.Equals (n);
  }

  // Check if this Edge's Node's roots are the same
  public bool MakesCycle ()
  {
    return this.N1.Grandparent.Equals (this.N2.Grandparent);
  }

  // Add each Node to eachother's neighbors' lists
  public void Unite ()
  {
    this.N1.Grandparent.Parent = this.N2.Grandparent;
    this.N1.Neighbors.Add (this.N2);
    this.N2.Neighbors.Add (this.N1);
  }

  // Set and get
  public Node N1 {
    get {
      return this.n1;
    }
    set {
      n1 = value;
    }
  }
  
  public Node N2 {
    get {
      return this.n2;
    }
    set {
      n2 = value;
    }
  }
  
  public int Weight {
    get {
      return this.weight;
    }
    set {
      weight = value;
    }
  }
}