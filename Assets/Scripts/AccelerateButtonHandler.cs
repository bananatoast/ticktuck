﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AccelerateButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  public GameObject player;
  private PlayerController playerController;
  static int SPEED_UP = 20;
  private int originalSpeed;
  void Start()
  {
    playerController = player.GetComponent<PlayerController>();
    originalSpeed = playerController.Speed;
  }
  public void OnPointerDown(PointerEventData eventData)
  {
    playerController.Speed = originalSpeed + SPEED_UP;
  }
  public void OnPointerUp(PointerEventData eventData)
  {
    playerController.Speed = originalSpeed;
  }
}
