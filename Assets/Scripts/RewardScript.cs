using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RewardScript : MonoBehaviour
{
    public PassiveDrop r1, r2, r3;
    private GameObject boss;
    private bool isUsed = false;


    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectsWithTag("Bossu").First();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUsed) {
            if (!r1.gameObject.activeSelf)
            {
                if (Random.Range(0, 1) == 0)
                {
                    boss.GetComponent<BaseBoss>().AddPassivePool(r2.passive);
                }else {
                    boss.GetComponent<BaseBoss>().AddPassivePool(r3.passive);
                }
                isUsed = true;
                Debug.Log(this.gameObject.name);
                this.gameObject.SetActive(false);
                Destroy(this);
            }
            else if (!r2.gameObject.activeSelf)
            {
                if (Random.Range(0, 1) == 0)
                {
                    boss.GetComponent<BaseBoss>().AddPassivePool(r1.passive);
                }else {
                    boss.GetComponent<BaseBoss>().AddPassivePool(r3.passive);
                }
                isUsed = true;
                Debug.Log(this.gameObject.name);
                this.gameObject.SetActive(false);
                Destroy(this);
            }
            else if (!r3.gameObject.activeSelf)
            {
                if (Random.Range(0, 1) == 0)
                {
                    boss.GetComponent<BaseBoss>().AddPassivePool(r2.passive);
                }else {
                    boss.GetComponent<BaseBoss>().AddPassivePool(r1.passive);
                }
                isUsed = true;
                Debug.Log(this.gameObject.name);
                this.gameObject.SetActive(false);
                Destroy(this);
            }
        }
    }
}
