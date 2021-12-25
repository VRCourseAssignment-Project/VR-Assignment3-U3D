using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField]
    private GameObject XRRig;

    private Vector3 XRRigPosition; // Rig position
    private Quaternion XRRigRotation; // Rig rotation


    bool ButtonPressed()
    {
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);


        foreach (var device in rightHandedControllers)
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

    public void Reset()
    {
        XRRig.transform.position = XRRigPosition; // set current positions to initial positions
        XRRig.transform.rotation = XRRigRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonPressed())
        {
            Reset();
        }
    }
}

