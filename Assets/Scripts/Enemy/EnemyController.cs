using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] players;
    [SerializeField] private float speed;
    [SerializeField] private Color[] color;
    
    private void Start()
    {
        int r = Random.Range(0, 2);
        Transform enemyTransform = transform;
        enemyTransform.right = players[r].position - enemyTransform.position;
        GetComponent<MeshRenderer>().material.color = color[r];
    }

    private void Update()
    {
        transform.Translate(new Vector2(1f, 0f) * Time.deltaTime * speed);
    }
}
