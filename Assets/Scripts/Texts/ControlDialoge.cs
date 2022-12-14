using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlDialoge : MonoBehaviour
{
    private Queue<string> dialoges;
    Texts text;
    public TextMeshProUGUI textInScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveDialoge(Texts textObject)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        text = textObject;
    }

    public void ActiveText()
    {
        dialoges.Clear();
        foreach (string saveText in text.arrayTexts)
        {
            dialoges.Enqueue(saveText);
        }
        NextLine();
    }

    public void NextLine()
    {
        if (dialoges.Count == 0)
        {
            CloseDialoge();
            return;
        }
        string actualLine = dialoges.Dequeue();
        textInScreen.text = actualLine;
        StartCoroutine(ShowCharacters(actualLine));
    }

    public void CloseDialoge()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator ShowCharacters(string text)
    {
        textInScreen.text = "";
        foreach(char character in text.ToCharArray())
        {
            textInScreen.text += character;
            yield return new WaitForSeconds(0.02f);
        }
    }
}