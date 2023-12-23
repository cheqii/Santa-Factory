using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverUIAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Animator animator;


    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.ResetTrigger("exitHover");
        animator.SetTrigger("isHover");
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("ExitHover");
        animator.ResetTrigger("isHover");
        animator.SetTrigger("exitHover");
    }
}
