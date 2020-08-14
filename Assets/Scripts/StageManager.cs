using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
  public int rows = 10;
  public int cols = 10;
  public float tileSize = 1;
  public GameObject empty;
  public GameObject player;

  void Start()
  {
    GenerateStage();
  }

  private void GenerateStage()
  {
    var panelRefs = new Dictionary<Railway, GameObject>();
    panelRefs.Add(Railway.Stop, (GameObject)Instantiate(Resources.Load("railway1")));
    panelRefs.Add(Railway.Cross, (GameObject)Instantiate(Resources.Load("railway2")));
    panelRefs.Add(Railway.LeftTop, (GameObject)Instantiate(Resources.Load("railway3")));
    panelRefs.Add(Railway.RightTop, (GameObject)Instantiate(Resources.Load("railway4")));
    panelRefs.Add(Railway.Wall, (GameObject)Instantiate(Resources.Load("wall")));

    var rand = new System.Random();
    for (int col = 0; col < cols; col++)
    {
      for (int row = 0; row < rows; row++)
      {
        if (row == 0 && col == 0) // starting point
        {
          GameObject panelObj = (GameObject)Instantiate(panelRefs[Railway.Cross], transform);
          panelObj.transform.position = new Vector2(col * tileSize, -row * tileSize);
        }
        else if (row == (int)(rows / 2) && col == (int)(cols / 2)) // center
        {
          empty.transform.position = new Vector2(col * tileSize, -row * tileSize);
        }
        else
        {
          var rail = (Railway)(rand.Next(4) + 1);
          GameObject panelObj = (GameObject)Instantiate(panelRefs[rail], transform);
          panelObj.transform.position = new Vector2(col * tileSize, -row * tileSize);
        }
      }
    }

    // Draw Walls
    DrawWallAndGoal(panelRefs[Railway.Wall]);

    // Destroy GameObjects
    foreach (KeyValuePair<Railway, GameObject> kvp in panelRefs)
    {
      Destroy(kvp.Value);
    }

    // shift to center
    var leftTop = new Vector2((1 - cols) * tileSize / 2, (1 + rows) * tileSize / 2);
    transform.position = leftTop;
    player.transform.position = leftTop;
  }

  private void DrawWallAndGoal(GameObject wallRef)
  {
    var walls = new List<Tuple<int, int>>();
    for (int col = -1; col <= cols; col++)  //ceil
    {
      walls.Add(new Tuple<int, int>(col, -1));
    }
    for (int col = -1; col <= cols; col++) //bottom
    {
      walls.Add(new Tuple<int, int>(col, rows));
    }
    for (int row = 0; row < rows; row++)  //left
    {
      walls.Add(new Tuple<int, int>(-1, row));
    }
    for (int row = 0; row < rows - 1; row++)  //right
    {
      walls.Add(new Tuple<int, int>(cols, row));
    }
    walls.ForEach(t =>
    {
      GameObject panelObj = (GameObject)Instantiate(wallRef, transform);
      panelObj.transform.position = new Vector2(t.Item1 * tileSize, -t.Item2 * tileSize);
    });
    // goal
    GameObject goal = (GameObject)Instantiate(Resources.Load("goal"), transform);
    goal.transform.position = new Vector2(cols * tileSize, -(rows - 1) * tileSize);
  }
  void Update()
  {
  }

  public void OnClick(Panel panel)
  {
    var d = (panel.transform.position - empty.transform.position).sqrMagnitude;
    if (d <= tileSize * tileSize)
    {
      // switch position
      var oldEmptyPosition = empty.transform.position;
      empty.transform.position = panel.transform.position;
      panel.transform.position = oldEmptyPosition;
    }
  }

  public void Reset()
  {
    Scene loadScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(loadScene.name);
  }
}
