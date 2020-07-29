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
    var railRefs = new Dictionary<Railway, GameObject>();
    railRefs.Add(Railway.Stop, (GameObject)Instantiate(Resources.Load("railway1")));
    railRefs.Add(Railway.Cross, (GameObject)Instantiate(Resources.Load("railway2")));
    railRefs.Add(Railway.LeftTop, (GameObject)Instantiate(Resources.Load("railway3")));
    railRefs.Add(Railway.RightTop, (GameObject)Instantiate(Resources.Load("railway4")));

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
    // course[1][0] = Railway.Cross;
    // course[1][0] = Railway.RightTop;

    for (int col = 0; col < cols; col++)
    {
      for (int row = 0; row < rows; row++)
      {
        Railway rail = course[col][row];
        if (rail != Railway.None)
        {
          GameObject railObj = (GameObject)Instantiate(railRefs[rail], transform);
          RailPanel panel = railObj.GetComponent<RailPanel>();
          railObj.transform.position = new Vector2(col * tileSize, -row * tileSize);
        }
      }
    }

    foreach (KeyValuePair<Railway, GameObject> kvp in railRefs)
    {
      Destroy(kvp.Value);
    }

    // float gridWidth = cols * tileSize;
    // float gridHight = rows * tileSize;
    // transform.position = new Vector2((tileSize - gridWidth) / 2, (tileSize + gridHight) / 2);
  }

  void Update()
  {
  }

  public void OnClick(RailPanel panel)
  {
    Debug.LogFormat("Click {0}", panel.transform.position);

    var d = (panel.transform.position - empty.transform.position).sqrMagnitude;
    if (d <= tileSize * tileSize)
    {
      //TODO update course

      // switch positino
      var oldEmptyPosition = empty.transform.position;
      empty.transform.position = panel.transform.position;
      panel.transform.position = oldEmptyPosition;
    }
  }
}
