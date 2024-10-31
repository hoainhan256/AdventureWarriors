using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool _startGame = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Invoke(nameof(StartGame), 180f);
    }
    public void StartGame()
    {
        _startGame = true;
    }
    private void Update()
    {
        
    }
}
