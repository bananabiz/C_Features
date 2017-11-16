using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Breakout
{
public class ScoreManager : MonoBehaviour
    {
        public int score, blockLeft;
        public Text scoreText;
        //public GameManager gameManagerScript;

        // Use this for initialization
        void Start()
        {
            scoreText = this.GetComponent<Text>();
            //gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
            //int blockLeft = gameManagerScript.total;
        }

	    void Update ()
        {
            if (score < 10)
            {
                blockLeft = 150 - score;
                scoreText.text = "Score: " + score.ToString() + "   (^0^)   " + blockLeft.ToString() + " blocks left!";
            }
            else if (score == 10)
            {
                scoreText.text = "STAGE CLEAR!";
                Time.timeScale = 0;
                //yield return new WaitForSeconds(3);
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    SceneManager.LoadScene(1);
                }
            }
	    }
    }
}
