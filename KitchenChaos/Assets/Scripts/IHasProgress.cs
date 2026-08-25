using System;
using UnityEngine;

public interface IHasProgress {
    public event EventHandler<OnProgressChangedEventHandler> OnProgressChanged;
    public class OnProgressChangedEventHandler : EventArgs {
        public float progressNormalized;
    }       

}
