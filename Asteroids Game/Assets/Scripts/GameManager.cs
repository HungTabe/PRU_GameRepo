using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int lives = 3;
    public float respawnTime = 3.0f;

    public void PlayerDied()
    {
        this.lives--;
        
        if (this.lives <= 0)
        {
            GameOver();
        } else { 
        
        Invoke(nameof(Respawn), this.respawnTime);

        }

    }

    private void Respawn()
    {
        // // Đặt lại vị trí của người chơi về (0, 0, 0)
        this.player.transform.position = Vector3.zero;
        // // Kích hoạt lại đối tượng người chơi
        this.player.gameObject.SetActive(true);

    }

    private void GameOver()
    {

    }
}
