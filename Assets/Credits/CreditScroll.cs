using UnityEngine;

public class CreditScroll : MonoBehaviour
{
  public Transform textTransform;
  public float speed = 1;
  void FixedUpdate()
  {
    textTransform.position = textTransform.position + Vector3.up * speed * Time.deltaTime;
  }
}
