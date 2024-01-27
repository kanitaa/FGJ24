using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject canvas;
    public GameObject background;
    public GameObject overlay;
    public GameObject scene1, scene2, scene3, scene4, scene7, scene8, scene9, alienPopup;
    public GameObject form;
    private int state = 0;
    private GameObject activeOverlay;

    // Start is called before the first frame update
    void Start()
    {
        Next();   
    }


    public void Next()
    {
        if (this.state == 0) {
            //this.activeOverlay = Instantiate(scene1, overlay.transform.position, Quaternion.identity, overlay.transform);
            //this.activeOverlay.transform.SetParent(overlay.transform);
            //this.activeOverlay.transform.localScale += new Vector3(1960, 1080, 1);
            // Push example 1 to canvas
            scene1.SetActive(true);
            this.state = 1;
        }
        else if (this.state == 1) {
            //Destroy(this.activeOverlay);
            //this.activeOverlay = Instantiate(scene2, overlay.transform.position, Quaternion.identity, overlay.transform);
            //this.activeOverlay.transform.SetParent(overlay.transform);
            //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
            // Push example 2 to canvas
            scene1.SetActive(false);
            scene2.SetActive(true);
            this.state = 2;
        }
        else if (this.state == 2)
        {
            //Destroy(this.activeOverlay);
            //this.activeOverlay = Instantiate(scene3, overlay.transform.position, Quaternion.identity, overlay.transform);
            //this.activeOverlay.transform.SetParent(overlay.transform);
            //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
            // Push example 2 to canvas
            scene2.SetActive(false);
            scene3.SetActive(true);
            this.state = 3;
        }
        else if (this.state == 3)
        {
            //Destroy(this.activeOverlay);
            //this.activeOverlay = Instantiate(scene4, overlay.transform.position, Quaternion.identity, overlay.transform);
            //this.activeOverlay.transform.SetParent(overlay.transform);
            //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
            // Push example 2 to canvas
            scene3.SetActive(false);
            scene4.SetActive(true);
            this.state = 4;
        }
        else if (this.state == 4) {
            //Destroy(this.activeOverlay);
            form.SetActive(true);
            scene4.SetActive(false);
            GameObject nextButton = overlay.transform.GetChild(0).gameObject;
            nextButton.SetActive(false);
            //this.activeOverlay = Instantiate(form, overlay.transform.position, Quaternion.identity, overlay.transform);
            //this.activeOverlay.transform.SetParent(overlay.transform);
            //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
            // Push example 2 to canvas
            this.state = 5;
        }
        else if (this.state == 5)
        {
            //Title screen
            scene7.SetActive(true);
            GameObject nextButton = overlay.transform.GetChild(0).gameObject;
            nextButton.SetActive(true);
            this.state = 6;
        }
        else if (this.state == 6)
        {
            //Surgery room
            scene7.SetActive(false);
            scene8.SetActive(true);
            this.state = 7;
        }
        else if (this.state == 7)
        {
            
            scene8.SetActive(false);
            scene9.SetActive(true);
            this.state = 8;
        }
        else if (this.state == 8)
        {
            //Alien popup
            scene9.SetActive(false);
            alienPopup.SetActive(true);
            GameObject nextButton = overlay.transform.GetChild(0).gameObject;
            nextButton.SetActive(false);
            this.state = 9;
        }
      
    }
}
