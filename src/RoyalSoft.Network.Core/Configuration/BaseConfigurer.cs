#region using

using System;

#endregion

namespace RoyalSoft.Network.Core.Configuration
{
    public class BaseConfigurer<TComponent>
    {
        protected TComponent Component { get; private set; }

        protected BaseConfigurer(TComponent component)
        {
            if(component == null)
                throw new ArgumentNullException(nameof(component));

            Component = component;
        }

        public virtual void SetDefaultConfig()
        {
        }
    }
}
