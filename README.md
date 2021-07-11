# RemoteControl.Voiceroid
Demonstrate how to use VOICEROID2/A.I.VOICE Editor API to control the app to speak.

* [+] Easy to install, support both VOICEROID2 and A.I.VOICE
* [+] No touch to ANY existed VOICEROID2/A.I.VOICE files! Call me "**Law Abiding Citizen**"😎
* [-] Must use the exactly matched VOICEROID2/A.I.VOICE version
* [-] No support for Voiceroid(1/EX)

## Usage
1. Copy the dll file to the VOICEROID2/A.I.VOICE folder (where `VoiceroidEditor.exe` or `AIVoiceEditor.exe` exists).
2. Launch VOICEROID2/A.I.VOICE, and wait until it shows the main window.
3. Now you can access to the API. Try run the demo: `VOICeVIO.RemoteControl.CLI`

### VOICEROID2
For VOICEROID2, you can import `AI.Talk.Editor.Api.dll` from VOICEROID2 itself to access the API.

* If you're using 64bit VOICEROID2, your program have to be 64bit. Use `ttsControl.Initialize("VOICEROID2 (x64)");` to initialize.
* If you're using 64bit VOICEROID2, your program have to be 32bit. Use `ttsControl.Initialize("VOICEROID2");` to initialize.

### A.I.VOICE
For A.I.VOICE, currently you must use `AI.Talk.Editor.Api.dll` from this repo (also appeared in `VOICeVIO.RemoteControl.CLI`) to access the API.

Your program have to be 64bit. Use `ttsControl.Initialize("A.I.VOICE");` to initialize. Make sure to use the `AI.Talk.Editor.Api.dll` from this repo.

## Build
This project requires VS 2019 or higher to build.

You have to manually add dlls from VOICEROID2/A.I.VOICE.

By default, we built for VOICEROID2/A.I.VOICE 64bit. If your target is VOICEROID2 32bit, you have to set target platform for all projects. 

If you cannot figure this out, just download binaries from Release page. There is no support for this project.

## License
Apache-2.0

---
by Ulysses from VOICeVIO
