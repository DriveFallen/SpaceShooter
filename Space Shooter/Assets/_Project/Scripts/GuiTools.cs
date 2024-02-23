using UnityEngine;

public class GuiTools : MonoBehaviour
{
    [SerializeField] private GameObject pauseFrame;
    private bool _isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0;
            pauseFrame.SetActive(_isPaused);
        }
        else
        {
            Time.timeScale = 1;
            pauseFrame.SetActive(_isPaused);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}