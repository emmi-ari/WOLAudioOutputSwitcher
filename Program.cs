using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;

using System.Diagnostics.CodeAnalysis;

namespace AudioOutputToSpeakers
{
    internal class Program
    {
        private static Task Main() => new Program().MainAsync();

        [SuppressMessage("Performance", "CA1822:Mark members as static")]
        private async Task MainAsync()
        {
            CoreAudioController audioController = new();

            IEnumerable<CoreAudioDevice> outputDevices = await audioController.GetPlaybackDevicesAsync();
            outputDevices = outputDevices.Where(outDev => outDev.State == DeviceState.Active);

            CoreAudioDevice currentDefaultOutput = await audioController.GetDefaultDeviceAsync(DeviceType.Playback, Role.Multimedia);

            if (currentDefaultOutput.FullName != "Speakers (USB PnP Audio Device)")
                audioController.SetDefaultDevice(outputDevices.Where(outDev => outDev.FullName == "Speakers (USB PnP Audio Device)").First());
        }
    }
}
