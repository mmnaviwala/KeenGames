using UnityEngine;
using System.Collections;

public interface IDestructable {

    void TakeDamage(int damage);
    void TakeDamage(int damage, CharacterStats source);
    void TakeDamage(int damage, Vector3 sourcePos);
    void TakeDamage(bool instantKill);

    void TakeDamageThroughArmor(int damage);
    void TakeDamageThroughArmor(int damage, CharacterStats source);
    void TakeDamageThroughArmor(int damage, Vector3 sourcePos);
    void TakeDamageThroughArmor(bool instantKill);

    void Die();
}
