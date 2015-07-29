using System;
using System.ServiceModel.Configuration;

namespace Infotecs.Attika.AttikaConsoleHost.Ninject
{
// ReSharper disable UnusedMember.Global
// Required for ninject behavior extension
    public class NinjectBehaviorExtensionElement : BehaviorExtensionElement
// ReSharper restore UnusedMember.Global
    {
        public override Type BehaviorType
        {
            get { return typeof (NinjectBehaviorAttribute); }
        }

        protected override object CreateBehavior()
        {
            return new NinjectBehaviorAttribute();
        }
    }
}