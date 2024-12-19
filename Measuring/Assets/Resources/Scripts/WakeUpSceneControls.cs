using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WakeUpSceneControls : MonoBehaviour
{
    public Button wakeUpButton;

    public Button tallyButton;

    public Button crossOutButton;

    public Button ventureButton;

    public GameObject crossOutAnimatorHolder;
    public GameObject tallyAnimatorHolder;

    public CinemachineClearShot asleepCamera;
    public CinemachineVirtualCamera awakeCamera;

    public Animator crossOutAnimator;
    public Animator tallyAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        crossOutAnimator = crossOutAnimatorHolder.GetComponent<Animator>();
        tallyAnimator = tallyAnimatorHolder.GetComponent<Animator>();
        
        //set buttons for later to not active for now
        tallyButton.gameObject.SetActive(false);
        crossOutButton.gameObject.SetActive(false);
        ventureButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WakeUp()
    {
        wakeUpButton.gameObject.SetActive(false);
        tallyButton.gameObject.SetActive(true);
        asleepCamera.Priority = 5;
        awakeCamera.Priority = 10;
    }

    public void TallyDays()
    {
        tallyAnimator.SetTrigger("Active");
        tallyButton.gameObject.SetActive(false);
        crossOutButton.gameObject.SetActive(true);
    }

    public void CrossOutDay()
    {
        crossOutAnimator.SetTrigger("Active");
        ventureButton.gameObject.SetActive(true);
    }

    public void Venture()
    {
        SceneManager.LoadScene("VentureScene");
    }
}
