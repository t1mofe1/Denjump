using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private PlatformSpawner platformSpawnerScript;

    public int count;
    private Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<Text>();
        platformSpawnerScript = GameObject.Find("Platforms").GetComponent<PlatformSpawner>();

        // Check if some of the variables are not present
        Debug.Assert(platformSpawnerScript, $"PlatformSpawner not found in ScoreScript.. Check if object with name 'PlatformSpawner' exists");
        Debug.Assert(textComponent, $"Text component not found in ScoreScript.. Check if it's added to the object with 'ScoreScript' script");

        // Add event listener
        platformSpawnerScript.AddUserLandCallback((collision) => AddScore());
    }

    private void AddScore()
    {
        count++;
        textComponent.text = count.ToString();
    }
}