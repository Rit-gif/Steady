using UnityEngine;

public class player_animation_events : MonoBehaviour
{
    private Player_Script player;

    private void Awake()
    {
        player = GetComponentInParent<Player_Script>();
    }

    public void damage_enemies() => player.damage_enemies();
    private void disable_jump_movement() => player.enable_jump_movement(false);
    private void enable_jump_movement() => player.enable_jump_movement(true);


}
