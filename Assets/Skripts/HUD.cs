using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text score;
    bool gameIsRun = true;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsRun)
        {
            timer += Time.deltaTime;
            score.text = ((int)timer).ToString();
        }  
    }
    public void StopGameTimer()
    {
        gameIsRun = false;
    }
}
