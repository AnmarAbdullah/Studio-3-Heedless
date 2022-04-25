using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsGame : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 40f;
    [SerializeField]
    // private string scaneNameToLoad;

    private float timeElapsed;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            delayBeforeLoading += 40;
        }

        if (timeElapsed > delayBeforeLoading || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
