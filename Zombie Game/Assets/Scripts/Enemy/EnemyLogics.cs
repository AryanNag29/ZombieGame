using UnityEngine;

namespace ZombieGame
{
    public class EnemyLogics : MonoBehaviour
    {
        #region References

        [Header("References")] [SerializeField]
        private Gun gun;

        #endregion

        #region Variables

        [Header("Health")] public float currentHealth;
        private float maxHealth = 100f;

        [Header("Damage/Heal")] private float damage = 10f;

        private float heal = 10f;

        #endregion


        #region Functions

        public void DealDamage()
        {
            currentHealth = maxHealth;
            maxHealth -= damage;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        #endregion


        #region Start/Update

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        #endregion
    }
}