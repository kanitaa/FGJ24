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
    private int state = 0;
    private GameObject activeOverlay;


    [SerializeField] GameObject _scene, _titleScene;
    [SerializeField] List<Sprite> _sceneSprites;
    [TextArea(5,5)]
    [SerializeField] List<string> _sceneStrings;
    [SerializeField] GameObject _organs;
    [SerializeField] GameObject _nextButton;
    int _stringIndex = 0;
    bool _textRunning = false;
    // Start is called before the first frame update
    void Start()
    {
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
        state++;
        Debug.Log(state);


        if (state < 9)
        {
            _scene.GetComponentInChildren<TextMeshProUGUI>().text = "";
            _scene.GetComponentInChildren<Image>().sprite = _sceneSprites[state-1];
            StartCoroutine(FancyText(state-1));
        }
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
        if (state == 8)
        {
            Destroy(activeOverlay);
            _scene.SetActive(true);
            _organs.SetActive(true);
        }
        if (state == 9) //Alien popup
        {
            TriggerAlien();

            return;
        }
        if (state == 10)
        {
            activeOverlay = Instantiate(kidney, overlay.transform.position, Quaternion.identity, overlay.transform);
            activeOverlay.transform.SetParent(overlay.transform);
            activeOverlay.transform.localScale += new Vector3(1, 1, 1);
        }
        if (state == 11)
        {
            Destroy(activeOverlay);
            activeOverlay = Instantiate(robot, overlay.transform.position, Quaternion.identity, overlay.transform);
            activeOverlay.transform.SetParent(overlay.transform);
            activeOverlay.transform.localScale += new Vector3(1, 1, 1);
        }
        if (state == 12)
        {
            TriggerAlien();
        }
        if (state == 14)
        {
            TriggerAlien();
        }
        if (state == 16)
        {
            TriggerAlien();
        }
        if (state == 17)
        {
            Destroy(activeOverlay);
            catman.SetActive(true);
        }
        
        

        //if (this.state == 0) {
        //    //this.activeOverlay = Instantiate(scene1, overlay.transform.position, Quaternion.identity, overlay.transform);
        //    //this.activeOverlay.transform.SetParent(overlay.transform);
        //    //this.activeOverlay.transform.localScale += new Vector3(1960, 1080, 1);
        //    // Push example 1 to canvas
        //  //  scene1.SetActive(true);
        //    _scene.GetComponentInChildren<Image>().sprite = _sceneSprites[state];
        //    _scene.GetComponentInChildren<TextMeshProUGUI>().text = _sceneStrings[state];
        //    this.state = 1;
        //}
        //else if (this.state == 1) {
        //    //Destroy(this.activeOverlay);
        //    //this.activeOverlay = Instantiate(scene2, overlay.transform.position, Quaternion.identity, overlay.transform);
        //    //this.activeOverlay.transform.SetParent(overlay.transform);
        //    //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
        //    // Push example 2 to canvas
        //  //  scene1.SetActive(false);
        //   // scene2.SetActive(true);

        //    _scene.GetComponentInChildren<Image>().sprite = _sceneSprites[state];
        //    _scene.GetComponentInChildren<TextMeshProUGUI>().text = _sceneStrings[state];
        //    this.state = 2;
        //}
        //else if (this.state == 2)
        //{
        //    //Destroy(this.activeOverlay);
        //    //this.activeOverlay = Instantiate(scene3, overlay.transform.position, Quaternion.identity, overlay.transform);
        //    //this.activeOverlay.transform.SetParent(overlay.transform);
        //    //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
        //    // Push example 2 to canvas
        //    //  scene2.SetActive(false);
        //    // scene3.SetActive(true);
        //   // _scene.GetComponentInChildren<Image>().sprite = _sceneSprites[state];
        //   // _scene.GetComponentInChildren<TextMeshProUGUI>().text = _sceneStrings[state];
        //    this.state = 3;
        //}
        //else if (this.state == 3)
        //{
        //    //Destroy(this.activeOverlay);
        //    //this.activeOverlay = Instantiate(scene4, overlay.transform.position, Quaternion.identity, overlay.transform);
        //    //this.activeOverlay.transform.SetParent(overlay.transform);
        //    //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
        //    // Push example 2 to canvas
        //    scene3.SetActive(false);
        //    scene4.SetActive(true);
        //    this.state = 4;
        //}
        //else if (this.state == 4) {
        //    //Destroy(this.activeOverlay);
        //    form.SetActive(true);
        //    scene4.SetActive(false);
        //    GameObject nextButton = overlay.transform.GetChild(0).gameObject;
        //    nextButton.SetActive(false);
        //    //this.activeOverlay = Instantiate(form, overlay.transform.position, Quaternion.identity, overlay.transform);
        //    //this.activeOverlay.transform.SetParent(overlay.transform);
        //    //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
        //    // Push example 2 to canvas
        //    this.state = 5;
        //}
        //else if (this.state == 5)
        //{
        //    //Title screen
        //    scene7.SetActive(true);
        //    GameObject nextButton = overlay.transform.GetChild(0).gameObject;
        //    nextButton.SetActive(true);
        //    this.state = 6;
        //}
        //else if (this.state == 6)
        //{
        //    //Surgery room
        //    scene7.SetActive(false);
        //    scene8.SetActive(true);
        //    this.state = 7;
        //}
        //else if (this.state == 7)
        //{

        //    scene8.SetActive(false);
        //    scene9.SetActive(true);
        //    this.state = 8;
        //}
        //else if (this.state == 8)
        //{
        //    //Alien popup
        //    scene9.SetActive(false);
        //    alienPopup.SetActive(true);
        //    GameObject nextButton = overlay.transform.GetChild(0).gameObject;
        //    nextButton.SetActive(false);
        //    this.state = 9;
        //}

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
        _scene.transform.GetChild(0).gameObject.SetActive(false);
        alienPopup.SetActive(true);
        _nextButton.SetActive(false);
        alienPopup.GetComponentInChildren<AlienManager>().isDistraction = false;
    }

    IEnumerator FancyText(int index)
    {
        _textRunning = true;
        _scene.GetComponentInChildren<TextMeshProUGUI>().text += _sceneStrings[index].ElementAt(_stringIndex);
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
