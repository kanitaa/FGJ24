using UnityEngine;

public class Kitten : MonoBehaviour
{
  public Kitten kitten { get; private set; }
  public Transform target;

  private CatManMovement movement;

  private void Awake()
  {
    kitten = GetComponent<Kitten>();
    movement = GetComponent<CatManMovement>();
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    Node node = other.GetComponent<Node>();

    if (node != null && enabled)
    {
      Vector2 direction = Vector2.zero;
      float maxDistance = float.MinValue;

      // Find the available direction that moves farthest from pacman
      foreach (Vector2 availableDirection in node.availableDirections)
      {
        // If the distance in this direction is greater than the current
        // max distance then this direction becomes the new farthest
        Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
        float distance = (kitten.target.position - newPosition).sqrMagnitude;

        if (distance > maxDistance)
        {
          direction = availableDirection;
          maxDistance = distance;
        }
      }

      kitten.movement.SetDirection(direction);
    }
  }
}
