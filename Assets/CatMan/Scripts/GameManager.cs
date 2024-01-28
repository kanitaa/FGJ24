using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public CatMan catman;
  public Transform victoryScreen;
  public bool success;
  public Controller _ctrl;
  private int catsCaught;
    public AudioClip kittenClip;

    private void Start()
  {
    this.catman.gameObject.SetActive(true);
    Instance = this;
  }
    public void AlienPopup()
    {
        StartCoroutine(Alien());
    }
    IEnumerator Alien()
    {
        yield return new WaitForSeconds(6);
        _ctrl.Next();
       
    }
  private void GameOver()
  {
        StopAllCoroutines();
        _ctrl.GameOver();
        transform.parent.gameObject.SetActive(false);
        
        
    }

  public void KittenCaught()
  {
        _ctrl.soundSource.PlayOneShot(kittenClip);
    catsCaught += 1;
    if (catsCaught == 4)
    {
            GameOver();
    }
    
  }
}