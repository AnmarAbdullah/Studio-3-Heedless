using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsGame : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 12f;
    [SerializeField]
    // private string scaneNameToLoad;

    private float timeElapsed;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            delayBeforeLoading += 12;
        }

        if (timeElapsed > delayBeforeLoading || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
