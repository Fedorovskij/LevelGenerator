using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewEditTarget : MonoBehaviour
{
    [SerializeField]
    private Button _jellyButton;
	
    [SerializeField]
    private Button _sugarDropButton;
	
    [SerializeField]
    private ViewSugarDrop _sugarDropPanel;

    [SerializeField]
    private ViewTargetColorInFieldInGame _targetColorPanel;

    public ViewTargetColorInFieldInGame GetTargetColorPanel
    {
        get{ return _targetColorPanel; }
    }

    [SerializeField]
    private FieldController _fc;

    [SerializeField]
    ViewCellTarget _viewCellTarget;

    private List<ViewCellTarget> _listallViewCellTarget = new List<ViewCellTarget>();

    public void Construct(FieldTarget fieldTarget)
    {
        if (_listallViewCellTarget.Count == 0)
        {
            _viewCellTarget.gameObject.SetActive(false);

            foreach (string name in Enum.GetNames(typeof(FieldTarget)))
            {
                if (name == "None")
                {
                    continue;
                }

                GameObject temp = Instantiate(_viewCellTarget.gameObject);

                temp.transform.SetParent(_viewCellTarget.gameObject.transform.parent, false);

                ViewCellTarget v = temp.GetComponent<ViewCellTarget>();

                v.nameTarget.text = name;

                _listallViewCellTarget.Add(v);

                v.toggle.onValueChanged.AddListener(
                    (ViewCellTarget) =>
                    {
                        ActivateTarget(v);
                    });

                FieldTarget ft = (FieldTarget)Enum.Parse(typeof(FieldTarget), name);

                switch (ft)
                {
                    case FieldTarget.Block:
                        
                        v.PanelToActivate = null;

                        break;

                    case FieldTarget.SugarDrop:
                        
                        v.PanelToActivate = _sugarDropPanel.gameObject;

                        break; 

                    case FieldTarget.Color:
                        
                        v.PanelToActivate = _targetColorPanel.gameObject;

                        break;
                }

                temp.SetActive(true);
            }
        }

        ChangeTarget(fieldTarget);
		
    }

    private bool ActivateTarget(ViewCellTarget viewCellTarget)
    {
        foreach (var v in _listallViewCellTarget)
        {
            if (v.nameTarget != viewCellTarget.nameTarget)
            {
                v.toggle.onValueChanged.RemoveAllListeners();

                v.toggle.isOn = false;

                if (v.PanelToActivate != null)
                {
                    v.PanelToActivate.SetActive(false);
                }

                v.toggle.onValueChanged.AddListener(
                    (ViewCellTarget) =>
                    {
                        ActivateTarget(v);
                    });
            }
            else
            {
                if (v.PanelToActivate != null)
                {
                    v.PanelToActivate.SetActive(true); 
                }
            }
        }

        return true;
    }


    private void ChangeTarget(FieldTarget fg)
    {	 
        var v = _listallViewCellTarget.Find(x => x.nameTarget.text == fg.ToString());

        if (v != null)
        {
            v.toggle.isOn = true;
        }
    }

}
