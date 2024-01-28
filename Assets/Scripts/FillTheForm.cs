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
    [SerializeField] Controller _ctrl;

    [SerializeField] Button _patientName, _restartName, _submitName;
    [SerializeField] TextMeshProUGUI _patientNameText;
    [SerializeField] GameObject _nameGame;
    string _scrambledName = "";

    // Number variables
    [SerializeField] List<TextMeshProUGUI> _numbers;
    int _currentNumber= 0;
    int _tempNumber = -1;
    [SerializeField] Button _enableNumbers, _restartNumbers, _setNumber;

    // Symptom
    [SerializeField] TMP_InputField _symptom;
   

    // Draw
    Camera _mainCam;
    [SerializeField] GameObject _pen;
    LineRenderer _currentLR;
    Vector2 _lastPos;
    [SerializeField] GameObject _drawingsHolder;

    bool _canDraw;
    public bool canDraw
    {
        get => _canDraw;
        set => _canDraw = value;
    }


    // Submit
    [SerializeField] Button _submitForm, _goToSurgery;
    [SerializeField] GameObject _submitPanel;
    [TextArea(5,5)]
    string _submitString,  _urgentString;
    int _stringIndex;
    [SerializeField] TextMeshProUGUI _urgentText, _submitText, _diagnosisText;
   

    [SerializeField] TextMeshProUGUI _errorMessage;


    bool _isNameDone, _isNumberDone, _isSymptomDone, _isDrawingDone=false;

    [SerializeField] AudioClip urgentClip;
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

        _mainCam = Camera.main;

        _symptom.onEndEdit.AddListener(Symptom);

        _submitForm.onClick.AddListener(SubmitForm);
        _goToSurgery.onClick.AddListener(GoToSurgery);
        _goToSurgery.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(_canDraw) Draw();
    }
    #region Name

    void EnableNameGame()
    {
        _nameGame.SetActive(true);
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
            int indexChar = Random.Range(0, characters.Count);
            result += characters[indexChar];
            characters.RemoveAt(indexChar);

        }
        if(result[0] != result[1] && result.Length>2)
        {
            while (result == input)
            {
                result = "";
                characters = new List<char>(input.ToCharArray());
                while (characters.Count > 0)
                {
                    int indexChar = Random.Range(0, characters.Count);
                    result += characters[indexChar];
                    characters.RemoveAt(indexChar);

                }
            }
        }
            _patientNameText.text = result;
            _scrambledName = result;
            _isNameDone = true;
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

        _numbers[_currentNumber].GetComponentInParent<Image>().color = Color.green;
        _numbers[_currentNumber].text = _tempNumber.ToString();
        yield return new WaitForSeconds(0.15f);

        StartCoroutine(ChangeNumber());
    }

    void RestartNumbers()
    {
        foreach(TextMeshProUGUI number in _numbers)
        {
            number.text = "";
            number.GetComponentInParent<Image>().color = Color.gray;
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
        }

        _tempNumber = -1;
    }

    #endregion

    #region Symptoms
    void Symptom(string input)
    {
        if(input.Length>0) _isSymptomDone = true;
    }

    #endregion

    #region Drawing

    void Draw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBrush();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            if(mousePos != _lastPos)
            {
                AddAPoint(mousePos);
                _lastPos = mousePos;
            }
            
        }
        else
        {
            _currentLR = null;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && !_isDrawingDone)
        {
            _isDrawingDone = true;
            Debug.Log("Drawing ended");
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(_pen, _drawingsHolder.transform);
        _currentLR = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _currentLR.SetPosition(0, mousePos);
        _currentLR.SetPosition(1, mousePos);
    }

    void AddAPoint(Vector2 pointPos)
    {
        _currentLR.positionCount++;
        int posIndex = _currentLR.positionCount-1;
        _currentLR.SetPosition(posIndex, pointPos);
    }
    #endregion

    void SubmitForm()
    {
        if (!_isNameDone)
        {
            _errorMessage.text = "You need to write down your name first.";
           
        }
        else if(!_isNumberDone)
        {
            _errorMessage.text = "You need to add your phone number first.";
        }
        else if (!_isDrawingDone)
        {
            _errorMessage.text = "You need to draw your symptoms first.";
        }
        else if (!_isSymptomDone)
        {
            _errorMessage.text = "You need to describe your symptoms first.";
        }
       
        else
        {
            _ctrl.musicSource.Stop();
            _ctrl.soundSource.PlayOneShot(urgentClip);
            Debug.Log("Submit form");
            _drawingsHolder.SetActive(false);
            _submitPanel.SetActive(true);
            _urgentString = "URGENT!!!";
            _submitString = "You need immediate surgery, " + _scrambledName+ ". But our surgeon is playing golf right now (and"+
                " you can't afford it anyway). You need to perform self-surgery! You know, like a surgeon!";
            StartCoroutine(ShowTitle());
        }
        StartCoroutine(HideErrorMessage());
    }
    IEnumerator HideErrorMessage()
    {
        yield return new WaitForSeconds(1.4f);
        _errorMessage.text = "";
    }

    IEnumerator ShowTitle()
    {
        _urgentText.text += _urgentString.ElementAt(_stringIndex);
        if (_stringIndex < _urgentString.Length - 1)
            _stringIndex++;
        else
        {
            Debug.Log("Last letter");
            _stringIndex = 0;
            StopAllCoroutines();
            StartCoroutine(ShowDiagnosis());

        }
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(ShowTitle());
    }
    IEnumerator ShowDiagnosis()
    {
        _submitText.text += _submitString.ElementAt(_stringIndex);
        if (_stringIndex < _submitString.Length - 1)
            _stringIndex++;
        else
        {
            Debug.Log("Last letter");
            yield return new WaitForSeconds(0.5f);
            StopAllCoroutines();
            _diagnosisText.gameObject.SetActive(true);
            _goToSurgery.gameObject.SetActive(true);
            
        }
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(ShowDiagnosis());
    }

    void GoToSurgery()
    {
        _ctrl.Next();
        transform.parent.gameObject.SetActive(false);
    }
}
