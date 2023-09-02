using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public InputActionAsset actions;

    public bool isGamePaused;
    public GameObject pauseMenuObj;

    public string menuSceneName = "Menu";

    private void Start()
    {
        // Check if some of the variables are not present
        Debug.Assert(actions, $"Actions not found in GamePause.. Check if it's provided to the script");

        // Pause action
        InputAction pauseAction = actions.FindAction("PauseGame");
        pauseAction.performed += ctx => ChangePauseState();
        pauseAction.Enable();
    }

    private void ChangePauseState()
    {
        Debug.Log("Pause action triggered");

        if (isGamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuObj.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Pause()
    {
        pauseMenuObj.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}
