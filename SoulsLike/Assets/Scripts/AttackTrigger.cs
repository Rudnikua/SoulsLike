using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    [SerializeField] private WeaponSO weaponData;

    private void Update() {
        Debug.Log(weaponData.damage);
    }
    private void OnTriggerEnter(Collider other) {
        if (weaponData == null) {
            Debug.LogError("WeaponData is NULL");
        }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null) {
            Debug.Log("Enemy was hit");
            enemy.TakeDamage(weaponData.damage);
        }
    }
}
