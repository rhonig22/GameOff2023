using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _crossfade;
    private Animator _crossfadeAnimator;
    public static readonly int MainMenu = 1;
    public static readonly int GameOverMenu = 4;
    public static readonly int Leaderboard = 2;
    public static readonly int Credits = 5;
    public static readonly int Options = 3;
    public static readonly int StartLevel = 6;
    public static readonly int StartTimerLevel = 7;
    private static readonly float _waitTime = .25f;
    private bool _isLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.currentLevel = SceneManager.GetActiveScene().buildIndex;
        _crossfadeAnimator = _crossfade.GetComponent<Animator>();
        DataManager.gameOver.AddListener(GameOver);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GameOver();
        }
    }

    public void LoadNextLevel()
    {
        if (!_isLoading)
        {
            _isLoading= true;
            _crossfadeAnimator.SetBool("EndLevel", true);
            StartCoroutine(FinishLoading());
        }
    }

    IEnumerator FinishLoading()
    {
        yield return new WaitForSeconds(_waitTime);
        _isLoading= false;
        int nextLevel = StartTimerLevel;

        SceneManager.LoadScene(nextLevel);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(GameOverMenu);
    }
}
