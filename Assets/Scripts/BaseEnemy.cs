using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public int maxLifePoint = 10;
    private int actualLifePoint;

    void Start()
    {
        actualLifePoint = maxLifePoint;
    }

    public void TakeDamage(int damage)
    {
        actualLifePoint -= damage;

        if (actualLifePoint <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
