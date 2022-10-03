using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour
{
    public EnemyWaveManager m_manager;
    public TMP_Text m_text;
    public int level;
    public GameObject player;
    public Canvas deadScreen;

    private bool gameFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        m_text.text = "Level " + level;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") || (Input.GetButtonDown("Submit") && gameFinished))
        {
            SceneManager.LoadScene("SceneMenu");
        }

        if (player.GetComponent<BaseCharacter>().isDead)
        {
            deadScreen.gameObject.SetActive(true);

            gameFinished = true;
        }
    }

    public void Reset()
    {
        m_manager.Reset();
        level++;
        m_text.text = "Level " + level;
    }
}
