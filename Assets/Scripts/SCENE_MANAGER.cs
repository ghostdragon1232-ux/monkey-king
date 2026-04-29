using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class SCENE_MANAGER : MonoBehaviour
{
    // Public function to be called by your "Start" button
    public void StartGame()
    {
        // Replace "ROCKETS UI" with the exact name of your game scene
        SceneManager.LoadScene("map"); 
    }

    // Optional: Quit the game
    public void QuitGame()
    {
        
        Application.Quit();
        Debug.Log("Game is exiting");
         

    }
}
