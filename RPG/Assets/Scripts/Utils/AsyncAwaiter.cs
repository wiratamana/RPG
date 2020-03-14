﻿using System;
using System.Runtime.CompilerServices;

namespace Tamana
{
    public class AsyncAwaiter : INotifyCompletion
    {
        public bool IsCompleted { get; private set; }        
        private Action continuation;

        private readonly int frames;
        public int CurrentFrame
        {
            set
            {
                if (value == frames)
                {
                    IsCompleted = true;
                    continuation();
                }
            }
        }

        public AsyncAwaiter(int frames)
        {
            this.frames = frames;
        }

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;   
        }

        public void GetResult() { }
        public AsyncAwaiter GetAwaiter() => this;
    }
}
