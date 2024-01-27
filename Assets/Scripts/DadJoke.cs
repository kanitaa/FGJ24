using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DadJoke", menuName = "ScriptableObjects/DadJoke", order = 1)]
public class DadJoke : ScriptableObject
{
    public string punchLine;
    public List <string> option;
    public int answerId;
}
