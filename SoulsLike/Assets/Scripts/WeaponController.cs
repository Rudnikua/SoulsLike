using Mono.Cecil.Cil;
using StarterAssets;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour {

    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private const string DRAW_SHEATH_WEAPON = "DrawWeapon";
    private const string ATTACK = "Attack";

    private bool canAttack = false;
    private bool isDrawn = false;
    private bool inAction = false;

    private void Awake() {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();    
    }

    private void Update() {
        DrawOrSheathWeapon();
        OnAttack();

        starterAssetsInputs.draw = false;
        starterAssetsInputs.attack = false;
    }

    private void DrawOrSheathWeapon() {
        if (inAction) {
            return;
        }
        if (starterAssetsInputs.draw) {
            if (!isDrawn) {
                animator.SetBool(DRAW_SHEATH_WEAPON, true);
                isDrawn = true;
                inAction = false;
            } else {
                animator.SetBool(DRAW_SHEATH_WEAPON, true);
                isDrawn = false;
                inAction = true;
                canAttack = false;
            }
        }
    }


    private void OnAttack() {
        if (starterAssetsInputs.attack && canAttack) {
            animator.SetBool(ATTACK, true);
        }
    }

    private void OnDrawFinished() {
        animator.SetBool(DRAW_SHEATH_WEAPON, false);
        inAction = false;
        canAttack = true;
    }
    private void OnSheathFinished() {
        animator.SetBool(DRAW_SHEATH_WEAPON, false);
        inAction = false;
    }

    private void OnAttackFinished() {
        starterAssetsInputs.attack = false;
        animator.SetBool(ATTACK, false);
    }
}
