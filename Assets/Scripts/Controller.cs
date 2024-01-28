using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Controller : MonoBehaviour
{
    public GameObject canvas;
    public GameObject background;
    public GameObject overlay;
    public GameObject alienPopup;
    public GameObject kidney;
    public GameObject robot;
    public GameObject form;
    public GameObject catman;
    private int state = 1;
    private GameObject activeOverlay;


    [SerializeField] GameObject _scene, _titleScene;
    [SerializeField] List<Sprite> _sceneSprites;
    [TextArea(5,5)]
    [SerializeField] List<string> _sceneStrings;
    [SerializeField] GameObject _organs;
    [SerializeField] GameObject _nextButton;
    int _stringIndex = 0;
    bool _textRunning = false;


    [SerializeField] List<Sprite> _gameOverSprites;
    [TextArea(5, 5)]
    [SerializeField] List<string> _gameOverStrings;

    public bool isGameOver = false;

    public void SetState(int state)
    {
        this.state = state;
    }
    // Start is called before the first frame update
    void Start()
    {
        catman.SetActive(false);
        Next();   
    }


    public void Next()
    {
        if (_textRunning)
        {
            StopAllCoroutines();
            _textRunning = false;
            _stringIndex = 0;
            _scene.GetComponentInChildren<TextMeshProUGUI>().text = _sceneStrings[state-1];
            return;
        }

        if (!isGameOver)
        {
            if (state < 10) state++;
            else state--;
        }
        else if (isGameOver) state++;
        




        if (state < 9)
        {
            _scene.GetComponentInChildren<TextMeshProUGUI>().text = "";
            _scene.GetComponentInChildren<Image>().sprite = _sceneSprites[state-1];
            StartCoroutine(FancyText(state-1));
        }
       
        Debug.Log(state);
        if (state == 5) //Form
        {
            form.SetActive(true);
            _scene.SetActive(false);
            _nextButton.SetActive(false);
            _textRunning = false;
            _stringIndex = 0;
        }
        if (state == 6) //Title screen
        {
            activeOverlay = Instantiate(_titleScene, overlay.transform);
            activeOverlay.transform.SetAsFirstSibling();
            _nextButton.SetActive(true);
        }
        if(state == 7)
        {
            Destroy(activeOverlay);
            _scene.SetActive(true);
        }
        if (state == 8)
        {
            _organs.SetActive(true);
        }
        if (state == 9) //Alien popup
        {
            TriggerAlien();
        }

        if (state == 10)
        {

            catman.SetActive(true);
            _organs.SetActive(false);
            alienPopup.SetActive(false);
            _nextButton.SetActive(false);
            catman.GetComponentInChildren<CatMan>().isAlienDistracting = false;
            StartCoroutine(Alien());
        }
        if (state > 10 && state < 17)
        {
            catman.SetActive(false);
            _scene.GetComponentInChildren<TextMeshProUGUI>().text = "";
            _scene.GetComponentInChildren<Image>().sprite = _gameOverSprites[state - 1];
            StartCoroutine(FancyText(state - 1));
        }

        //if (state == 10)
        //{
        //    activeOverlay = Instantiate(kidney, overlay.transform.position, Quaternion.identity, overlay.transform);
        //    activeOverlay.transform.SetParent(overlay.transform);
        //    activeOverlay.transform.localScale += new Vector3(1, 1, 1);
        //}
        //if (state == 11)
        //{
        //    Destroy(activeOverlay);
        //    activeOverlay = Instantiate(robot, overlay.transform.position, Quaternion.identity, overlay.transform);
        //    activeOverlay.transform.SetParent(overlay.transform);
        //    activeOverlay.transform.localScale += new Vector3(1, 1, 1);
        //}


    }

    IEnumerator Alien()
    {
        yield return new WaitForSeconds(6);
        Next();

    }

    public void AlienDistraction()
    {
        _scene.transform.GetChild(0).gameObject.SetActive(false);
        alienPopup.SetActive(true);
        _nextButton.SetActive(false);
        alienPopup.GetComponentInChildren<AlienManager>().isDistraction = true;

    }

    public void TriggerAlien()
    {
        catman.GetComponentInChildren<CatMan>().isAlienDistracting = true;
        _scene.transform.GetChild(0).gameObject.SetActive(false);
        alienPopup.SetActive(true);
        _nextButton.SetActive(false);
        alienPopup.GetComponentInChildren<AlienManager>().isDistraction = false;
    }

    IEnumerator FancyText(int index)
    {
        _textRunning = true;
        if(!isGameOver) _scene.GetComponentInChildren<TextMeshProUGUI>().text += _sceneStrings[index].ElementAt(_stringIndex);
        else _scene.GetComponentInChildren<TextMeshProUGUI>().text += _gameOverStrings[index].ElementAt(_stringIndex);
        if (_stringIndex < _sceneStrings[index].Length - 1)
            _stringIndex++;
        else
        {
            _stringIndex = 0;
            _textRunning = false;
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(FancyText(index));
    }
}
