using System.Collections;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [SerializeField] float animDuration = 1f;
    public Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Btn()
    {
        StartCoroutine(BtnAnim());
    }

    public IEnumerator BtnAnim()
    {
        animator.SetTrigger("StartAnim");
        yield return new WaitForSeconds(animDuration);
        animator.SetTrigger("EndAnim");
    }
}
