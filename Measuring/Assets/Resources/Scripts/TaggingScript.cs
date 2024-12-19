using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TaggingScript : MonoBehaviour
{
    //TAGGING SCRIPT STUFF
    //public static TaggingScript instance;
    
    private GraphicRaycaster m_Raycaster;

    private PointerEventData m_PointerEventData;

    private EventSystem m_EventSystem;

    public Image selector;

    public TextMeshProUGUI markText;

    public TextMeshProUGUI journalText1;
    public TextMeshProUGUI journalText2;
    public TextMeshProUGUI journalText3;
    public TextMeshProUGUI journalText4;
    public TextMeshProUGUI journalText5;
    public TextMeshProUGUI journalText6;

    public Animator journalText1Anim;
    public Animator journalText2Anim;
    public Animator JournalText3Anim;
    public Animator journalText4Anim;
    public Animator journalText5Anim;
    public Animator journalText6Anim;

    public TextMeshProUGUI outputText = null;

    //private TyperScript typerController;
    
    //TYPERSCRIPT STUFF:
    private string remainingWord = string.Empty;
    private string currentWord = "mark";
    
    public bool canType = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        selector.gameObject.SetActive(false);
        markText.gameObject.SetActive(false);
        outputText.gameObject.SetActive(false);
        journalText1.gameObject.SetActive(false);
        journalText2.gameObject.SetActive(false);
        journalText3.gameObject.SetActive(false);
        journalText4.gameObject.SetActive(false);
        journalText5.gameObject.SetActive(false);
        journalText6.gameObject.SetActive(false);
        
        /*
        if (canType)
        {
            SetCurrentWord();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {

            m_PointerEventData = new PointerEventData(m_EventSystem);
            //set Pointer Event position to the mouse position:
            m_PointerEventData.position = Input.mousePosition;
            
            //list for Raycast results:
            List<RaycastResult> results = new List<RaycastResult>();
            //raycast using the Graphics Raycaster and mouse click pos:
            m_Raycaster.Raycast(m_PointerEventData, results);


            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name == "Target")
                {
                    selector.gameObject.SetActive(true);
                    Mark();
                    //markText.gameObject.SetActive(true);
                    Debug.Log("got em");
                }
                //Debug.Log("Hit " + result.gameObject.name);
            }
        }

        if (canType)
        {
            CheckInput();
        }
    }

    public void Mark()
    {
        Marked();
        //set mark instructions on
        markText.gameObject.SetActive(true);
        Debug.Log("mark");
        //turn mark input text on
        SetCurrentWord();
        outputText.gameObject.SetActive(true);
        //Coroutine to wait a few seconds and then turn off the selector:
        StartCoroutine(WaitDeselect());
    }

    IEnumerator WaitDeselect()
    {
        yield return new WaitForSeconds(4f);
        selector.gameObject.SetActive(false);
    }

    public bool Marked()
    {
        canType = true;
        return true;
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
        remainingWord = newString;
        outputText.text = remainingWord;
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
                markText.gameObject.SetActive(false);
                outputText.gameObject.SetActive(false);
                //MAKE THIS ANIMATIONS INSTEAD
                journalText1.gameObject.SetActive(true);
                StartCoroutine(WaitAnimate());
            }
        }
    }
    

    IEnumerator WaitAnimate()
    {
        yield return new WaitForSeconds(5f);
        journalText1Anim.Play("journal1anim");
        journalText2.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        journalText2Anim.Play("journal2anim");
        journalText3.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        JournalText3Anim.Play("journal3anim");
        journalText4.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        journalText4Anim.Play("journal4anim");
        journalText5.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        journalText5Anim.Play("journal5anim");
        journalText6.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        journalText6Anim.Play("journal6anim");

    }
    
    
    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
        //is the index of the letter we're trying to check the first one?
        //if it's correct, remove it
    }
    
    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1); //removes the first character from the string
        SetRemainingWord(newString); //passes in the rest of the word
    }
    
    private bool IsWordComplete()
    {
        //what's the length of the remaining word?
        //if 0, then we've removed all the characters
        return remainingWord.Length == 0;
    }
    
}
