using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    Debug.LogFormat("player {0}", transform.position);
  }

  // Update is called once per frame
  void Update()
  {
    var position = transform.position;
    position.x += 0.001f;
    transform.position = position;
    // Debug.LogFormat("Player ({0},{1})", (int)((transform.position.x + 0.5) / 1.05), (int)((transform.position.y + 0.5) / 1.05));
  }
  public void EnterPanel(RailPanel panel)
  {
    Debug.LogFormat("Player entered {0}: {1}", transform.position, panel.railway);
  }
}
