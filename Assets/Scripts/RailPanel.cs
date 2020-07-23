using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RailPanel : MonoBehaviour, IPointerClickHandler
{
  public Railway railway;
  private StageManager stage;

  void Start()
  {
    GameObject stageObj = GameObject.Find("Stage");
    stage = stageObj.GetComponent<StageManager>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void OnPointerClick(PointerEventData pointerEventData)
  {
    stage.OnClick(this);
  }
}
