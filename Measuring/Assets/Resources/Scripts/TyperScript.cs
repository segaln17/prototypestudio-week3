using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TyperScript : MonoBehaviour
{
    public static TyperScript instance;
    public TextMeshProUGUI wordOutput = null;

    private string remainingword = string.Empty;
    private string currentWord = " ";

    private TaggingScript markController;

    public bool canType = false;

    public bool wordOutputOn = false;

    private void Awake()
    {
        markController = GetComponent<TaggingScript>();
        //wordOutput.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (canType)
        {
            SetCurrentWord();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canType)
        {
            CheckInput();   
        }
    }

    public void SetCurrentWord()
    {
        //get bank word
        currentWord = "mark";
        //pass it to remainingword
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingword = newString;
        wordOutput.text = remainingword;
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            //whenever a key is pressed
            string keysPressed = Input.inputString;
            //Inputstring contains all keys pressed in the frame
            if (keysPressed.Length ==1) //if only one key was pressed
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        //is it the right key?
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
            if (IsWordComplete())
            {
                SetCurrentWord();
                markController.markText.gameObject.SetActive(false);
                markController.journalText1.gameObject.SetActive(true);
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingword.IndexOf(letter) == 0;
        //is the index of the letter we're trying to check the first one?
        //if it's correct, remove it
    }

    private void RemoveLetter()
    {
        string newString = remainingword.Remove(0, 1); //removes the first character from the string
        SetRemainingWord(newString); //passes in the rest of the word
    }

    private bool IsWordComplete()
    {
        //what's the length of the remaining word?
        //if 0, then we've removed all the characters
        return remainingword.Length == 0;
    }
    
}
