using UnityEngine;

public class AnimatiorManager : MonoBehaviour
{
    InputManager inputManager;
    Animator animator;
    int horizontal;
    int vertical;
    float moveAnim;
    Combat combat;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        combat = GetComponent<Combat>();
    }
    private void Update()
    {
        

    }
   public void PlayAnimation()
    {
        if (inputManager.moverInput.x != 0 || inputManager.moverInput.y != 0)
        {

            if(moveAnim <1)
            {
                moveAnim += 1 * Time.deltaTime;
            } 
            else if (moveAnim > 0.5f && moveAnim <=2 )
            {
                if (inputManager.Spirits)
                moveAnim += 1 * Time.deltaTime;
                
            }
             if (moveAnim >1.1f && !inputManager.Spirits)
            {
                moveAnim -= 1 * Time.deltaTime;
            }
            
        }
        else if (inputManager.moverInput.x == 0 && inputManager.moverInput.y == 0)
        {

            if (moveAnim > 0)
            {
                moveAnim -= 1f * Time.deltaTime;
            }

        }
        moveAnim = Mathf.Clamp(moveAnim, 0, 2);

        animator.SetFloat("Speed", Mathf.Abs(moveAnim));
        if(!inputManager.isDodge)
        {
            animator.SetBool("Block", inputManager.isBlock);
        }
        else
        {
            animator.SetBool("Block", !inputManager.isDodge);
        }
        animator.SetBool("isDodge", inputManager.isDodge);
        FlameFire();
    }
    public void PlayDodgeAnim()
    {
        animator.SetTrigger("dodge");
        inputManager.isDodge = true;
        
    }
    public void PlayNorAttack()
    {
        
        animator.SetTrigger("isAttack");
    }
     void FlameFire()
    {
        animator.SetBool("flameFire", inputManager.isFlameFire);
    }
}
