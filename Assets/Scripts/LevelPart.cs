using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{ 
    public class LevelPart : MonoBehaviour
    {
        [SerializeField] public Transform m_NextRoomPoint;

        [SerializeField] public List<GameObject> m_PossibleNextRoom;

        // Update is called once per frame
        void Update()
        {

        }
    }
}