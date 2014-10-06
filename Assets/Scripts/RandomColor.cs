
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class used in picking random colors for Nodes
 */ 
public class RandomColor
{
  // Possible colors
  private static Color[] colors =
  {
    Color.red,
    Color.green,
    Color.blue,
    Color.magenta,
    Color.cyan,
    Color.yellow
  };

  // Get a random color
  public static Color GetColor ()
  {
    return colors [Random.Range (0, colors.Length)];
  }
}

