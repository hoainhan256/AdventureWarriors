using UnityEngine;

public class blockSkill : MonoBehaviour
{
    public GameObject _shieldBlue;
    Combat combat;
   InputManager inputManager;
    private void Awake()
    {
        _shieldBlue = this.gameObject.transform.GetChild(0).gameObject;
        inputManager = GetComponent<InputManager>();
        combat = GetComponent<Combat>();
        _shieldBlue.SetActive(false);

    }
    public void BlockSkill()
    {
        if (!inputManager.isDodge && !combat.isAttack && !inputManager.isFlameFire)
        {
            if (inputManager.isBlock) _shieldBlue.SetActive(true);
            else _shieldBlue.SetActive(false);
        }
        else
        {
            _shieldBlue.SetActive(false);
        }
        
        
       
    }
}
