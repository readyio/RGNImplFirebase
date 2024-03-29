using System;
using System.Collections.Generic;
using RGN.ImplDependencies.Core;
using RGN.ImplDependencies.Core.DynamicLinks;

namespace RGN.Modules.DynamicLinks.Runtime
{
    public sealed class DynamicLinks : IDynamicLinks, IImpl<IDynamicLinks>
    {
        private readonly List<DynamicLinkReceivedEventArgsBinder> mDynamicLinkListeners = new List<DynamicLinkReceivedEventArgsBinder>();

        event Action<object, IDynamicLinkReceivedEventArgs> IDynamicLinks.DynamicLinkReceived
        {
            add
            {
                Action<object, IDynamicLinkReceivedEventArgs> toSubscribe = value;
                mDynamicLinkListeners.Add(new DynamicLinkReceivedEventArgsBinder(toSubscribe));
            }
            remove
            {
                Action<object, IDynamicLinkReceivedEventArgs> toUnsubscribe = value;
                var listener = mDynamicLinkListeners.Find((binder) => binder.ToSubscribe == toUnsubscribe);
                if (listener == null)
                {
                    return;
                }
                listener.Dispose();
                mDynamicLinkListeners.Remove(listener);
            }
        }
    }
}
