using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void Move();

public class PlayerController : MonoBehaviour
{
  static Vector3 LeftTop = new Vector3(-0.5f, 0.5f, 0);
  static Vector3 LeftBottom = new Vector3(-0.5f, -0.5f, 0);
  static Vector3 RightTop = new Vector3(0.5f, 0.5f, 0);
  static Vector3 RightButtom = new Vector3(0.5f, -0.5f, 0);
  private Vector3 velocity;
  private float omega;
  private Railway rail;
  private Move next;
  void Start()
  {
    Debug.LogFormat("player {0}", transform.position);
    velocity = new Vector3(0.001f, 0, 0);
    next = () => { transform.position += velocity; };
  }

  void Update()
  {
    next();
    Debug.LogFormat("{0}", transform.position);
  }
  public void EnterPanel(RailPanel panel)
  {
    Debug.LogFormat("Player entered {0}: {1}", transform.position, panel.railway);
    rail = panel.railway;
    switch (rail)
    {
      case Railway.Stop:
        velocity = -velocity;
        next = () => { transform.position += velocity; };
        break;
      case Railway.RightTop:
        next = () =>
        {
          //   transform.RotateAround(panel.transform.position, -transform.forward, 10f * Time.deltaTime);
          var angleAxis = Quaternion.AngleAxis(-360 / 10 * Time.deltaTime, Vector3.forward);
          var pos = transform.position;
          pos -= (panel.transform.position + LeftBottom);
          pos = angleAxis * pos;
          pos += (panel.transform.position + LeftBottom);
          transform.position = pos;
        };
        break;
      case Railway.LeftTop:
        next = () =>
        {
          //   transform.RotateAround(panel.transform.position, transform.forward, 10f * Time.deltaTime);
          var angleAxis = Quaternion.AngleAxis(360 / 10 * Time.deltaTime, Vector3.forward);
          var pos = transform.position;
          pos -= (panel.transform.position + LeftTop);
          pos = angleAxis * pos;
          pos += (panel.transform.position + LeftTop);
          transform.position = pos;
        };
        break;
      default:
        next = () => { transform.position += velocity; };
        break;
    }
  }
}
