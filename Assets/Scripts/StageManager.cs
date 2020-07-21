using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Railway : int
{
    None = 0,
    Stop = 1,
    Cross = 2,
    Right = 3,
    Left = 4
}

public class StageManager : MonoBehaviour
{
  public int rows = 10;
  public int cols = 10;
  public float tileSize = 1;
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
    railRefs.Add(Railway.Right, (GameObject)Instantiate(Resources.Load("railway3")));
    railRefs.Add(Railway.Left, (GameObject)Instantiate(Resources.Load("railway4")));

    // for (int i = 0; i <= 3; i++)
    // {
    //   railRefs.Add((GameObject)Instantiate(Resources.Load($"railway{i}")));
    // }
    course = new Railway[rows][];

    var rand = new System.Random();
    for (int row = 0; row < rows; row++)
    {
      course[row] = new Railway[cols];
      for (int col = 0; col < cols; col++)
      {
        if (row != (int)(rows/2) || col != (int)(cols/2)) {
          course[row][col] = (Railway)(rand.Next(4)+1);
        }
      }
    }
    course[0][0] = Railway.Cross;
    course[1][0] = Railway.Cross;
    // course[2][0] = Railway.Cross;

    for (int row = 0; row < rows; row++)
    {
      for (int col = 0; col < cols; col++)
      {
        Railway rail = course[row][col];
        if (rail != Railway.None) {
          GameObject railObj = (GameObject)Instantiate(railRefs[rail], transform);
          float posX = col * tileSize;
          float posY = -row * tileSize;
          railObj.transform.position = new Vector2(posX, posY);
        }
      }
    }

    foreach(KeyValuePair<Railway, GameObject> kvp in railRefs )
    {
      Destroy(kvp.Value);
    }

    float gridWidth = cols * tileSize;
    float gridHight = rows * tileSize;
    transform.position = new Vector2((tileSize - gridWidth) / 2, (tileSize + gridHight) / 2);
  }
  // Update is called once per frame
  void Update()
  {

  }
}
