using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningEnemyBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("score") && PlayerPrefs.GetInt("score") >= 100)
        {
            GetComponent<TextMeshProUGUI>().text = "WARNING: ENEMY 1 APPROACHING";
        }

        // if (PlayerPrefs.HasKey("score") && PlayerPrefs.GetInt("score") >= 100)
        // {
        //     GetComponent<TextMeshProUGUI>().text = "WARNING: ENEMY 1 APPROACHING";
        // }

        // if (PlayerPrefs.HasKey("score") && PlayerPrefs.GetInt("score") >= 300)
        // {
        //     GetComponent<TextMeshProUGUI>().text = "WARNING: ENEMY 2 APPROACHING";
        // }

        // if (PlayerPrefs.HasKey("score") && PlayerPrefs.GetInt("score") >= 300)
        // {
        //     GetComponent<TextMeshProUGUI>().text = "WARNING: ALL ENEMIES APPROACHING";
        // }
    }
}
