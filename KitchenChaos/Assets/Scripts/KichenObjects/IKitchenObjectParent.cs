using System.Collections;
using UnityEngine;


    public interface IKitchenObjectParent {

    public Transform GetKichenObjectFollowTransform();
    public void SetKichenObject(KitchenObjects kichenObject);

    public KitchenObjects GetKitchenObject();

    public void ClearKichenObject();

    public bool HasKitchenObject();
}
