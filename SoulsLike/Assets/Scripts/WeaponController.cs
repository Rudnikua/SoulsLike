using Mono.Cecil.Cil;
using StarterAssets;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour {

    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private const string DRAW_WEAPON = "DrawWeapon";
    private const string SHEATH_WEAPON = "SheathWeapon";

    private bool isDrawn = false;
    private bool inAction = false;

    private void Awake() {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();    
    }

    private void Update() {
        DrawOrSheathWeapon();

        starterAssetsInputs.draw = false;
        starterAssetsInputs.sheath = false;
    }
    private void DrawOrSheathWeapon() {
        if (inAction) {
            return;
        }
        if (starterAssetsInputs.draw && !isDrawn) {
            animator.SetBool(DRAW_WEAPON, true);
            isDrawn = true;
            inAction = true;
        }
        if (starterAssetsInputs.sheath && isDrawn) {
            animator.SetBool (SHEATH_WEAPON, true);
            isDrawn = false;
            inAction = true;
        }
    }

    private void OnDrawFinished() {
        animator.SetBool(DRAW_WEAPON, false);
        inAction = false;
    }
    private void OnSheathFinished() {
        animator.SetBool(SHEATH_WEAPON, false);
        inAction = false;
    }
}
