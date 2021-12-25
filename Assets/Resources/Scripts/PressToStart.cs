using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class PressToStart : MonoBehaviour
{
    [SerializeField]
    private GameObject XRRig;

    private Vector3 XRRigPosition; // Rig position
    private Quaternion XRRigRotation; // Rig rotation

    

    bool Check()
    {
        //myBall = new CreateBalls();


        
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);


        foreach (var device in leftHandedControllers)
        {
            bool triggerValue = true;

            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Debug.Log("button is pressed");
                return true;
            }
        }

        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        XRRigPosition = XRRig.transform.position; // record initial positions
        XRRigRotation = XRRig.transform.rotation;

    }

    //Update is called once per frame
    void Update()
    {
        if (XRRig.transform.position == XRRigPosition) // starting countdown should work only on starting position, also after reset (does not work yet)
        {
            if (Check())
            {
                var CountdownController = XRRig.GetComponent<CountdownController>();
                CountdownController.enabled = true;

            }
        }
            
    }

   

}

/* 
   // Update is called once per frame
    void FixedUpdate()
    {
        if (Check())
        {
            audioSource.Play();
            DisplayTie(3);
            
            GameObject canvas;
            canvas = GameObject.Find("Canvas");
            var textGameObject = canvas.transform.GetChild(1);
            var txt = textGameObject.GetComponent<Text>();
           
            timeText.text = txt.text;
            
//while (timeRemaining > 0)
{
    
    //txt1.text = timeRemaining.ToString();
     DisplayTime(3);
     timeRemaining -= Time.deltaTime;
     string ret = timeRemaining.ToString;
     txt.text = timeRemaining.ToString;
    

}
            //txt1.text = "GO!";
        }
             
            
            //gameObject.SendMessage("sdsd");
    }

    void DisplayTime(float timeToDisplay)
{
    float minutes = Mathf.FloorToInt(timeToDisplay / 60);
    float seconds = Mathf.FloorToInt(timeToDisplay % 60);
    float milliSeconds = (timeToDisplay % 1) * 1000;
    GameObject canvas;
    canvas = GameObject.Find("Canvas");
    var textGameObject = canvas.transform.GetChild(1);
    var txt = textGameObject.GetComponent<Text>();
    txt.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    //timeText.text = txt.text;
}

*/