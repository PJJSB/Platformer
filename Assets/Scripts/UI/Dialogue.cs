using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // Shows the array in the inspector to put the sentences in
    [TextArea(3, 10)]
    public string[] sentences;
}