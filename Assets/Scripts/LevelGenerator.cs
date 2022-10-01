using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{

    public class LevelGenerator : MonoBehaviour
    {

        public GameObject baseRoom;
        public uint numOfRoom = 10;

        private void Start()
        {

            LevelPart lp = baseRoom.GetComponent<LevelPart>();

            Vector2 nextPos = new Vector2(17.5f, 0);

            for (int i = 0; i < numOfRoom; i++)
            {
                GameObject nextRoom = lp.m_PossibleNextRoom[Random.Range(0, lp.m_PossibleNextRoom.Count)];

                if (nextRoom)
                {
                    Instantiate(nextRoom, nextPos, Quaternion.identity);

                    lp = nextRoom.GetComponent<LevelPart>();
                    nextPos.x += 17.5f;
                }
            }
        }
    }

}
