using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UIElements;

//Script that gets the interaction object, sets a radius of size and detects colliders
//When an interaction ocuurs an action is performed
public class Interactor : NetworkBehaviour
{
    //Gets position on interaction point
    [SerializeField] private Transform _interactionPoint;
    //Sets radius of interaction field
    [SerializeField] private float _interactionPointRadius = 0.3f;
    //Sets a layer so the script only interacts with objects in set layer (Should be "interaction" layer)
    [SerializeField] private LayerMask _interactionMask;

    //Detects a max of 4 colliders at once
    private readonly Collider[] _colliders = new Collider[3];
    //(For testing purpouses) Displays the number of interactable objects colliding with the interaction point
    [SerializeField] private int _numFound;



    private void Update()
    {
        if (!isLocalPlayer) return;
        //Gets the number of interactable objects colliding with the interaction point
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
            _interactionMask);
        //If an object is found
        if (_numFound > 0)
        {
            // Will get the nearest (first registered) object
            var interactable = _colliders[0].GetComponent<I_Interactable>();
            //If "e" key is pressed
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                //Peform the interaction task
                interactable.Interact(this);
            }
        }
    }

    //(For testing purpouses) Will display a wired sphere in edit mode to visualises the area of interaction
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position,_interactionPointRadius);
    }
}



