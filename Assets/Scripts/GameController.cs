using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int playerScore;
    public int health=100;
    public Text scoreText;
    public Text healthText;

    public Transform playerSpawnPoint;
    public GameObject playerPrefab;

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd){
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    void Start (){
        if (GameObject.FindWithTag("Player") == null)
        {
            // Respawn the player
            Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        }
    }
    

    public void updateHealth(int amount){
        health = health - amount;
        healthText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
