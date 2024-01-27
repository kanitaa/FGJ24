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
    public GameObject scene1, scene2, scene3, scene4, scene7, scene8, scene9, alienPopup;
    public GameObject form;
    private int state = 0;
    private GameObject activeOverlay;


    [SerializeField] GameObject _scene;
    [SerializeField] List<Sprite> _sceneSprites;
    [TextArea(5,5)]
    [SerializeField] List<string> _sceneStrings;

    GameObject _nextButton;
    int _stringIndex = 0;
    bool _textRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        _nextButton = overlay.transform.GetChild(0).gameObject;
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
          
        _scene.GetComponentInChildren<TextMeshProUGUI>().text = "";

        if (state == 4) //Form
        {
            form.SetActive(true);
            _scene.SetActive(false);
            _nextButton.SetActive(false);
            _textRunning = false;
            _stringIndex = 0;
        }
        if (state == 5) //Title screen
        {
            _scene.SetActive(true);
            _nextButton.SetActive(true);
        }
        if (state == 8) //Alien popup
        {
            _scene.transform.GetChild(0).gameObject.SetActive(false);
            alienPopup.SetActive(true);
            _nextButton.SetActive(false);

            return;
        }
        _scene.GetComponentInChildren<Image>().sprite = _sceneSprites[state];
        StartCoroutine(FancyText(state));
        state++;
        

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
