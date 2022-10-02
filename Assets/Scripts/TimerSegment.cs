using UnityEngine;

public class TimerSegment : MonoBehaviour
{
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void Pulse()
    {
        m_Animator.SetTrigger("Pulse");
        m_Animator.ResetTrigger("Fade");
        m_Animator.ResetTrigger("Reset");
    }

    public void Fade()
    {
        m_Animator.SetTrigger("Fade");
        m_Animator.ResetTrigger("Pulse");
        m_Animator.ResetTrigger("Reset");
    }

    public void Reset()
    {
        m_Animator.SetTrigger("Reset");
        m_Animator.ResetTrigger("Fade");
        m_Animator.ResetTrigger("Pulse");
    }
}
