using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNative {
    private event Action _action = delegate {};

    public void Invoke() { 
        _action.Invoke();
    }

    public void AddListener(Action listener) { 
        _action -= listener;
        _action += listener; 
    }

    public void RemoveListener(Action listener) { 
        _action -= listener; 
    }
}

public class EventNative<A> {
    private event Action<A> _action = delegate {};

    public void Invoke(A paramA) { 
        _action.Invoke(paramA);
    }

    public void AddListener(Action<A> listener) { 
        _action -= listener;
        _action += listener; 
    }

    public void RemoveListener(Action<A> listener) { 
        _action -= listener; 
    }
}

public class EventNative<A, B> {
    private event Action<A, B> _action = delegate {};

    public void Invoke(A paramA, B paramB) { 
        _action.Invoke(paramA, paramB);
    }

    public void AddListener(Action<A, B> listener) { 
        _action -= listener;
        _action += listener; 
    }

    public void RemoveListener(Action<A, B> listener) { 
        _action -= listener; 
    }
}
