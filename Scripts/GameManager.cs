using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    [SerializeField]
    int score = 0;
    int wave = 1;
    [SerializeField]
    int maxEnemy = 50;
    [SerializeField]
    int spawnEnemy = 0;
    [SerializeField]
    int currentEnemy = 0;

    bool isGameStart = false;
    bool isGameOver = false;
    bool isWaveStart = false;

    public bool START { get { return isGameStart; } }
    public bool END { get { return isGameOver; } }
    public bool WAVESTART { get { return isWaveStart; } }
    public int MAXENEMY { get { return maxEnemy; } }
    public int SPAWENEMY { get { return spawnEnemy; } }

    Text scoreText;
    Text waveText;

    GameObject startUI;
    GameObject overUI;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        startUI = GameObject.Find("GameStartUI");
        overUI = GameObject.Find("GameOverUI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ExitGame();
    }

    public void GameStart()
    {
        isGameStart = true;
        startUI.SetActive(false);
        WaveStart();
    }

    public void GameOver()
    {
        isGameOver = true;
        overUI.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void AddScore(int _score)
    {
        score += _score;
        scoreText.text = "SCORE:" + score.ToString();
        RemoveEnemy();
    }
    public void AddEnemy()
    {
        spawnEnemy++;
        currentEnemy++;
    }

    void RemoveEnemy()
    {
        currentEnemy--;
        if(currentEnemy == 0 && maxEnemy == spawnEnemy)
        {
            isWaveStart = false;
            wave++;
            spawnEnemy = 0;
            maxEnemy += 10;
            Invoke("WaveStart", 2);
        }
    }

    void WaveStart()
    {
        StartCoroutine(WaveStartcor()); 
    }

    IEnumerator WaveStartcor()
    {
        waveText.enabled = true;
        waveText.text = "WAVE" + wave.ToString();
        yield return new WaitForSeconds(3);
        waveText.enabled = false;
        isWaveStart = true;
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
