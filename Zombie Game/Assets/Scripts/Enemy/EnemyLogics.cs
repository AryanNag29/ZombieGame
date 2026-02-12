using UnityEngine;

public class EnemyLogics : MonoBehaviour
{
    #region References

    [Header("References")] [SerializeField]
    private Gun gun;

    #endregion

    #region Variables

    [Header("Health")] private float currentHealth;
    private float maxHealth = 100f;

    [Header("Damage/Heal")] private float damage = 10f;

    private float heal = 10f;

    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}