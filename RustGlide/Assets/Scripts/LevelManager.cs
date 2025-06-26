using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private int currentLevelIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        // Even
        if (LevelIndexCheck() == 0)
        {
            SceneManager.LoadScene(2);  // Level 2
        }
        // Odd
        if (LevelIndexCheck() == 1)
        {
            SceneManager.LoadScene(1);  // Level 1
        }
    }

    private int LevelIndexCheck()
    {
        return currentLevelIndex % 2;
    }

    // When all waves for level done:
    // 1. Load next level 
    //      a. Call function to go to next level x
    //      b. Add +1 to current level index x
    //      c. Load current level index x
    //      d. Load same 2 levels x
    //
    // 2. Increase enemy spawn amount/locations
    // 3. Add waves if needed
    // 4. Increase stamina decrease if current level index is even or odd %


}
