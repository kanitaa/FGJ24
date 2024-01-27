using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlienManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _alienText, _answerA, _answerB;
    [SerializeField] Button _aButton, _bButton;
    [SerializeField] List<DadJoke> _dadJokes;

    int _jokeIndex = 0;

    private void Start()
    {
        GenerateJoke();
    }
    void GenerateJoke()
    {
        _aButton.onClick.RemoveAllListeners();
        _bButton.onClick.RemoveAllListeners();

        _alienText.text = _dadJokes[_jokeIndex].punchLine;

        _answerA.text = _dadJokes[_jokeIndex].option[0];
        _answerB.text = _dadJokes[_jokeIndex].option[1];

        if (_dadJokes[_jokeIndex].answerId == 0)
        {
            _aButton.onClick.AddListener(CorrectAnswer);
            _bButton.onClick.AddListener(WrongAnswer);
        }
        if (_dadJokes[_jokeIndex].answerId == 1)
        {
            _aButton.onClick.AddListener(WrongAnswer);
            _bButton.onClick.AddListener(CorrectAnswer);
        }

        if (_jokeIndex < _dadJokes.Count - 1) _jokeIndex++;
        else _jokeIndex = 0;
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
