using System;
using System.ServiceModel.Configuration;

namespace Infotecs.Attika.AttikaConsoleHost.Ninject
{
    public class NinjectBehaviorExtensionElement : BehaviorExtensionElement
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