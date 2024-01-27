using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokeList
{
    public List<string> jokes;
    public List<string> punchlines;
}

public class Joke
{
    public string joke;
    public string punchline;
}

public class AlienManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _alienText, _answerA, _answerB;
    [SerializeField] Button _aButton, _bButton;
    [SerializeField] List<DadJoke> _dadJokes;

    private List<Joke> jokeList;

    private void Start()
    {
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

        Debug.Log(jokeList.Count);
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
        GenerateJoke();
    }
    void WrongAnswer()
    {
        Debug.Log("Incorrect!");
        GenerateJoke();
    }
}
