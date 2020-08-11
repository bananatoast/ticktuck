using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Panel : MonoBehaviour, IPointerClickHandler
{
  public Railway railway;
  private StageManager stage;
  private PlayerController player;
  private bool isPlayerOn;

  void Start()
  {
    GameObject stageObj = GameObject.Find("Stage");
    stage = stageObj.GetComponent<StageManager>();
    GameObject playerObj = GameObject.Find("Player");
    player = playerObj.GetComponent<PlayerController>();
    isPlayerOn = false;
  }

  void Update()
  {
    var overlap = GetComponent<Collider2D>().OverlapPoint(player.transform.position);
    if (isPlayerOn && !overlap)
    {
      isPlayerOn = false;
    }
    else if (!isPlayerOn && overlap)
    {
      isPlayerOn = true;
      player.EnterPanel(this);
    }
  }

  public void OnPointerClick(PointerEventData pointerEventData)
  {
    if (railway != Railway.Wall && !isPlayerOn)
    {
      Debug.LogFormat("Click {0}", transform.position);
      stage.OnClick(this);
    }
  }
}
