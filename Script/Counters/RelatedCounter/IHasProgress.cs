using System;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<onProgressesEventArgs> Progresses;
    public class onProgressesEventArgs: System.EventArgs
    {
        public float progressNormalized;
    }
}
