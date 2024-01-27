using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterMovement : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] string letter = "";
    [SerializeField] FillTheForm _form;

    [SerializeField] Vector2 direction;

    Rigidbody2D _rb;
    void Start()
    {
       
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = letter;
       // GetComponent<Button>().onClick.AddListener(SubmitLetter);
       // _rb = GetComponent<Rigidbody2D>();
       // StartCoroutine(ChangeDirection());
    }
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;
        StartCoroutine(ChangeDirection());

    }
    IEnumerator ChangeDirection() {
        _rb.AddForce(direction * 5, ForceMode2D.Impulse);
        yield return new WaitForSeconds(3);
        direction = Random.insideUnitCircle.normalized;
        StartCoroutine(ChangeDirection());
    }
    void SubmitLetter()
    {
        _form.AddLetter(letter);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SubmitLetter();
    }
}
