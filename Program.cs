using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;

using System.Diagnostics.CodeAnalysis;

namespace AudioOutputToSpeakers
{
    internal class Program
    {
        private static readonly Guid _speakerId = Guid.Parse("{7594ad6c-e057-48fc-ba08-e62d2c989246}");

        private static Task Main() => new Program().MainAsync();

        [SuppressMessage("Performance", "CA1822:Mark members as static")]
        private async Task MainAsync()
        {
            CoreAudioController audioController = new();

            IEnumerable<CoreAudioDevice> outputDevices = await audioController.GetPlaybackDevicesAsync();
            outputDevices = outputDevices.Where(outDev => outDev.State == DeviceState.Active);

            CoreAudioDevice currentDefaultOutput = await audioController.GetDefaultDeviceAsync(DeviceType.Playback, Role.Multimedia);

            if (currentDefaultOutput.Id != _speakerId)
                await audioController.SetDefaultDeviceAsync(outputDevices.Where(outDev => outDev.Id == _speakerId).First());
        }
    }
}
