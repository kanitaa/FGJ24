using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject canvas;
    public GameObject background;
    public GameObject overlay;
    public GameObject example1;
    public GameObject example2;
    public GameObject form;
    private int state = 0;
    private GameObject activeOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        if (this.state == 0) {
            this.activeOverlay = Instantiate(example1, overlay.transform.position, Quaternion.identity, overlay.transform);
            this.activeOverlay.transform.SetParent(overlay.transform);
            this.activeOverlay.transform.localScale += new Vector3(1960, 1080, 1);
            // Push example 1 to canvas
            this.state = 1;
        }
        else if (this.state == 1) {
            Destroy(this.activeOverlay);
            this.activeOverlay = Instantiate(example2, overlay.transform.position, Quaternion.identity, overlay.transform);
            this.activeOverlay.transform.SetParent(overlay.transform);
            this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
            // Push example 2 to canvas
            this.state = 2;
        }
        else if (this.state == 2) {
            Destroy(this.activeOverlay);
            form.SetActive(true);
            GameObject nextButton = overlay.transform.GetChild(0).gameObject;
            nextButton.SetActive(false);
            //this.activeOverlay = Instantiate(form, overlay.transform.position, Quaternion.identity, overlay.transform);
            //this.activeOverlay.transform.SetParent(overlay.transform);
            //this.activeOverlay.transform.localScale += new Vector3(1, 1, 1);
            // Push example 2 to canvas
            this.state = 3;
        }
        else if (this.state == 3)
        {
            //Surgery room
            Destroy(this.activeOverlay);
            
            this.state = 4;
        }
    }
}
