using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void Move();

public class PlayerController : MonoBehaviour
{
  static float Speed = 0.001f;
  static Vector3 LeftTop = new Vector3(-0.5f, 0.5f, 0);
  static Vector3 LeftBottom = new Vector3(-0.5f, -0.5f, 0);
  static Vector3 RightTop = new Vector3(0.5f, 0.5f, 0);
  static Vector3 RightButtom = new Vector3(0.5f, -0.5f, 0);
  private Vector3 vector;
  private Vector3 pole;
  private int rotation = 1; //1: clockwise, -1:anti-clockwise
  private Railway rail;
  private Move next;
  void Start()
  {
    vector = new Vector3(1f, 0, 0);
    next = () => { transform.position += vector * Speed; };
  }

  void Update()
  {
    next();
    // Debug.LogFormat("{0}", transform.position);
  }
  public void EnterPanel(Panel panel)
  {
    rail = panel.railway;
    switch (rail)
    {
      case Railway.Stop:
      case Railway.Wall:
        vector = -vector;
        next = () => { transform.position += vector * Speed; };
        break;
      case Railway.RightTop:
        pole = Vector3.Dot(vector, new Vector3(1, 1, 0)) > 0 ? LeftBottom : RightTop;
        rotation = (Mathf.Abs(vector.x) < Speed / 10) ? 1 : -1;
        vector = Quaternion.Euler(0, 0, rotation * 90) * vector;
        next = () =>
        {
          var angleAxis = Quaternion.AngleAxis(rotation * 360 / 10 * Time.deltaTime, Vector3.forward);
          var pos = transform.position;
          pos -= (panel.transform.position + pole);
          pos = angleAxis * pos;
          pos += (panel.transform.position + pole);
          transform.position = pos;
        };
        break;
      case Railway.LeftTop:
        pole = Vector3.Dot(vector, new Vector3(1, -1, 0)) > 0 ? LeftTop : RightButtom;
        rotation = (Mathf.Abs(vector.y) < Speed / 10) ? 1 : -1;
        vector = Quaternion.Euler(0, 0, rotation * 90) * vector;
        next = () =>
        {
          var angleAxis = Quaternion.AngleAxis(rotation * 360 / 10 * Time.deltaTime, Vector3.forward);
          var pos = transform.position;
          pos -= (panel.transform.position + pole);
          pos = angleAxis * pos;
          pos += (panel.transform.position + pole);
          transform.position = pos;
        };
        break;
      default:
        next = () => { transform.position += vector * Speed; };
        break;
    }
    Debug.Log($"Player entered:{panel.railway} {transform.position} v:({vector.x:0.00}, {vector.y:0.00}) r:{rotation}");
  }
}
