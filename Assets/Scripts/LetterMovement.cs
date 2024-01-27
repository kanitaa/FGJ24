using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterMovement : MonoBehaviour
{
    [SerializeField] string letter = "";
    [SerializeField] FillTheForm _form;
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = letter;
        GetComponent<Button>().onClick.AddListener(SubmitLetter);
    }

   void SubmitLetter()
    {
        _form.AddLetter(letter);
    }
}
