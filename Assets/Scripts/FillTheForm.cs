using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;

public class FillTheForm : MonoBehaviour
{
    [SerializeField] Button _patientName, _restartName, _submitName;
    [SerializeField] TextMeshProUGUI _patientNameText;
    [SerializeField] GameObject _nameGame;

    // Number variables
    [SerializeField] List<TextMeshProUGUI> _numbers;
    int _currentNumber= 0;
    int _tempNumber = -1;
    [SerializeField] Button _enableNumbers, _restartNumbers, _setNumber;

    // Symptoms
    [SerializeField] List<Toggle> _symptoms;
    bool _isToggling = false;



    // Diagnosis
    [SerializeField] TextMeshProUGUI _diagnosisText;
    [SerializeField] string _diagnosis;
    int _diagnosisIndex=0;





    bool _isNameDone, _isNumberDone, _isSymptomDone=false;
    private void Start()
    {
        _patientName.onClick.AddListener(EnableNameGame);
        _restartName.onClick.AddListener(RestartNameGame);
        _submitName.onClick.AddListener(SubmitName);

        _restartNumbers.onClick.AddListener(RestartNumbers);
        _setNumber.onClick.AddListener(()=>SetNumber(_tempNumber));
        _enableNumbers.onClick.AddListener(EnableNumbers);

        _restartNumbers.gameObject.SetActive(false);
        _setNumber.gameObject.SetActive(false);
    }

    #region Name

    void EnableNameGame()
    {
        _nameGame.SetActive(true);
        _patientName.gameObject.SetActive(false);
    }

    public void AddLetter(string letter)
    {
        _patientNameText.text += letter;
    }
    void RestartNameGame()
    {
        _patientNameText.text = "";
    }
    void SubmitName()
    {
        _nameGame.SetActive(false);
        ScrambleName(_patientNameText.text);
    }

    void ScrambleName(string input)
    {
        string result = "";
        List<char> characters = new List<char>(input.ToCharArray());
        while (characters.Count > 0)
        {
            int indexChar = Random.Range(0, characters.Count - 1);
            result += characters[indexChar];
            characters.RemoveAt(indexChar);

        }

        _patientNameText.text = result;
        _isNameDone = true;
        if (_isNameDone && _isNumberDone && _isSymptomDone) StartCoroutine(ShowDiagnosis());
    }
    #endregion

    #region Number

    void EnableNumbers()
    {
        _enableNumbers.gameObject.SetActive(false);
        _restartNumbers.gameObject.SetActive(true);
        _setNumber.gameObject.SetActive(true);

        StartCoroutine(ChangeNumber());
    }

    IEnumerator ChangeNumber()
    {
        if (_tempNumber < 9)
            _tempNumber++;
        else _tempNumber = 0;

        _numbers[_currentNumber].text = _tempNumber.ToString();
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(ChangeNumber());
    }

    void RestartNumbers()
    {
        foreach(TextMeshProUGUI number in _numbers)
        {
            number.text = "";
        }
        _currentNumber = 0;
        _tempNumber = -1;
    }

    void SetNumber(int number)
    {
        _numbers[_currentNumber].text = number.ToString();
        if (_currentNumber < _numbers.Count - 1)
            _currentNumber++;
        else
        {
            Debug.Log("Last number");
            StopAllCoroutines();
            _setNumber.gameObject.SetActive(false);
            _restartNumbers.gameObject.SetActive(false);
            _isNumberDone = true;

            if(_isNameDone&&_isNumberDone&&_isSymptomDone) StartCoroutine(ShowDiagnosis());
        }

        _tempNumber = -1;
    }

    #endregion

    public void ToggleSymptom()
    {
        if (!_isToggling)
        {
            int random = Random.Range(0, _symptoms.Count - 1);
            _symptoms[random].isOn = true;
            _symptoms[random].StartCoroutine(KeepToggling());
        }

        _isToggling = true;

      
    }
    IEnumerator KeepToggling()
    {
        bool allToggled = true;
        foreach (Toggle toggle in _symptoms)
        {
            if (!toggle.isOn)
            {
                toggle.isOn = true;
                allToggled = false;
                break ;
            }
        }
        if (allToggled)
        {
            Debug.Log("All toggled");
            StopCoroutine(KeepToggling());
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator ShowDiagnosis()
    {
        _diagnosisText.text += _diagnosis.ElementAt(_diagnosisIndex);
        if (_diagnosisIndex < _diagnosis.Length - 1)
            _diagnosisIndex++;
        else
        {
            Debug.Log("Last letter");
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(0.05f);

        StartCoroutine(ShowDiagnosis());
    }

}
