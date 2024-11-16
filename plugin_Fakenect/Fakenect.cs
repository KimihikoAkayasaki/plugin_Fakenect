// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Amethyst.Plugins.Contract;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace plugin_Fakenect;

[Export(typeof(ITrackingDevice))]
[ExportMetadata("Name", "Xbox One Kinect (Fake)")]
[ExportMetadata("Guid", "K2VRTEAM-AME2-APII-DVCE-DVCEFAKENECT")]
[ExportMetadata("Publisher", "公彦赤屋先")]
[ExportMetadata("Version", "1.0.0.2")]
[ExportMetadata("Website", "https://github.com/KimihikoAkayasaki/plugin_Fakenect")]
public class Fakenect : ITrackingDevice
{
    [Import(typeof(IAmethystHost))] private IAmethystHost Host { get; set; }

    public bool IsPositionFilterBlockingEnabled => false;
    public bool IsPhysicsOverrideEnabled => false;
    public bool IsSelfUpdateEnabled => true;
    public bool IsFlipSupported => true;
    public bool IsAppOrientationSupported => true;
    public bool IsSettingsDaemonSupported => false;
    public object SettingsInterfaceRoot => null;

    public bool IsInitialized { get; private set; }
    public bool IsSkeletonTracked => true;

    public int DeviceStatus => IsInitialized ? 0 : 1;

    public ObservableCollection<TrackedJoint> TrackedJoints { get; } =
        // Prepend all supported joints to the joints list
        new(Enum.GetValues<TrackedJointType>().Where(x => x != TrackedJointType.JointManual)
            .Select(x => new TrackedJoint { Name = x.ToString(), Role = x }));

    public string DeviceStatusString => "Success!\nS_OK\nEverything's all fine!";
    public Uri ErrorDocsUri => null;

    public void OnLoad()
    {
    }

    public void Initialize()
    {
        // Mark as initialized
        IsInitialized = true;

        Host.Log($"Tried to initialize the fake Kinect sensor with status: {DeviceStatusString}");
    }

    public void Shutdown()
    {
        // Mark as not initialized
        IsInitialized = false;
    }

    public void Update()
    {
        // ignored
    }

    public void SignalJoint(int jointId)
    {
        // ignored
    }
}