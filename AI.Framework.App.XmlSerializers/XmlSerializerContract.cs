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
        private static bool _initialized = false;
        static XmlSerializerContract()
        {
            Console.WriteLine("VOICeVIO RemoteControl API by Ulysses");
        }

        public override bool CanSerialize(Type type)
        {
            if (!_initialized)
            {
                _initialized = TryStartHost();
            }

            return false; //私は 何も できません
        }

        public bool TryStartHost()
        {
            FieldInfo remoteServiceHostField = null;
            foreach (var fieldInfo in typeof(MainPresenter).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (fieldInfo.FieldType.FullName != null && fieldInfo.FieldType.FullName.Contains("RemoteServiceHost"))
                {
                    remoteServiceHostField = fieldInfo;
                    Console.WriteLine($"[INFO] Found MainPresenter._remoteServiceHost as {fieldInfo.Name}");
                    break;
                }
            }

            //var remoteServiceHostField = typeof(MainPresenter).GetField("q", BindingFlags.NonPublic | BindingFlags.Instance);

            if (remoteServiceHostField == null)
            {
                Console.WriteLine("[ERROR] Can not find MainPresenter._remoteServiceHost, things may not work.");
            }

            if (MainPresenter.Current == null)
            {
                Console.WriteLine("[ERROR] MainPresenter is null, most likely to fail.");
                return false;
            }

            try
            {
                var remoteServiceHost = remoteServiceHostField?.GetValue(MainPresenter.Current);
                if (remoteServiceHost == null)
                {
                    RemoteServiceHost<IRemoteService> remote = new(new RemoteService(MainPresenter.Current), "A.I.VOICE");
                    remoteServiceHostField?.SetValue(MainPresenter.Current, remote);
                    remote.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }


            return true;
        }
    }
}
