using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionMenu : MonoBehaviour
{
    [SerializeField] public Transform m_cursor;
    [SerializeField] public Transform m_char;

    private List<Vector3> positions = new List<Vector3>{
        new Vector3(-5.4f, -1.5f, 0f),
        new Vector3(0.0f, -1.5f, 0f),
        new Vector3(5.15f, -1.5f, 0f)
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float minDist = float.MaxValue;
        Vector3 p = new Vector3();
        int idx = 0;
        foreach(Vector3 v in positions)
        {
            Vector3 d = v - m_char.position;
            float dist = Vector3.Dot(d, d);
            if (minDist > dist)
            {
                minDist = dist;
                p = v;
                idx = positions.IndexOf(v);
            }
        }

        m_cursor.position = p;

        if (Input.GetButtonDown("Submit"))
        {
            switch(idx)
            {
                case 0:
                SceneManager.LoadScene("SampleScene");
                break;
                case 1:
                break;
                case 2:
                Application.Quit();
                break;
            }
        }
    }
}
