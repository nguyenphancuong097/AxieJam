using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupBase : MonoBehaviour
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    // [SerializeField]
    // protected Canvas canvas;
    // [SerializeField]
    // protected GraphicRaycaster graphicRaycaster;
    [SerializeField]
    protected CanvasGroup canvasGroup;
    [SerializeField]
    private Animator uiAnimator;
    #endregion

    #region PARAMS    
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    #endregion

    #region METHODS
    public virtual void OnInit()
    {
        OnHide();
    }

    [Sirenix.OdinInspector.Button]
    public virtual void OnShow(float fadeTime = 0)
    {
        canvasGroup.SetActive(true, fadeTime);
    }

    [Sirenix.OdinInspector.Button]
    public virtual void OnHide(float fadeTime = 0)
    {
        canvasGroup.SetActive(false, fadeTime);
    }

    public virtual void OnRelease()
    {

    }

    public virtual bool OnBack()
    {
        return false;
    }

    public virtual string GetName()
    {
        return this.GetType().FullName;
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        // if (canvas == null)
        // {
        //     canvas = gameObject.GetComponent<Canvas>();
        //     if (canvas == null)
        //     {
        //         canvas = gameObject.AddComponent<Canvas>();
        //     }
        // }

        // if (graphicRaycaster == null)
        // {
        //     graphicRaycaster = gameObject.GetComponent<GraphicRaycaster>();
        //     if (graphicRaycaster == null)
        //     {
        //         graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        //     }
        // }
    }
#endif
    #endregion

    #region DEBUG
    #endregion
}
