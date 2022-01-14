using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextEventHandler : MonoBehaviour
{
    public TextEvent start;
    public TextEvent end;
    public TextEvent current;
    public textInput input;
    public typeText target;
    public Image img1;
    public Image img2;
    void Start()
    {
        current = start;
        setText(start);
    }
    private void Update()
    {
        img1.GetComponent<RectTransform>().localPosition = Vector2.MoveTowards(img1.GetComponent<RectTransform>().localPosition, new Vector2(), Time.deltaTime * 400);
    }
    public void pickOption()
    {   
        foreach (TextEvent i in current.textEvents)
        {
            if (i.Action.ToLower() == input.input.ToLower())
            {
                current = i;
                input.input = "";
                break;
            }
        }
        if (current == end)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        setText(current);
        
    }
    public void setText(TextEvent text)
    {
        if (text.changeBG)
        {
            img2.GetComponent<Image>().sprite = img1.GetComponent<Image>().sprite;
            img1.GetComponent<Image>().sprite = text.changeBG;
            img1.GetComponent<RectTransform>().localPosition = new Vector2(0, 974);
        }
        target.targetText = text.Description+ "\n\n";
        foreach (TextEvent t in text.textEvents)
        {
            target.targetText += "\t>"+t.Action + "\n";
        }
    }
}
