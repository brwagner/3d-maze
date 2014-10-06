using UnityEngine;
using System.Collections;

/**
 * Used to handle win conditions
 */ 
public class WinDetect : MonoBehaviour
{

  private bool win;

  // Use this for initialization
  void Start ()
  {
    win = false;
  }
  
  // Update is called once per frame
  void Update ()
  {
    Screen.showCursor = false;
    Screen.lockCursor = true;
    // Check if the player is out of the maze
    if (this.transform.position.y > Constants.POS_SCALE * Constants.MAZE_HEIGHT) {
      win = true;
    }
  }

  // Display the win message and replay options
  void OnGUI ()
  {
    if (win) {
      GUI.skin.label.fontSize = 100;
      GUI.skin.label.alignment = TextAnchor.MiddleCenter;
      GUI.Label (Rect.MinMaxRect (0, 0, Screen.width, Screen.height), "YOU WIN\nPress the \"r\" key to play again");
      if (Input.GetKey (KeyCode.R)) {
        Application.LoadLevel ("level");
      }
    }
  }
}
