using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public float move_speed;
    public string enemyName;
    
    private void Update()
    {
        
    }

    public void enemy_move()
    {
        Debug.Log(enemyName + " move at speed " + move_speed);
    }

    public void enemy_attack()
    {

    }
}
