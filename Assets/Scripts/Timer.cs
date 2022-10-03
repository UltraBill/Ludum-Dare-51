using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Animator m_Animator;

    private const uint maxSegmentNumber = 9;
    private int m_SegmentNumber;

    [SerializeField] private GameObject m_Parent;
    [SerializeField] private GameObject m_Segment;
    [SerializeField] private GameObject m_BaseCharacter;

    [Header("Passive Display")]
    [SerializeField] private GameObject m_PassiveDisplayer;

    private List<TimerPosition> segmentList;

    void Start()
    {
        segmentList = new List<TimerPosition>();

        m_SegmentNumber = (int)maxSegmentNumber;
        m_Animator = GetComponent<Animator>();

        for (int i = 0; i < m_SegmentNumber; i++)
        {
            InstanciateSegment(i, false);
            InstanciateSegment(i, true);
        }

        m_BaseCharacter.GetComponent<BaseCharacter>().ChangePassive();
        DisplayPassive();

        InvokeRepeating(nameof(Pulse), 0f, 1f);
    }

    void InstanciateSegment(int i, bool isLeft)
    {
        var segment = Instantiate(m_Segment);

        segment.transform.SetParent(m_Parent.transform);

        var rect = segment.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2((isLeft ? -1 : 1) * (i + 1) * 50, -70);

        if (isLeft)
            rect.rotation = Quaternion.Euler(0f, 180f, 0f);

        // Storage
        segmentList.Add(new TimerPosition(segment.GetComponent<TimerSegment>(), i));
    }

    void Pulse()
    {
        m_Animator.SetTrigger("Pulse");

        if (m_SegmentNumber >= 0)
        {
            foreach (var segment in segmentList)
            {
                if (segment.position >= m_SegmentNumber)
                {
                    segment.timerSegment.Fade();
                }
                else
                {
                    segment.timerSegment.Pulse();
                }
            }
        }
        else
        {
            m_SegmentNumber = (int)maxSegmentNumber;
            segmentList.ForEach(s => { s.timerSegment.Reset(); s.timerSegment.Pulse(); });

            if (m_BaseCharacter)
            {
                m_BaseCharacter.GetComponent<BaseCharacter>().ChangePassive();

                DisplayPassive();
            }
        }

        m_SegmentNumber--;
    }

    void DisplayPassive()
    {
        var sprite = m_BaseCharacter.GetComponent<BaseCharacter>().GetPassiveSprite();

        if (m_PassiveDisplayer)
        {
           var image = m_PassiveDisplayer.GetComponent<Image>();
           image.sprite = sprite;
        }
           
    }

    private struct TimerPosition
    {
        public TimerSegment timerSegment;
        public int position;

        public TimerPosition(TimerSegment timerSegment, int position) : this()
        {
            this.timerSegment = timerSegment;
            this.position = position;
        }
    }
}
