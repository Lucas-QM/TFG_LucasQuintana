using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlDialoge : MonoBehaviour
{
    private Queue<string> dialoges;
    Texts text;
    public TextMeshProUGUI textInScreen;
    public GameObject activator;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        dialoges = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveDialoge(Texts textObject)
    {
        player.GetComponent<PlayerController>().isTalking = true;
        activator.SetActive(true);
        text = textObject;
        ActiveText();
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
        player.GetComponent<PlayerController>().isTalking = false;
        activator.SetActive(false);
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