using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    private GameObject camera;
    private GameObject leftHand;
    private GameObject rightHand;
    private GameObject XRRig;
    private GameObject head;

    private bool isJumping = false;
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    // vector from camera point to player
    // camera -> player
    private Vector3 vectorCameraPlayer;

    public Transform lookAt;
    public Transform camTransform;
    public float camearInitialPositionY;
    public float yOffsetDistance = -1.7f;
    public float zOffsetDistance = -0.2f;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    private float sensitivityX = 20.0f;
    private float sensitivityY = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        XRRig = GameObject.FindGameObjectWithTag("XRRig");
        leftHand = GameObject.FindGameObjectWithTag("LeftHand");
        rightHand = GameObject.FindGameObjectWithTag("RightHand");

        head = GameObject.FindGameObjectWithTag("Head");

        camearInitialPositionY = camera.transform.position.y;
        StartCoroutine(ResetCameraYAxis());
        
    }

    public void Jump()
    {
        // when left or right hand higher than head/camera
        if (camera.transform.position.y < leftHand.transform.position.y && camera.transform.position.y < rightHand.transform.position.y)
        {
            XRRig.transform.position = new Vector3(XRRig.transform.position.x,
            XRRig.transform.position.y + 0.01f,
            XRRig.transform.position.z);
        }
    }

    // after 5 seconds reset the y axis
    IEnumerator ResetCameraYAxis()
    {
        for (; ; )
        {
            XRRig.transform.position = new Vector3(XRRig.transform.position.x,
                0.7f,
                XRRig.transform.position.z);
            yield return new WaitForSeconds(5.0f);
        }
    }

    

        // Update is called once per frame
    void Update()
    {
        Jump();
        // this.transform.rotation = camera.transform.rotation;
        if (photonView.IsMine)
        {
            this.transform.position = new Vector3(camera.transform.position.x,
                camera.transform.position.y + yOffsetDistance,
                camera.transform.position.z + zOffsetDistance);
        }
    }
    
}
