using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
  public int rows = 10;
  public int cols = 10;
  public float tileSize = 1;
  public GameObject empty;
  public GameObject player;
  private Railway[][] course;

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

    course = new Railway[cols][];

    var rand = new System.Random();
    for (int col = 0; col < cols; col++)
    {
      course[col] = new Railway[rows];
      for (int row = 0; row < rows; row++)
      {
        if (row != (int)(rows / 2) || col != (int)(cols / 2))
        {
          course[col][row] = (Railway)(rand.Next(4) + 1);
        }
        else
        {
          empty.transform.position = new Vector2(col * tileSize, -row * tileSize);
        }
      }
    }
    course[0][0] = Railway.Cross;
    course[1][0] = Railway.RightTop;

    for (int col = 0; col < cols; col++)
    {
      for (int row = 0; row < rows; row++)
      {
        Railway rail = course[col][row];
        if (rail != Railway.None)
        {
          GameObject panelObj = (GameObject)Instantiate(panelRefs[rail], transform);
          panelObj.transform.position = new Vector2(col * tileSize, -row * tileSize);
        }
      }
    }
    // Draw Walls
    DrawWalls((GameObject)Instantiate(panelRefs[Railway.Wall], transform));

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

  private void DrawWalls(GameObject wallRef)
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
    for (int row = 0; row < rows; row++)  //right
    {
      walls.Add(new Tuple<int, int>(cols, row));
    }
    walls.ForEach(t =>
    {
      GameObject panelObj = (GameObject)Instantiate(wallRef, transform);
      panelObj.transform.position = new Vector2(t.Item1 * tileSize, -t.Item2 * tileSize);
    });

  }
  void Update()
  {
  }

  public void OnClick(Panel panel)
  {
    var d = (panel.transform.position - empty.transform.position).sqrMagnitude;
    if (d <= tileSize * tileSize)
    {
      //TODO update course

      // switch position
      var oldEmptyPosition = empty.transform.position;
      empty.transform.position = panel.transform.position;
      panel.transform.position = oldEmptyPosition;
    }
  }
}
