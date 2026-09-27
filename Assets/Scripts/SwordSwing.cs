using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            if (!state.IsName("SwordSwing") || state.normalizedTime >= 1f)
            {
                animator.Play("SwordSwing", 0, 0f);
            }
        }
    }
}