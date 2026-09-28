using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public float move_speed;
    public string enemyName;
    
    private void Update()
    {
        enemy_move();
        if (Input.GetKeyDown(KeyCode.F)) enemy_attack();
    }

    public void enemy_move()
    {
        
    }

    public void take_damage()
    {
        
    }

    public virtual void enemy_attack()
    {
        Debug.Log(enemyName + " attack");
    }
}
