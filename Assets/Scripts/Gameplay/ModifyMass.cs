using UnityEngine;

public class ModifyMass : MonoBehaviour
{
    private GameObject[] enemyObjects; 
    
    private void Start()
    {
    }

    private void Update()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (Input.GetMouseButtonDown(0))
        {
            GetClosestEnemy(enemyObjects).gameObject.transform.position = Vector2.zero + new Vector2(100f, 0f);
            Change(GetComponent<Rigidbody2D>(), 1);
        }
    }
    
    private Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Change(GetComponent<Rigidbody2D>(), 0);
    }

    public void Change(Rigidbody2D rigidbody2D, int direction)
    {
        if (direction == 0)
            rigidbody2D.mass -= .1f;
        else if (direction == 1 && rigidbody2D.mass < 1)
            rigidbody2D.mass += .1f;
    }
}
