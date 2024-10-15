using System.Collections;
using System.Collections.Generic;
using TMPro; // Đảm bảo bạn sử dụng thư viện TMP cho TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startText;   // Tham chiếu tới Text bắt đầu
    public GameObject gameOverText; // Tham chiếu tới Text game over
    public TextMeshProUGUI gameScore; // Tham chiếu tới TMP UI cho điểm số
    public Food food;  // Đối tượng Food để cập nhật điểm khi rắn ăn thức ăn
    private bool isGameStarted = false;
    private bool isGameOver = false;
    private int score = 0; // Biến theo dõi điểm số
    public  float speedSnake = 1f;
    public AudioSource audio_source;
    public AudioSource vfx;
    public AudioClip bg;
    public AudioClip getFood;

    void Start()
    {
        // Hiển thị Text bắt đầu
        startText.SetActive(true);
        gameOverText.SetActive(false);
        gameScore.text = "Score: 0"; // Khởi tạo điểm số
        Time.timeScale = 0f; // Dừng game cho đến khi bắt đầu
    }

    void Update()
    {
        // Kiểm tra nếu trò chơi chưa bắt đầu
        if (!isGameStarted && Input.anyKeyDown)
        {
            StartGame();
        }

        // Cập nhật điểm số khi trò chơi đang diễn ra
        if (isGameStarted)
        {
            // Giả sử mỗi lần rắn ăn thức ăn thì food.score sẽ tăng
            UpdateScore(food.score); // Hàm để cập nhật UI điểm số
            UpdateLevel(food.score);
            if (!audio_source.isPlaying)
            {
                audio_source.clip = bg;
                audio_source.loop = true;  // Đảm bảo nhạc nền lặp lại
                audio_source.Play();
            }
        }

        // Kiểm tra trò chơi kết thúc và người chơi muốn chơi lại
        if (isGameOver )
        {
            audio_source.loop=false;
            audio_source.Stop();
            if(Input.GetKeyDown(KeyCode.R))
            RestartGame();
        }
    }

    public void EndGame()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
        Time.timeScale = 0f; // Dừng game khi kết thúc
    }

    void StartGame()
    {
        isGameStarted = true;
        startText.SetActive(false);

        Time.timeScale = 1f; // Bắt đầu game
    }

    void RestartGame()
    {
        isGameOver = false;
        gameOverText.SetActive(false);
        Time.timeScale = 1f;
        // Tải lại scene hiện tại để khởi động lại game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    // Hàm cập nhật điểm số và hiển thị trên UI
    void UpdateScore(int newScore)
    {
        score = newScore;  // Cập nhật biến điểm
        gameScore.text = "Score: " + score.ToString(); // Hiển thị điểm số
    }
    void UpdateLevel(float score)
    {
        if (score == 0) speedSnake = 1f;
        else speedSnake = 1.0f/score ;
       Time.fixedDeltaTime = speedSnake;
    }
}
