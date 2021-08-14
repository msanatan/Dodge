using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = string.Format("{0}: {1}", scoreText.text, PlayerStats.Score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
