using System;
using System.Collections.Generic;
using System.Text;

namespace Events
{
    public class Disposer
    {
        private Stack<Action> disposeCalls;
        private static Disposer i;

        private Disposer()
        {
            disposeCalls = new Stack<Action>();
        }

        public static void Register(Action callback)
        {
            if (i == null) i = new Disposer();

            lock (i)
            {
                i.disposeCalls.Push(callback);
            }
        }

        public static void DisposeAll()
        {
            if (i == null) return;

            lock(i)
            {
                foreach (Action dispose in i.disposeCalls)
                {
                    dispose();
                }
            }
        }
    }

    public abstract class DisposeOnExit
    {
        Action pointer;

        public DisposeOnExit()
        {
            pointer = () => { dispose(); };
            Disposer.Register(() => { pointer(); });
        }

        public void DisposeEarly()
        {
            pointer();
            pointer = () => { };
        }

        protected abstract void dispose();

        
    }
}
