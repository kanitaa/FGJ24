using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public CatMan catman;
  public Transform victoryScreen;
  public bool success;
  public Controller _ctrl;
  private int catsCaught;


  private void Start()
  {
    this.catman.gameObject.SetActive(true);
    Instance = this;
  }

  private void GameOver()
  {
    this.catman.gameObject.SetActive(false);
    _ctrl.Next();
  }

  public void KittenCaught()
  {
    catsCaught += 1;
    if (catsCaught == 4)
    {
      victoryScreen.position = new Vector3(0,0,-2);
    }
    
  }
}