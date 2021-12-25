using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    
    public Text countdownDisplay;
    public AudioSource audioSource;

    [SerializeField]
    private GameObject XRRig;

    private int countdownTime = 3;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        audioSource.Play();
        while (countdownTime > 0)
        {
            
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        var SwingingArmMotion = XRRig.GetComponent<SwingingArmMotion>();
        SwingingArmMotion.enabled = true;

        /* Call the code to "begin" your game here.
		 * For example, mine allows the player to start
		 * moving and starts the in game timer.
         */
        // GameController.instance.BeginGame();

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        canvas.gameObject.SetActive(false);
    }
}
