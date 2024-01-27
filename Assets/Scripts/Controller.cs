using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject canvas;
    public GameObject background;
    public GameObject overlay;
    public GameObject example1;
    public GameObject example2;
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
            this.activeOverlay.transform.SetParent(overlay.transform, false);
            // Push example 1 to canvas
            this.state = 1;
        }
        else if (this.state == 1) {
            Instantiate(example1, overlay.transform.position, Quaternion.identity, overlay.transform);
            Destroy(this.activeOverlay);
            this.activeOverlay = Instantiate(example2, overlay.transform.position, Quaternion.identity, overlay.transform);
            // Push example 2 to canvas
            this.state = 2;
        }
    }
}
