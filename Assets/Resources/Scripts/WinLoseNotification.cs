using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinLoseNotification : MonoBehaviour
{

    [SerializeField]
    private GameObject XRRig;

    private Vector3 XRRigPosition; // Rig position
    private Quaternion XRRigRotation; // Rig rotation

    public ResetPosition reset;
    public Canvas CanvasFinish;
    // Start is called before the first frame update
    void Start()
    {
    }

    bool Finish()
    {
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Finish()) ;
          //Text result = UnityEngine.UI.Text.;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if(collision.transform.tag == "Obstacles")
        {
            XRRig.transform.position = XRRigPosition; // set current positions to initial positions
            XRRig.transform.rotation = XRRigRotation;
        }
        if (collision.transform.tag == "FinalLine")
        {
            // final line arrived;
            Debug.Log("Finish line");
        }
        // CanvasFinish.gameObject.SetActive(true);
        // Debug.Log("Finish line");
    }
}
