using UnityEngine;

namespace RhythmSystem
{
    public class RhythmChecker : MonoBehaviour
    {
        /// Select VRIF weapon damage type
        [SerializeField]
        private DamageDealerType damageDealerType;

        [SerializeField]
        private bool RhythmActive = false;

        /// Initilize with start VRIF weapon base damage.
        private float damage;

        /// Last rhythm bonus checked
        private RhythmBonusSO rhythmBonus;

        private void Start()
        {
            // First we save the default damage

            switch (damageDealerType)
            {
                case DamageDealerType.RaycastWeapon:
                    damage = GetComponent<BNG.RaycastWeapon>().Damage;
                    break;
                case DamageDealerType.DamageCollider:
                    damage = GetComponent<BNG.DamageCollider>().Damage;
                    break;
            }
        }

        public void CheckRhythm ()
        {
            if (RhythmActive)
            {
                Debug.Log("Checking Rhythm");

                // Stop if there isn't a RhythmManager
                if (RhythmManager.Instance == null)
                {
                    Debug.LogError("There is no RhythmManager");
                    return;
                }

                // Change rhythm bonus
                RhythmManager.Instance.CheckRhythmBonus(out rhythmBonus);

                // Change weapon damage with rhythm bonus
                switch (damageDealerType)
                {
                    case DamageDealerType.RaycastWeapon:
                        GetComponent<BNG.RaycastWeapon>().Damage = damage * rhythmBonus.Bonus;
                        break;
                    case DamageDealerType.DamageCollider:
                        GetComponent<BNG.DamageCollider>().Damage = damage * rhythmBonus.Bonus;
                        break;
                }

                Debug.Log($"Total damage: {damage * rhythmBonus.Bonus} Base damage: {damage} Rhythm bonus: {rhythmBonus.Bonus}");
            }
            else
            {
                // Change weapon damage
                switch (damageDealerType)
                {
                    case DamageDealerType.RaycastWeapon:
                        GetComponent<BNG.RaycastWeapon>().Damage  = damage;
                        break;
                    case DamageDealerType.DamageCollider:
                        GetComponent<BNG.DamageCollider>().Damage = damage;
                        break;
                }

                Debug.Log($"Damage: {damage}");
            }

        }

        public void SetRhythmActive(bool active)
        {
            RhythmActive = active;
        }

        enum DamageDealerType
        {
            RaycastWeapon  = 0,
            DamageCollider = 1,
        }
    }
}
