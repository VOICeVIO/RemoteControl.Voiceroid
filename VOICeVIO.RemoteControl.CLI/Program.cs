//extern alias ai;
using System;
using System.Threading.Tasks;
using AI.Talk.Editor.Api;

namespace VOICeVIO.RemoteControl.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tts = new TtsControl();
            tts.Initialize("VOICEROID2 (x64)");
            //tts.StartHost(); //StartHost Currently only works for VOICEROID2 (x64)
            //await Task.Delay(10000);

            try
            {
                tts.Connect();
            }
            catch
            {
                Console.WriteLine("Can not connect to VOICEROID2. Please launch VOICEROID2 before this program.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Connected to Voiceroid2.");
            Console.WriteLine("Current Available Voice Presets:");
            for (var i = 0; i < tts.VoicePresetNames.Length; i++)
            {
                var voice = tts.VoicePresetNames[i];
                Console.WriteLine($"[{i:D2}] {voice}");
            }

            var selectedVoice = "結月ゆかり";
            Console.WriteLine("Input a number to select voice:");
            var input = Console.ReadLine()?.Trim();
            if (int.TryParse(input, out var num))
            {
                if (num >= 0 && num < tts.VoicePresetNames.Length)
                {
                    selectedVoice = tts.VoicePresetNames[num];
                }
            }
            Console.WriteLine($"Select {selectedVoice}");
            tts.CurrentVoicePresetName = selectedVoice;
            
            Console.WriteLine("Set Master Control...");
            var master = tts.MasterControl; 
            master.Pitch = 0.85;
            master.PitchRange = 1.3;
            tts.MasterControl = master;

            Console.WriteLine("Sending Text...");
            tts.Text = "あなたを神像へと嵌め込みましょう！";
            Console.WriteLine($"Play Time: {tts.GetPlayTime()}");
            await WaitTillIdle(tts);
            
            Console.WriteLine("Speaking...");
            await Task.Delay(1000);
            tts.Play();
            await WaitTillIdle(tts);
            Console.WriteLine("Speak done!");

            Console.WriteLine("Saving WAV...");
            tts.WriteWaveToFile("output.wav");
            await WaitTillIdle(tts);

            Console.WriteLine("All done! Press Enter to exit.");
            Console.ReadLine();
            tts.Disconnect();
        }

        static async Task WaitTillIdle(TtsControl tts)
        {
            while (tts.Status == HostStatus.Busy)
            {
                await Task.Delay(100);
            }
        }
    }
}
