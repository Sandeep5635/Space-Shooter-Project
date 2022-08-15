using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _LivesImage;

    [SerializeField]
    private Sprite[] _livesSprite;

    [SerializeField]

    private Text _GameOverText;

    [SerializeField]
    private Text _RestartText;

    Player player;
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 50;
        player = GameObject.Find("Player").GetComponent<Player>();
       _GameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is Null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            _scoreText.text = "Score: "+ player.GetScore();
        }
       // _GameOverText.text = " ";

      //  _GameOverText.text = "GAME OVER ";
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _livesSprite[currentLives];

        if (currentLives < 1)
        {
            //  _GameOverText.enabled = true;

            GameOverLevel();
        }

    }

    public void GameOverLevel()
    {
        _GameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _RestartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _GameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _GameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
