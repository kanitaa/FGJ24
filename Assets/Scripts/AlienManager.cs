using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Joke
{
    public string joke;
    public string punchline;
}

public class AlienManager : MonoBehaviour
{
    [SerializeField] Controller _ctrl;
    [SerializeField] TextMeshProUGUI _alienText, _answerA, _answerB;
    [SerializeField] Button _aButton, _bButton;
    public bool isDistraction = false;

    private List<Joke> jokeList;

    int _correctCounter,_incorrectCounter = 0;
    Animator anim;
    [SerializeField] AudioClip _laughClip, _loseClip;
    [SerializeField] AudioClip[]  _fartClips;
    bool firstQuestion = false;
   
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        using (StreamReader r = new StreamReader("Assets/dad-jokes.json"))
        {
            string json = r.ReadToEnd();
            jokeList = JsonConvert.DeserializeObject<List<Joke>>(json);
        }
        GenerateJoke();
    }

    void GenerateJoke()
    {
        _aButton.onClick.RemoveAllListeners();
        _bButton.onClick.RemoveAllListeners();

        int correct = Random.Range(0, jokeList.Count);
        int incorrect = Random.Range(0, jokeList.Count);

        _alienText.text = jokeList[correct].punchline;

        string correct_joke = jokeList[correct].joke;
        string incorrect_joke = jokeList[incorrect].joke;

        

        if (Random.Range(0,2) == 0)
        {
            _answerA.text = correct_joke;
            _answerB.text = incorrect_joke;
            _aButton.onClick.AddListener(CorrectAnswer);
            _bButton.onClick.AddListener(WrongAnswer);
        }
        else 
        {
            _answerA.text = incorrect_joke;
            _answerB.text = correct_joke;
            _aButton.onClick.AddListener(WrongAnswer);
            _bButton.onClick.AddListener(CorrectAnswer);
        }
    }
   
    void CorrectAnswer()
    {
        Debug.Log("Correct!");
        if (!firstQuestion) _ctrl.musicSource.Stop();
        firstQuestion = true;
        _ctrl.soundSource.PlayOneShot(_laughClip);
        _correctCounter++;
       
        if (_correctCounter == 5) Win();
        else _ctrl.Next();
       GenerateJoke();
       transform.parent.gameObject.SetActive(false);
    }
    void WrongAnswer()
    {
        int random = Random.Range(0, 2);
        _ctrl.soundSource.PlayOneShot(_fartClips[random]);
        Debug.Log("Incorrect!");
        anim.SetTrigger("fart");
        //Lose();
        _incorrectCounter++;
        if (_incorrectCounter == 5) Lose();
        else GenerateJoke();
    }

    void Win()
    {
        Debug.Log("Win");
        transform.parent.gameObject.SetActive(false);
        _ctrl.GameOver();
       
        _ctrl.catman.SetActive(false);
        
    }

    void Lose()
    {
        _ctrl.soundSource.PlayOneShot(_loseClip);
        Debug.Log("Lose");
        transform.parent.gameObject.SetActive(false);
      
    }
}
