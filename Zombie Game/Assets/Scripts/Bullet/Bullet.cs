using UnityEngine;

namespace ZombieGame
{
    public class Bullet : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 3f);
        }

        private void Update()
        {
            float moveSpeed = 10f;
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }

        // private void OnTriggerEnter2D(Collider2D collider2D) {
        //     if (collider2D.gameObject.TryGetComponent(out Enemy enemy)) {
        //         enemy.GetComponent<HealthSystem>().Damage(30);
        //         DestroySelf();
        //     }


        // private void DestroySelf() {
        //     Destroy(gameObject);
        // }
    }
}