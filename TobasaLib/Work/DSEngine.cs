#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2018  Jefri Sibarani
 
    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/
#endregion

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using DirectShowLib;

namespace Tobasa
{
    public enum PlayState
    {
        Stopped,
        Paused,
        Running,
        Init
    }

    public enum MediaType
    {
        Audio,
        Video
    }

    public enum DisplaySource
    {
        Clip,
        Stream
    }

    public class PlaylistItem
    {
        public string ClipPath { get; set; }
        public bool Played { get; set; }
    }

    public class DSEngine : Notifier
    {
        #region Setters/getters
        
        /// Raise event every time playing a media
        public event Action<EventArgs> PlayerStarted;

        public IMediaControl MediaControl
        {
            get { return mediaControl; }
        }

        public IVideoWindow VideoWindow
        {
            get { return videoWindow; }
        }

        public IBaseFilter SourceFilter
        {
            get { return sourceFilter; }
        }

        public IAMCrossbar Crossbar
        {
            get { return crossbar; }
        }

        public IAMTVTuner Tuner
        {
            get { return tuner; }
        }

        public string VideoInputDevicePath
        {
            get { return videoInputDevicePath; }
            set { videoInputDevicePath = value; }
        }

        public string VideoInputDeviceName
        {
            get { return videoInputDeviceName; }
        }

        public PhysicalConnectorType VideoInputDeviceSource
        {
            get { return videoInputDeviceSource; }
        }

        public bool UsingCrossbar
        {
            get { return usingCrossBar; }
        }

        public bool AudioOnly
        {
            get { return isAudioOnly; }
        }

        public PlayState CurrentPlayState
        {
            get { return currentState; }
        }

        public DisplaySource DisplaySource
        {
            get { return displaySource; }
        }

        public bool UsePlayList
        {
            get { return usePlayList; }
            set { usePlayList = value; }
        }

        public int CurrentVolume
        {
            get { return currentVolume; }
        }

        #endregion

        #region DirectShow interfaces

        IGraphBuilder graphBuilder = null;
        IMediaControl mediaControl = null;
        IMediaEventEx mediaEventEx = null;
        IVideoWindow videoWindow = null;
        IBasicAudio basicAudio = null;
        IBasicVideo basicVideo = null;
        IMediaSeeking mediaSeeking = null;
        IMediaPosition mediaPosition = null;
        IVideoFrameStep frameStep = null;
        ICaptureGraphBuilder2 captureGraphBuilder = null;
        IBaseFilter sourceFilter = null; // temp
        IAMCrossbar crossbar = null;
        IAMTVTuner tuner = null;

        #endregion

        #region  Member variables

        public const int WMGRAPHNOTIFY = 0x0400 + 13;
        public string MediaFileFilter = "Video Files (*.avi; *.qt; *.mov; *.mpg; *.mpeg; *.m1v)|*.avi; *.qt; *.mov; *.mpg; *.mpeg; *.m1v|Audio files (*.wav; *.mpa; *.mp2; *.mp3; *.au; *.aif; *.aiff; *.snd)|*.wav; *.mpa; *.mp2; *.mp3; *.au; *.aif; *.aiff; *.snd|MIDI Files (*.mid, *.midi, *.rmi)|*.mid; *.midi; *.rmi|Image Files (*.jpg, *.bmp, *.gif, *.tga)|*.jpg; *.bmp; *.gif; *.tga|All Files (*.*)|*.*";
        const int VolumeFull = 0;
        const int VolumeSilence = -10000;
        string videoInputDevicePath = "";
        string videoInputDeviceName = "";
        PhysicalConnectorType videoInputDeviceSource = PhysicalConnectorType.Video_Tuner;
        bool usingCrossBar = false;
        string filename = string.Empty;
        bool isAudioOnly = false;
        int currentVolume = VolumeFull;
        PlayState currentState = PlayState.Init;
        double currentPlaybackRate = 1.0;
        IntPtr hDrain = IntPtr.Zero;
        DisplaySource displaySource = DisplaySource.Clip;
        List<PlaylistItem> playList;
        bool usePlayList = false;
        int playlistPlayedCount = 0;
        Panel videoPanel;
        IntPtr notifyWindow;
        
        #endregion

        #region OlePropertyFrame

        /// A (modified) definition of OleCreatePropertyFrame found here: http://groups.google.no/group/microsoft.public.dotnet.languages.csharp/browse_thread/thread/db794e9779144a46/55dbed2bab4cd772?lnk=st&q=[DllImport(%22olepro32.dll%22)]&rnum=1&hl=no#55dbed2bab4cd772
        [DllImport("oleaut32.dll", CharSet=CharSet.Unicode, ExactSpelling=true)]
        public static extern int OleCreatePropertyFrame( 
            IntPtr hwndOwner, 
            int x, 
            int y, 
            [MarshalAs(UnmanagedType.LPWStr)] string lpszCaption, 
            int cObjects, 
            [MarshalAs(UnmanagedType.Interface, ArraySubType=UnmanagedType.IUnknown)] 
            ref object ppUnk, 
            int cPages, 
            IntPtr lpPageClsID, 
            int lcid, 
            int dwReserved, 
            IntPtr lpvReserved);
        
        #endregion  

#if DEBUG
        private DsROTEntry rot = null;
#endif

        #region Constructor
        public DSEngine(Panel _videoPanel,IntPtr _notifyWindow)
        {
            videoPanel   = _videoPanel;
            notifyWindow = _notifyWindow;
        }
        #endregion

        private void RenderToPanel(Panel panel, IntPtr notifyWindow)
		{
			int hr;

            // Have the graph signal event via window callbacks for performance
            hr = mediaEventEx.SetNotifyWindow(notifyWindow, DSEngine.WMGRAPHNOTIFY, IntPtr.Zero);
            DsError.ThrowExceptionForHR(hr);

            // Set the video window to be a child of the panel
			hr = videoWindow.put_Owner(panel.Handle);
			DsError.ThrowExceptionForHR(hr);

			// Set video window style
			hr = videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
			DsError.ThrowExceptionForHR(hr);

			// Position video window in client rect of panel
			hr = videoWindow.SetWindowPosition(0, 0, panel.Width, panel.Height);
			DsError.ThrowExceptionForHR(hr);

			// Make the video window visible, now that it is properly positioned
			hr = videoWindow.put_Visible(OABool.True);
			DsError.ThrowExceptionForHR(hr);
		}

        public void InitPlayList(string folder)
        {
            if (!usePlayList)
                return;
            
            if (playList != null)
                playList.Clear();

            playList = new List<PlaylistItem>();

            // Process the files found in the directory. 
            string[] fileEntries = Directory.GetFiles(folder);
            foreach (string fileName in fileEntries)
            {
                playList.Add(new PlaylistItem() { ClipPath = fileName, Played = false });
            }
        }

        public void StartFromPlayList()
        {
            // play file from play list.
            // work with HandleGraphEvent()

            // mark played file,increment played count & if all file already played
            // reset playlist item back to not played

            if (!usePlayList)
                return;
            
            // all file already played,reset playlist
            if (playList.Count == playlistPlayedCount)
            {
                foreach (PlaylistItem task in playList)
                {
                    task.Played = false;
                }
                playlistPlayedCount = 0;
            }


            foreach (PlaylistItem task in playList)
            {
                if (task.Played)
                    continue;
                else
                {
                    StartDisplay(videoPanel, notifyWindow, DisplaySource.Clip, task.ClipPath);
                    if (currentState == PlayState.Running)
                    {
                        // mark played file.
                        task.Played = true;
                        playlistPlayedCount++;
                        break;
                    }
                }
            }
        }

        public void StartDisplay(Panel panel ,IntPtr notifyWindow, DisplaySource dispSource,string filename)
        {
            int hr = 0;
            displaySource = dispSource;

            if (dispSource == DisplaySource.Clip)
            {
                if (filename == String.Empty)
                    return;
            }

            try
            {
                graphBuilder = (IGraphBuilder)new FilterGraph();
                // QueryInterface for DirectShow interfaces
                mediaControl = (IMediaControl)graphBuilder;
                mediaEventEx = (IMediaEventEx)graphBuilder;
                mediaSeeking = (IMediaSeeking)graphBuilder;
                mediaPosition = (IMediaPosition)graphBuilder;
                // Query for video interfaces, which may not be relevant for audio files
                videoWindow = graphBuilder as IVideoWindow;
                basicVideo = graphBuilder as IBasicVideo;
                // Query for audio interfaces, which may not be relevant for video-only files
                basicAudio = graphBuilder as IBasicAudio;

                if (displaySource == DisplaySource.Stream)
                {
                    /// Find source filter ///

                    string possibleTvTunerName = "Video Capture";
                    DsDevice[] vidInputDevices = GetVideoInputDevices();
                    
                    for (int i = 0; i < vidInputDevices.Length; i++)
                    {
                        DsDevice possibleTvTuner = vidInputDevices[i];

                        // use first device if none defined
                        if (videoInputDevicePath == "")
                            videoInputDevicePath = possibleTvTuner.DevicePath;

                        if (possibleTvTuner.DevicePath != videoInputDevicePath)
                            continue;
                        try
                        {
                            if (sourceFilter != null)
                            {
                                Marshal.ReleaseComObject(sourceFilter);
                                sourceFilter = null;
                            }
                            //Create the filter for the selected video input device
                            sourceFilter = CreateFilterFromPath(FilterCategory.VideoInputDevice, possibleTvTuner.DevicePath);
                            possibleTvTunerName = possibleTvTuner.Name;
                            videoInputDeviceName = possibleTvTuner.Name;

                            if (sourceFilter == null)
                                return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            //System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

                    // Attach the filter graph to the capture graph
                    hr = captureGraphBuilder.SetFiltergraph(graphBuilder);
                    DsError.ThrowExceptionForHR(hr);

                    // Add Capture filter to our graph.
                    hr = graphBuilder.AddFilter(sourceFilter, possibleTvTunerName  /*"Video Capture"*/);
                    DsError.ThrowExceptionForHR(hr);

                    //// Init tuner/crossbar //////////////////////////////////////////////////////////////////
                    object o;
                    tuner = null;
                    crossbar = null;

                    hr = captureGraphBuilder.FindInterface(FindDirection.UpstreamOnly, null, sourceFilter, typeof(IAMTVTuner).GUID, out o);
                    //DsError.ThrowExceptionForHR(hr);
                    if (hr >= 0)
                    {
                        tuner = (IAMTVTuner)o;
                        o = null;

                        hr = captureGraphBuilder.FindInterface(null, null, sourceFilter, typeof(IAMCrossbar).GUID, out o);
                        DsError.ThrowExceptionForHR(hr);
                        if (hr >= 0)
                        {
                            crossbar = (IAMCrossbar)o;
                            //crossbar = (IBaseFilter)o;
                            o = null;
                        }
                        usingCrossBar = true;
                    }
                    else
                        usingCrossBar = false;
                    ///////////////////////////////////////////////////////////////////////////////////////////

                    // Render the preview pin on the video capture filter
                    // Use this instead of this.graphBuilder.RenderFile
                    hr = captureGraphBuilder.RenderStream(PinCategory.Preview, DirectShowLib.MediaType.Video, sourceFilter, null, null);
                    DsError.ThrowExceptionForHR(hr);

                    // If we are using tv tuner, force audio on..and set device input from tuner
                    if (tuner !=null )
                    {
                        if (videoInputDeviceSource == PhysicalConnectorType.Video_Tuner)
                        {
                            ChangeVideoInputSource(PhysicalConnectorType.Video_Tuner);
                            ChangeAudioInputSource(PhysicalConnectorType.Audio_Tuner);
                        }
                    }

                    // Now that the filter has been added to the graph and we have
                    // rendered its stream, we can release this reference to the filter.
                    // temp
                    //Marshal.ReleaseComObject(sourceFilter);

                    isAudioOnly = false;
                }
                else
                {
                    // Have the graph builder construct its the appropriate graph automatically
                    hr = graphBuilder.RenderFile(filename, null);
                    DsError.ThrowExceptionForHR(hr);
                
                    // Is this an audio-only file (no video component)?
                    CheckVisibility();
                }

                if (!isAudioOnly)
                {
                    RenderToPanel(panel, notifyWindow);
                    GetFrameStepInterface();
                }
                else
                {
                    /// Initialize the default player size and enable playback menu items
                    /// <jefri> hr = InitPlayerWindow();
                    /// <jefri> DsError.ThrowExceptionForHR(hr);
                    /// <jefri> EnablePlaybackMenu(true, MediaType.Audio);
                }

                currentPlaybackRate = 1.0;

                #if DEBUG
                        rot = new DsROTEntry(this.graphBuilder);
                #endif

                // Run the graph to play the media
                hr = mediaControl.Run();
                DsError.ThrowExceptionForHR(hr);

                currentState = PlayState.Running;
                
                // Every call to this method,volume level will be 0, 
                // so inform client to adjust volume level
                // Raise event, so our client can take action for it
                if (PlayerStarted != null)
                    PlayerStarted(new EventArgs());
            }
            catch (COMException ce)
            {

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "DirectShow Exception";
                args.Source = ce.Source;
                args.Message = ce.Message;
                args.Exception = ce;

                OnNotifyError(args);
            }
            catch
            {
                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "DirectShow Exception";
                args.Source = "";
                args.Message = "An unrecoverable error has occurred.";
                args.Exception = null;

                OnNotifyError(args);
            }
        }

        public void CloseClip()
        {
            int hr = 0;

            // Stop media playback
            if (mediaControl != null)
                hr = mediaControl.Stop();

            // Clear global flags
            currentState = PlayState.Stopped;
            isAudioOnly = true;

            // Free DirectShow interfaces
            CloseInterfaces();

            // Clear file name to allow selection of new file with open dialog
            filename = string.Empty;

            // No current media state
            currentState = PlayState.Init;
        }

        public void CheckVisibility()
        {
            int hr = 0;
            OABool lVisible;

            if ((videoWindow == null) || (basicVideo == null))
            {
                // Audio-only files have no video interfaces.  This might also
                // be a file whose video component uses an unknown video codec.
                isAudioOnly = true;
                return;
            }
            else
            {
                // Clear the global flag
                isAudioOnly = false;
            }

            hr = videoWindow.get_Visible(out lVisible);
            if (hr < 0)
            {
                // If this is an audio-only clip, get_Visible() won't work.
                //
                // Also, if this video is encoded with an unsupported codec,
                // we won't see any video, although the audio will work if it is
                // of a supported format.
                if (hr == unchecked((int)0x80004002)) //E_NOINTERFACE
                {
                    isAudioOnly = true;
                }
                else
                    DsError.ThrowExceptionForHR(hr);
            }
        }

        /// Some video renderers support stepping media frame by frame with the
        /// IVideoFrameStep interface.  See the interface documentation for more
        /// details on frame stepping.
        public bool GetFrameStepInterface()
        {
            int hr = 0;
            IVideoFrameStep frameStepTest = null;

            // Get the frame step interface, if supported
            frameStepTest = (IVideoFrameStep)graphBuilder;

            // Check if this decoder can step
            hr = frameStepTest.CanStep(0, null);
            if (hr == 0)
            {
                frameStep = frameStepTest;
                return true;
            }
            else
            {
                frameStep = null;
                return false;
            }
        }

        public void CloseInterfaces()
        {
            int hr = 0;

            try
            {
                lock (this)
                {
                    // Relinquish ownership (IMPORTANT!) after hiding video window
                    if (!isAudioOnly)
                    {
                        hr = videoWindow.put_Visible(OABool.False);
                        DsError.ThrowExceptionForHR(hr);
                        hr = videoWindow.put_Owner(IntPtr.Zero);
                        DsError.ThrowExceptionForHR(hr);
                    }

                    if (mediaEventEx != null)
                    {
                        hr = mediaEventEx.SetNotifyWindow(IntPtr.Zero, 0, IntPtr.Zero);
                        DsError.ThrowExceptionForHR(hr);
                    }

                    #if DEBUG
                              if (rot != null)
                              {
                                rot.Dispose();
                                rot = null;
                              }
                    #endif

                    // Release and zero DirectShow interfaces
                    if (mediaEventEx != null)
                        mediaEventEx = null;
                    if (mediaSeeking != null)
                        mediaSeeking = null;
                    if (mediaPosition != null)
                        mediaPosition = null;
                    if (mediaControl != null)
                        mediaControl = null;
                    if (basicAudio != null)
                        basicAudio = null;
                    if (basicVideo != null)
                        basicVideo = null;
                    if (videoWindow != null)
                        videoWindow = null;
                    if (frameStep != null)
                        frameStep = null;
                    if (graphBuilder != null)
                        Marshal.ReleaseComObject(graphBuilder); graphBuilder = null;

                    // Tuner stuff
                    if (captureGraphBuilder != null)
                        Marshal.ReleaseComObject(captureGraphBuilder); captureGraphBuilder = null;

                    if (tuner != null)
                    {
                        Marshal.ReleaseComObject(tuner);
                        tuner = null;
                    }

                    if (crossbar != null)
                    {
                        Marshal.ReleaseComObject(crossbar);
                        crossbar = null;
                    }
                    
                    //temp
                    if (sourceFilter != null)
                    {
                        Marshal.ReleaseComObject(sourceFilter);
                        sourceFilter = null;
                    }                    
                    GC.Collect();
                }
            }
            catch
            {
            }
        }

        #region Media palayer controls

        public void PauseClip()
        {
            if (mediaControl == null)
                return;

            // Toggle play/pause behavior
            if ((currentState == PlayState.Paused) || (currentState == PlayState.Stopped))
            {
                if (mediaControl.Run() >= 0)
                    currentState = PlayState.Running;
            }
            else
            {
                if (mediaControl.Pause() >= 0)
                    currentState = PlayState.Paused;
            }
        }

        public void StopClip()
        {
            int hr = 0;
            DsLong pos = new DsLong(0);

            if ((mediaControl == null) || (mediaSeeking == null))
                return;

            // Stop and reset postion to beginning
            if ((currentState == PlayState.Paused) || (currentState == PlayState.Running))
            {
                hr = mediaControl.Stop();
                currentState = PlayState.Stopped;

                // Seek to the beginning
                hr = mediaSeeking.SetPositions(pos, AMSeekingSeekingFlags.AbsolutePositioning, null, AMSeekingSeekingFlags.NoPositioning);

                // Display the first frame to indicate the reset condition
                hr = mediaControl.Pause();
            }
        }

        public int ToggleMute()
        {
            int hr = 0;

            if ((graphBuilder == null) || (basicAudio == null))
                return 0;

            // Read current volume
            hr = basicAudio.get_Volume(out currentVolume);
            if (hr == -1) //E_NOTIMPL
            {
                // Fail quietly if this is a video-only media file
                return 0;
            }
            else if (hr < 0)
            {
                return hr;
            }

            // Switch volume levels
            if (currentVolume == VolumeFull)
                currentVolume = VolumeSilence;
            else
                currentVolume = VolumeFull;

            // Set new volume
            hr = basicAudio.put_Volume(currentVolume);
            return hr;
        }

        public int SetVolume(int level)
        {
            int hr = 0;

            if ((graphBuilder == null) || (basicAudio == null))
                return 0;

            // Read current volume
            hr = basicAudio.get_Volume(out currentVolume);
            if (hr == -1) //E_NOTIMPL
            {
                // Fail quietly if this is a video-only media file
                return 0;
            }
            else if (hr < 0)
            {
                return hr;
            }

            // Set new volume
            hr = basicAudio.put_Volume(level);
            
            // Save current volume
            hr = basicAudio.get_Volume(out currentVolume);

            return hr;
        }

        public int StepOneFrame()
        {
            int hr = 0;

            // If the Frame Stepping interface exists, use it to step one frame
            if (frameStep != null)
            {
                // The graph must be paused for frame stepping to work
                if (currentState != PlayState.Paused)
                    PauseClip();

                // Step the requested number of frames, if supported
                hr = frameStep.Step(1, null);
            }

            return hr;
        }

        public int StepFrames(int nFramesToStep)
        {
            int hr = 0;

            // If the Frame Stepping interface exists, use it to step frames
            if (frameStep != null)
            {
                // The renderer may not support frame stepping for more than one
                // frame at a time, so check for support.  S_OK indicates that the
                // renderer can step nFramesToStep successfully.
                hr = frameStep.CanStep(nFramesToStep, null);
                if (hr == 0)
                {
                    // The graph must be paused for frame stepping to work
                    if (currentState != PlayState.Paused)
                        PauseClip();

                    // Step the requested number of frames, if supported
                    hr = frameStep.Step(nFramesToStep, null);
                }
            }
            return hr;
        }

        public int ModifyRate(double dRateAdjust)
        {
            int hr = 0;
            double dRate;

            // If the IMediaPosition interface exists, use it to set rate
            if ((mediaPosition != null) && (dRateAdjust != 0.0))
            {
                hr = mediaPosition.get_Rate(out dRate);
                if (hr == 0)
                {
                    // Add current rate to adjustment value
                    double dNewRate = dRate + dRateAdjust;
                    hr = mediaPosition.put_Rate(dNewRate);

                    // Save global rate
                    if (hr == 0)
                    {
                        currentPlaybackRate = dNewRate;
                    }
                }
            }
            return hr;
        }

        public int SetRate(double rate)
        {
            int hr = 0;

            // If the IMediaPosition interface exists, use it to set rate
            if (mediaPosition != null)
            {
                hr = mediaPosition.put_Rate(rate);
                if (hr >= 0)
                {
                    currentPlaybackRate = rate;
                }
            }
            return hr;
        }

        #endregion

        public void ChangePreviewState(bool showVideo)
        {
            int hr = 0;

            // If the media control interface isn't ready, don't call it
            if (mediaControl == null)
                return;

            if (showVideo)
            {
                if (currentState != PlayState.Running)
                {
                    // Start previewing video data
                    hr = mediaControl.Run();
                    currentState = PlayState.Running;
                }
            }
            else
            {
                // Stop previewing video data
                hr = mediaControl.StopWhenReady();
                currentState = PlayState.Stopped;
            }
        }

        public void ResizeVideoWindow(Panel panel)
        {
            int hr = 0;

            /*
            // Stop graph when Form is iconic
            if (this.WindowState == FormWindowState.Minimized)
                ChangePreviewState(false);

            // Restart Graph when window come back to normal state
            if (this.WindowState == FormWindowState.Normal)
                ChangePreviewState(true);
            */

            // Track the movement of the container window and resize as needed
            if (videoWindow != null)
            {
                hr = videoWindow.SetWindowPosition(panel.ClientRectangle.Left,
                                                        panel.ClientRectangle.Top,
                                                        panel.ClientSize.Width,
                                                        panel.ClientSize.Height);
                DsError.ThrowExceptionForHR(hr);
            }
        }

        public void HandleGraphEvent()
        {
            int hr = 0;
            EventCode evCode;
            IntPtr evParam1, evParam2;

            // Make sure that we don't access the media event interface
            // after it has already been released.
            if (mediaEventEx == null)
                return;

            // Process all queued events
            while (mediaEventEx.GetEvent(out evCode, out evParam1, out evParam2, 0) == 0)
            {
                // Free memory associated with callback, since we're not using it
                hr = mediaEventEx.FreeEventParams(evCode, evParam1, evParam2);

                // If this is the end of the clip, reset to beginning
                if (evCode == EventCode.Complete)
                {
                    if (!usePlayList)
                    {
                        DsLong pos = new DsLong(0);
                        // Reset to first frame of movie
                        hr = mediaSeeking.SetPositions(pos, AMSeekingSeekingFlags.AbsolutePositioning,
                          null, AMSeekingSeekingFlags.NoPositioning);
                        if (hr < 0)
                        {
                            // Some custom filters (like the Windows CE MIDI filter)
                            // may not implement seeking interfaces (IMediaSeeking)
                            // to allow seeking to the start.  In that case, just stop
                            // and restart for the same effect.  This should not be
                            // necessary in most cases.
                            hr = mediaControl.Stop();
                            hr = mediaControl.Run();
                        }
                        
                    }
                    else if (usePlayList)
                    {
                        CloseClip();
                        StartFromPlayList();
                        break;
                    }
                }
            }
        }

        public void ChangeVideoInputSource(PhysicalConnectorType inputType)
        {
            if (crossbar != null)
            {
                int numOutputPins;
                int numInputPins;
                int hr = crossbar.get_PinCounts(out numOutputPins, out numInputPins);
                DsError.ThrowExceptionForHR(hr);

                int outputPin = -1;
                //get the video output pin
                for (int i = 0; i < numOutputPins; i++)
                {
                    int relatedPin;
                    PhysicalConnectorType physicalType;
                    hr = crossbar.get_CrossbarPinInfo(false, i, out relatedPin, out physicalType);
                    DsError.ThrowExceptionForHR(hr);

                    //VideoDecoder
                    //AudioDecoder
                    if (physicalType == PhysicalConnectorType.Video_VideoDecoder)
                    {
                        outputPin = i;
                        break;
                    }
                }

                int inputPin = -1;
                for (int i = 0; i < numInputPins; i++)
                {
                    int relatedPin;
                    PhysicalConnectorType physicalType;
                    hr = crossbar.get_CrossbarPinInfo(true, i, out relatedPin, out physicalType);
                    DsError.ThrowExceptionForHR(hr);

                    if (physicalType == inputType)
                    {
                        inputPin = i;
                        break;
                    }
                }

                if (outputPin != -1 && inputPin != -1)
                {
                    hr = crossbar.Route(outputPin, inputPin);

                    videoInputDeviceSource = inputType;

                    DsError.ThrowExceptionForHR(hr);
                }
            }
        }

        /// Force TV Tuner Audio to work
        public void ChangeAudioInputSource(PhysicalConnectorType inputType)
        {
            if (crossbar != null)
            {
                int numOutputPins;
                int numInputPins;
                int hr = crossbar.get_PinCounts(out numOutputPins, out numInputPins);
                DsError.ThrowExceptionForHR(hr);

                int outputPin = -1;
                //get the audio output pin
                for (int i = 0; i < numOutputPins; i++)
                {
                    int relatedPin;
                    PhysicalConnectorType physicalType;
                    hr = crossbar.get_CrossbarPinInfo(false, i, out relatedPin, out physicalType);
                    DsError.ThrowExceptionForHR(hr);


                    //AudioDecoder
                    if (physicalType == PhysicalConnectorType.Audio_AudioDecoder)
                    {
                        outputPin = i;
                        break;
                    }
                }

                int inputPin = -1;
                for (int i = 0; i < numInputPins; i++)
                {
                    int relatedPin;
                    PhysicalConnectorType physicalType;
                    hr = crossbar.get_CrossbarPinInfo(true, i, out relatedPin, out physicalType);
                    DsError.ThrowExceptionForHR(hr);

                    if (physicalType == inputType)
                    {
                        inputPin = i;
                        break;
                    }
                }

                if (outputPin != -1 && inputPin != -1)
                {
                    hr = crossbar.Route(outputPin, inputPin);
                    videoInputDeviceSource = inputType;
                    DsError.ThrowExceptionForHR(hr);
                }
            }
        }

        public DsDevice[] GetVideoInputDevices()
        {
            /// this returns TV tuner video input devices and also web cams
            DsDevice[] vidInputDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            /// TODO does audio work in my app if other tuner is in use?
            /// audio definitely does work if no other tuners are in use

            /// HACK
            ////since MCE is going to step on my tuner
            /// grab the last one in the list first
            /// so my program wont get stepped on until MCE needs both tuners
            Array.Reverse(vidInputDevices);

            return vidInputDevices;
        }

        public bool HasVideoInputDevices()
        {
            DsDevice[] vidInputDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            if (vidInputDevices == null || vidInputDevices.Length <= 0)
                return false;
            else
                return true;
        }

        #region DirextShow interfaces stuffs

        public IBaseFilter FindCaptureDevice()
        {
            IBaseFilter theVideoInputDevice = CreateFilterFromPath(FilterCategory.VideoInputDevice, videoInputDevicePath);
            if (theVideoInputDevice == null)
                return null; ;
            return theVideoInputDevice;
        }

        /// this call will return the first match
        private IBaseFilter CreateFilter(Guid category, string friendlyname)
        {
            object source = null;
            Guid iid = typeof(IBaseFilter).GUID;
            foreach (DsDevice device in DsDevice.GetDevicesOfCat(category))
            {
                if (device.Name.CompareTo(friendlyname) == 0)
                {
                    device.Mon.BindToObject(null, null, ref iid, out source);
                    break;
                }
            }
            return (IBaseFilter)source;
        }

        /// this call will return a specific device, for multiple tuners
        private IBaseFilter CreateFilterFromPath(Guid category, string devicePath)
        {
            object source = null;
            Guid iid = typeof(IBaseFilter).GUID;
            foreach (DsDevice device in DsDevice.GetDevicesOfCat(category))
            {
                if (device.DevicePath.CompareTo(devicePath) == 0)
                {
                    device.Mon.BindToObject(null, null, ref iid, out source);
                    break;
                }
            }
            return (IBaseFilter)source;
        }

        #endregion

        /// <summary>
        /// Displays a property page for a filter
        /// </summary>
        /// <param name="dev">The filter for which to display a property page</param>
        public void DisplayPropertyPage(IBaseFilter dev)
        {
            //Get the ISpecifyPropertyPages for the filter
            ISpecifyPropertyPages pProp = dev as ISpecifyPropertyPages;
            int hr = 0;

            if (pProp == null)
            {
                //If the filter doesn't implement ISpecifyPropertyPages, try displaying IAMVfwCompressDialogs instead!
                IAMVfwCompressDialogs compressDialog = dev as IAMVfwCompressDialogs;
                if (compressDialog != null)
                {

                    hr = compressDialog.ShowDialog(VfwCompressDialogs.Config, IntPtr.Zero);
                    DsError.ThrowExceptionForHR(hr);
                }
                else
                {
                    MessageBox.Show("Item has no property page", "No Property Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }

            //Get the name of the filter from the FilterInfo struct
            FilterInfo filterInfo;
            hr = dev.QueryFilterInfo(out filterInfo);
            DsError.ThrowExceptionForHR(hr);

            // Get the propertypages from the property bag
            DsCAUUID caGUID;
            hr = pProp.GetPages(out caGUID);
            DsError.ThrowExceptionForHR(hr);

            /// Create and display the OlePropertyFrame
            object oDevice = (object)dev;
            hr = OleCreatePropertyFrame(notifyWindow, 0, 0, filterInfo.achName, 1, ref oDevice, caGUID.cElems, caGUID.pElems, 0, 0, IntPtr.Zero);
            DsError.ThrowExceptionForHR(hr);

            Marshal.ReleaseComObject(oDevice);

            if (filterInfo.pGraph != null)
            {
                Marshal.ReleaseComObject(filterInfo.pGraph);
            }

            // Release COM objects
            Marshal.FreeCoTaskMem(caGUID.pElems);
        }
    }
}
