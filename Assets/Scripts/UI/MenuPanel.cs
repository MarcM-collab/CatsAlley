using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MenuPanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Animator _animator;
    public bool startShown = false;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();

        if(startShown)
        {
            _canvasGroup.alpha = 1;
            Show();
        }
    }

    public virtual void Show()
    {
        Interact();
        _animator.SetBool("Show", true);
    }

    public void Hide()
    {
        StopInteract();
        _animator.SetBool("Show", false);
    }

    private void Interact()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    private void StopInteract()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void VariantHide(bool discard)
    {
        Hide();
        if (discard)
        {
            _animator.SetBool("Discard", true);
        }
        else
        {
            _animator.SetBool("Discard", false);
        }
    }
}
