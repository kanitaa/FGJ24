using UnityEngine;

[RequireComponent(typeof(CatManMovement))]
public class CatMan : MonoBehaviour
{
  public CatManMovement movement { get; private set; }

    public bool isAlienDistracting = false;

  private void Awake()
  {
    this.movement = GetComponent<CatManMovement>();
  }
  
  void Update()
  {
        if (isAlienDistracting) return;

    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
      this.movement.SetDirection(Vector2.up);
    }
    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
      this.movement.SetDirection(Vector2.right);
    }
    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
      this.movement.SetDirection(Vector2.down);
    }
    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
      this.movement.SetDirection(Vector2.left);
    }
  }
  private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.layer == LayerMask.NameToLayer("Kitten"))
      {
        
        GameManager.Instance.KittenCaught();
        Destroy(collision.gameObject);
      }
    }
}
