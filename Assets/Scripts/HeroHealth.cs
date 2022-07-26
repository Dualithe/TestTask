using UnityEngine;
using UnityEngine.InputSystem;

public class HeroHealth : MonoBehaviour
{
    private HeartManager hm;

    public PlayerControls inputActions;
    public InputAction rmvHP;
    public InputAction addHP;
    public InputAction incHP;

    private void Start()
    {
        hm = GameManager.Get.HeartManager;
    }

    public void onHit(InputAction.CallbackContext context) //int value, 
    {
        hm.setMaxHp(8);
        hm.setCurrentHp(4);

        //hm.setMaxHp(-1);
        //hm.setCurrentHp(-1);

        //hm.setMaxHp(0);
        //hm.setCurrentHp(0);

        //hm.setMaxHp(4);
        //hm.setCurrentHp(-1);

        //hm.setMaxHp(-1);
        //hm.setCurrentHp(4);
    }

    public void onRegen(InputAction.CallbackContext context)//int value, 
    {
        hm.regenHeart(1);
    }
    public void onIncHP(InputAction.CallbackContext context)
    {
        hm.addMaxHeart(1);
    }

    public void animateDeath()
    {
        GetComponent<Animator>().Play("herochar_death");
    }

    public void die()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        addHP = inputActions.Player.addHP;
        addHP.Enable();
        addHP.performed += onRegen;

        rmvHP = inputActions.Player.rmvHP;
        rmvHP.Enable();
        rmvHP.performed += onHit;

        incHP = inputActions.Player.incHP;
        incHP.Enable();
        incHP.performed += onIncHP;
    }
    private void OnDisable()
    {
        addHP.Disable();
        rmvHP.Disable();

    }
}
