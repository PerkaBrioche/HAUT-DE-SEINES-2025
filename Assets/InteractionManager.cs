using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private BoxCollider _actualCollider;
    private bounce3D _colliderBounce3D;
    
    private bool _holdClick = false;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            BoxCollider box = hit.collider.GetComponent<BoxCollider>();
            if (box != null)
            {
                if (_holdClick)
                {
                    if (box.CompareTag("smoke"))
                    {
                        box.GetComponent<SmokeController>().Diseapear();
                        return;
                    }
                }
                
                
                
                if (_actualCollider != null) // COLLIDE DEJA AVANT
                { 
                    if(_actualCollider != box) // COLLIDER DIFFERENT ?
                    {
                        if(_colliderBounce3D != null)
                            _colliderBounce3D.EndHighlight();
                        _colliderBounce3D = null;
                        _actualCollider = box;
                    }
                }
                
                if(box.TryGetComponent(out bounce3D bounce))
                {
                    // NEW COLLIDER
                    _colliderBounce3D = bounce;
                    if(_colliderBounce3D != null)
                        _colliderBounce3D.HiglightBounce();                
                }
                _actualCollider = box;
            }
        }
        else
        {
            if(_actualCollider != null)
            {
                if(_colliderBounce3D != null)
                    _colliderBounce3D.EndHighlight();
                
                _colliderBounce3D = null;
                _actualCollider = null;
            }
        }
        
        
        
        
        
        if (Input.GetMouseButtonDown(0))
        {
            _holdClick = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _holdClick = false;
        }
    }

}
