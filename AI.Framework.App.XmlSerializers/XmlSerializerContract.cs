using System;
using System.Reflection;
using System.Xml.Serialization;
using AI.Talk.Editor;
using AI.Talk.Editor.Api;

// ReSharper disable once CheckNamespace
namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
    public class XmlSerializerContract : XmlSerializerImplementation
    {
        static XmlSerializerContract()
        {
            Console.WriteLine("VOICeVIO RemoteControl API by Ulysses");
            var remoteServiceHostField = typeof(MainPresenter).GetField("p", BindingFlags.NonPublic | BindingFlags.Instance);
            if (remoteServiceHostField == null)
            {
                Console.WriteLine("[ERROR] Can not find MainPresenter._remoteServiceHost, things may not work.");
            }
            var remoteServiceHost = remoteServiceHostField?.GetValue(MainPresenter.Current);
            if (remoteServiceHost == null)
            {
                RemoteServiceHost<IRemoteService> remote = new(new RemoteService(MainPresenter.Current), "VOICEROID2 (x64)");
                remoteServiceHostField?.SetValue(MainPresenter.Current, remote);
                remote.Start();
            }
        }

        public override bool CanSerialize(Type type)
        {
            return false; //私は 何も できません
        }
    }
}
