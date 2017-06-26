using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System;

public delegate void EventWindowView();
public abstract class WindowViewBase : UnityPoolObject
{
	[SerializeField]
	protected Button buttonClose;

	[SerializeField]
	protected GameObject content;

	[SerializeField]
	protected Vector2 scaleFrom = new Vector2(0.5f, 0.5f);

	protected float speed = 5f;
        
	private Sequence sequence;

	private CanvasGroup cg;

	public event EventWindowView onShowWindowEvent;
	public event EventWindowView onDeactivateWindowEvent;

	public abstract void Close();

	public virtual void OnEnable()
	{
		content.transform.DOScale(scaleFrom, speed).From().SetSpeedBased().OnComplete(() =>
			{
                OnShowWindowEvent();
			}
		);

        gameObject.transform.SetAsLastSibling();
	}

	public virtual void Start()
	{
		DOTween.Init();
		cg = gameObject.GetComponent<CanvasGroup>();
		if (cg == null)
			cg = gameObject.AddComponent<CanvasGroup>();
	}

	public override void OnPush()
	{
		if (content != null)
			content.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), speed).SetSpeedBased().OnComplete
            (
                () =>
                {
                    base.OnPush();
					content.transform.localScale = Vector3.one;
					cg.alpha = 1f;

                        OnDeactivateWindowEvent();
                }
			
			).OnStart(Opacity);
    }

	void Opacity()
	{
		sequence.Append(DOTween.To(() => cg.alpha, i => cg.alpha = i, 0, 0.2f)
                .OnComplete(
				() =>
				{
                  
				})).SetSpeedBased();
	}


    protected void OnShowWindowEvent()
    {
        if (onShowWindowEvent != null)
        {
            onShowWindowEvent();

            onShowWindowEvent = null;
        }
    }

    protected void OnDeactivateWindowEvent()
    {
        if (onDeactivateWindowEvent != null)
        {
            onDeactivateWindowEvent();

            onDeactivateWindowEvent = null;
        }
    }
             

	void OnDisable()
	{
        DOTween.Complete(this);
	}

	public virtual void ShowWindow()
	{

	}

	public virtual void DeactivateWindow()
	{

	}

	public virtual void DeactivateWindow(Action onDeactivateWindowAction)
	{

	}

	public virtual void Initialize()
	{

	}

}
