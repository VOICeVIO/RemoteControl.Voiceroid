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
            //foreach (var availableHostName in tts.GetAvailableHostNames())
            //{
            //    Console.WriteLine(availableHostName);
            //}
            tts.Initialize("A.I.VOICE");
            //tts.StartHost(); //StartHost Currently only works for VOICEROID2 (x64)
            //await Task.Delay(10000);

            try
            {
                tts.Connect();
            }
            catch
            {
                Console.WriteLine("Can not connect to VOICEROID2/A.I.VOICE. Please launch app before this program.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"Connected to A.I.VOICE: {tts.Version}");
            Console.WriteLine("Current Available Voice Presets:");
            for (var i = 0; i < tts.VoicePresetNames.Length; i++)
            {
                var voice = tts.VoicePresetNames[i];
                Console.WriteLine($"[{i:D2}] {voice}");
            }

            var selectedVoice = "琴葉 茜";
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
            tts.Text = "マイクテスト";
            tts.Play();
            await WaitTillIdle(tts);
            tts.Stop();

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

            Console.WriteLine("All done! Type quit to exit. Type any other words to speak.");
            while (true)
            {
                var text = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    if (text.ToLowerInvariant() == "quit")
                    {
                        Console.WriteLine("Bye~");
                        tts.Disconnect();
                        return;
                    }

                    await WaitTillIdle(tts);
                    tts.Stop();
                    tts.Text = text;
                    tts.Play();
                    await WaitTillIdle(tts);
                }
            }
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
