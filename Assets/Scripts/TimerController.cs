using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
  private Text timer;
  private float current = 0f;
  private bool isStopped = false;
  void Start()
  {
    timer = GetComponent<Text>();
    displayTime();
  }

  void Update()
  {
    if (!isStopped)
    {
      current += Time.deltaTime;
      displayTime();
    }
  }

  public void Stop()
  {
    isStopped = true;
  }
  private void displayTime()
  {
    timer.text = "Time: " + current.ToString("0.0");
  }
}
