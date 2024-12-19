using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimingScript : MonoBehaviour
{
    public float idealTime = 10f;
    public TextMeshProUGUI idealTimeText;

    public float timeGone;
    public float timeWasted;
    public TextMeshProUGUI timeWastedText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        idealTimeText.text = "Ideal Time to Reach Observatory: " + idealTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeGone += Time.deltaTime;

        if (timeGone >= idealTime)
        {
            timeWasted += Time.deltaTime;
            timeWastedText.text = "Time Wasted:" + " " + timeWasted;
        }
    }
}
