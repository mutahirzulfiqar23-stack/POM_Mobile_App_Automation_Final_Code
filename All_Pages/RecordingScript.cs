//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Threading;
//using OpenQA.Selenium.Appium.Android;

//namespace POM_Mobile_App_Automate_Stage.All_Pages
//{
//    internal class RecordingScript
//    {
//        // ─────────────────────────────────────────────────────────────
//        // CONFIGURATION
//        // ─────────────────────────────────────────────────────────────

//        // Local folder where final videos are saved
//        private const string LocalVideoFolder = @"D:\ScreenshotFailed\CompleteTestVideo";

//        // Temporary path on the Android device during recording
//        private const string DeviceVideoPath = "/sdcard/test_recording.mp4";

//        // ADB executable – change if adb.exe is not on your PATH
//        private const string AdbPath = "adb";

//        // ─────────────────────────────────────────────────────────────
//        // STATE
//        // ─────────────────────────────────────────────────────────────
//        private static Process? _adbProcess;
//        private static Thread? _recordingThread;
//        private static bool _isRecording;
//        private static string? _currentDeviceSerial;

//        // ─────────────────────────────────────────────────────────────
//        // PUBLIC API
//        // ─────────────────────────────────────────────────────────────

//        /// <summary>
//        /// Starts ADB screen recording on the connected Android device.
//        /// Call this inside [SetUp], right after the driver is ready.
//        /// </summary>
//        /// <param name="driver">The active AndroidDriver.</param>
//        public static void StartRecording(AndroidDriver driver)
//        {
//            try
//            {
//                // Resolve the device serial so we target the right device
//                _currentDeviceSerial = GetDeviceSerial(driver);

//                // Make sure local output folder exists
//                Directory.CreateDirectory(LocalVideoFolder);

//                // Remove any leftover recording file from a previous run
//                RunAdbCommand($"-s {_currentDeviceSerial} shell rm -f {DeviceVideoPath}");

//                _isRecording = true;

//                // screenrecord runs until we kill the process or the 3-minute limit is hit.
//                // --bit-rate 2M keeps file size manageable; raise if you need higher quality.
//                string args = $"-s {_currentDeviceSerial} shell screenrecord --bit-rate 2M {DeviceVideoPath}";

//                _recordingThread = new Thread(() =>
//                {
//                    try
//                    {
//                        _adbProcess = StartAdbProcess(args);
//                        _adbProcess.WaitForExit();
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine($"[RecordingScript] Recording thread error: {ex.Message}");
//                    }
//                });

//                _recordingThread.IsBackground = true;
//                _recordingThread.Start();

//                Console.WriteLine("[RecordingScript] Screen recording started.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[RecordingScript] Failed to start recording: {ex.Message}");
//            }
//        }

//        /// <summary>
//        /// Stops the recording and pulls the video file to the local machine.
//        /// Call this inside [TearDown].
//        /// </summary>
//        /// <param name="testName">Used to name the saved video file.</param>
//        public static void StopRecordingAndSave(string testName)
//        {
//            if (!_isRecording)
//            {
//                Console.WriteLine("[RecordingScript] No active recording to stop.");
//                return;
//            }

//            try
//            {
//                // Send SIGINT to gracefully stop screenrecord so it finalises the MP4
//                RunAdbCommand($"-s {_currentDeviceSerial} shell kill -SIGINT $(pidof screenrecord)");

//                // Give screenrecord a moment to write the file footer
//                Thread.Sleep(3000);

//                // Kill the local ADB process wrapper if still running
//                try
//                {
//                    if (_adbProcess != null && !_adbProcess.HasExited)
//                    {
//                        _adbProcess.Kill();
//                        _adbProcess.WaitForExit(5000);
//                    }
//                }
//                catch { /* ignore */ }

//                _isRecording = false;

//                // Build a timestamped local file name
//                string safeTestName = SanitiseFileName(testName);
//                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
//                string localFilePath = Path.Combine(LocalVideoFolder, $"{safeTestName}_{timestamp}.mp4");

//                // Pull the video from the device
//                Console.WriteLine("[RecordingScript] Pulling video from device...");
//                RunAdbCommand($"-s {_currentDeviceSerial} pull {DeviceVideoPath} \"{localFilePath}\"");

//                // Clean up on device
//                RunAdbCommand($"-s {_currentDeviceSerial} shell rm -f {DeviceVideoPath}");

//                if (File.Exists(localFilePath))
//                    Console.WriteLine($"[RecordingScript] Video saved to: {localFilePath}");
//                else
//                    Console.WriteLine("[RecordingScript] WARNING – video file was not pulled successfully.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[RecordingScript] Failed to stop/save recording: {ex.Message}");
//            }
//        }

//        // ─────────────────────────────────────────────────────────────
//        // HELPERS
//        // ─────────────────────────────────────────────────────────────

//        private static string GetDeviceSerial(AndroidDriver driver)
//        {
//            try
//            {
//                // Appium stores the device UDID in session capabilities
//                var caps = driver.Capabilities;
//                if (caps["udid"] is string udid && !string.IsNullOrWhiteSpace(udid))
//                    return udid;
//            }
//            catch { /* fall through */ }

//            // Fallback: use the first device reported by "adb devices"
//            string output = RunAdbCommandWithOutput("devices");
//            foreach (string line in output.Split('\n'))
//            {
//                string trimmed = line.Trim();
//                if (!string.IsNullOrEmpty(trimmed)
//                    && !trimmed.StartsWith("List")
//                    && trimmed.Contains("\t"))
//                {
//                    return trimmed.Split('\t')[0].Trim();
//                }
//            }

//            return string.Empty; // no -s flag; works when only one device is connected
//        }

//        private static void RunAdbCommand(string arguments)
//        {
//            using var p = StartAdbProcess(arguments);
//            p.WaitForExit(15_000);
//        }

//        private static string RunAdbCommandWithOutput(string arguments)
//        {
//            using var p = new Process();
//            p.StartInfo = new ProcessStartInfo
//            {
//                FileName = AdbPath,
//                Arguments = arguments,
//                RedirectStandardOutput = true,
//                RedirectStandardError = true,
//                UseShellExecute = false,
//                CreateNoWindow = true
//            };
//            p.Start();
//            string output = p.StandardOutput.ReadToEnd();
//            p.WaitForExit(10_000);
//            return output;
//        }

//        private static Process StartAdbProcess(string arguments)
//        {
//            var p = new Process();
//            p.StartInfo = new ProcessStartInfo
//            {
//                FileName = AdbPath,
//                Arguments = arguments,
//                RedirectStandardOutput = false,
//                RedirectStandardError = false,
//                UseShellExecute = false,
//                CreateNoWindow = true
//            };
//            p.Start();
//            return p;
//        }

//        private static string SanitiseFileName(string name)
//        {
//            foreach (char c in Path.GetInvalidFileNameChars())
//                name = name.Replace(c, '_');
//            return name;
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using OpenQA.Selenium.Appium.Android;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    /// <summary>
    /// Continuous ADB screen recorder with automatic segment rotation.
    ///
    /// Android's screenrecord command has a hard 3-minute limit per file.
    /// This class works around that by automatically rotating segments every
    /// ~170 seconds, pulling each finished segment to disk, and then merging
    /// all segments into a single MP4 when StopRecordingAndSave() is called.
    ///
    /// REQUIREMENT: FFmpeg must be installed and on your system PATH.
    ///   Download: https://ffmpeg.org/download.html  (add the \bin folder to PATH)
    ///   Quick test: open a terminal and run  ffmpeg -version
    /// </summary>
    internal class RecordingScript
    {
        // ─────────────────────────────────────────────────────────────────────
        // CONFIGURATION  –  tweak these if needed
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>Local folder where the final merged video is saved.</summary>
        private const string LocalVideoFolder = @"D:\ScreenshotFailed\CompleteTestVideo";

        /// <summary>
        /// Seconds per segment. Android's hard limit is 180 s; we rotate at 170 s
        /// to give the OS time to finalise the file before we pull it.
        /// </summary>
        private const int SegmentDurationSeconds = 170;

        /// <summary>ADB bit-rate for screenrecord. 2 Mbps is a good balance.</summary>
        private const string BitRate = "2M";

        /// <summary>ADB executable name (assumed to be on PATH).</summary>
        private const string AdbPath = "adb";

        /// <summary>FFmpeg executable name (assumed to be on PATH).</summary>
        private const string FfmpegPath = "ffmpeg";

        // ─────────────────────────────────────────────────────────────────────
        // PRIVATE STATE
        // ─────────────────────────────────────────────────────────────────────

        private static volatile bool _isRecording;
        private static string _deviceSerial = string.Empty;
        private static string _sessionFolder = string.Empty;
        private static Thread? _rotatorThread;
        private static int _segmentIndex;
        private static List<string> _localSegments = new();

        // ─────────────────────────────────────────────────────────────────────
        // PUBLIC API
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts continuous screen recording.
        /// Call this in [SetUp] right after the driver is confirmed non-null.
        /// </summary>
        public static void StartRecording(AndroidDriver driver)
        {
            try
            {
                _deviceSerial = GetDeviceSerial(driver);
                _segmentIndex = 0;
                _localSegments = new List<string>();
                _isRecording = true;

                // Unique temp folder for this test session's raw segments
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                _sessionFolder = Path.Combine(Path.GetTempPath(), $"adb_rec_{timestamp}");
                Directory.CreateDirectory(_sessionFolder);

                // Ensure the final output folder exists
                Directory.CreateDirectory(LocalVideoFolder);

                Console.WriteLine("[RecordingScript] Starting continuous screen recording...");
                Console.WriteLine($"[RecordingScript] Temp segments folder: {_sessionFolder}");

                // Background thread that records one segment after another
                _rotatorThread = new Thread(RotatorLoop) { IsBackground = true };
                _rotatorThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RecordingScript] StartRecording failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Stops recording, pulls the last segment, merges everything into
        /// one MP4, and saves it to D:\ScreenshotFailed\CompleteTestVideo\.
        /// Call this in [TearDown].
        /// </summary>
        public static void StopRecordingAndSave(string testName)
        {
            if (!_isRecording)
            {
                Console.WriteLine("[RecordingScript] No active recording to stop.");
                return;
            }

            try
            {
                Console.WriteLine("[RecordingScript] Stopping recording...");

                // Signal the rotator loop to stop
                _isRecording = false;

                // Wait for the rotator thread to finish pulling the last segment (up to 60 s)
                _rotatorThread?.Join(60_000);

                if (_localSegments.Count == 0)
                {
                    Console.WriteLine("[RecordingScript] No segments were recorded.");
                    return;
                }

                string safeTestName = SanitiseFileName(testName);
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string finalVideoPath = Path.Combine(LocalVideoFolder,
                                                     $"{safeTestName}_{timestamp}.mp4");

                if (_localSegments.Count == 1)
                {
                    // Only one segment – copy directly, no FFmpeg needed
                    File.Copy(_localSegments[0], finalVideoPath, overwrite: true);
                    Console.WriteLine($"[RecordingScript] Video saved to: {finalVideoPath}");
                }
                else
                {
                    MergeSegments(_localSegments, finalVideoPath);
                }

                CleanupTempFolder();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RecordingScript] StopRecordingAndSave failed: {ex.Message}");
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // SEGMENT ROTATION LOOP  (runs on background thread)
        // ─────────────────────────────────────────────────────────────────────

        private static void RotatorLoop()
        {
            while (_isRecording)
            {
                int index = _segmentIndex++;
                string devicePath = $"/sdcard/rec_seg_{index:D4}.mp4";
                string localPath = Path.Combine(_sessionFolder, $"seg_{index:D4}.mp4");

                // Remove any stale file on the device
                RunAdbCommand($"shell rm -f {devicePath}");

                Console.WriteLine($"[RecordingScript] Recording segment {index + 1} → {devicePath}");

                // --time-limit stops screenrecord after SegmentDurationSeconds automatically
                string args = BuildAdbArgs(
                    $"shell screenrecord --bit-rate {BitRate} --time-limit {SegmentDurationSeconds} {devicePath}");

                using (Process proc = StartAdbProcess(args))
                {
                    // Poll every second; if the test finished early, kill screenrecord immediately
                    while (!proc.WaitForExit(1000))
                    {
                        if (!_isRecording)
                        {
                            // Send SIGINT so Android finalises the MP4 header properly
                            RunAdbCommand($"shell kill -SIGINT $(pidof screenrecord)");
                            proc.WaitForExit(8000);
                            break;
                        }
                    }
                }

                // Give Android 2 s to flush and close the file
                Thread.Sleep(2000);

                // Pull the finished segment to local disk
                Console.WriteLine($"[RecordingScript] Pulling segment {index + 1}...");
                RunAdbCommand($"pull {devicePath} \"{localPath}\"");

                // Remove from device to free storage
                RunAdbCommand($"shell rm -f {devicePath}");

                if (File.Exists(localPath) && new FileInfo(localPath).Length > 0)
                {
                    _localSegments.Add(localPath);
                    Console.WriteLine(
                        $"[RecordingScript] Segment {index + 1} pulled " +
                        $"({new FileInfo(localPath).Length / 1024} KB)");
                }
                else
                {
                    Console.WriteLine(
                        $"[RecordingScript] WARNING – segment {index + 1} is missing or empty.");
                }
            }

            Console.WriteLine("[RecordingScript] Rotator loop finished.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // FFMPEG MERGE
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Uses FFmpeg's concat demuxer to join all segments into one MP4.
        /// Uses stream-copy (no re-encode) so it finishes in seconds.
        /// </summary>
        private static void MergeSegments(List<string> segments, string outputPath)
        {
            Console.WriteLine(
                $"[RecordingScript] Merging {segments.Count} segments into one video...");

            // Write the concat list file required by FFmpeg
            string concatListPath = Path.Combine(_sessionFolder, "concat_list.txt");
            using (var writer = new StreamWriter(concatListPath, append: false))
            {
                foreach (string seg in segments)
                {
                    // FFmpeg needs forward slashes; escape any single-quotes in the path
                    string escaped = seg.Replace("\\", "/").Replace("'", "\\'");
                    writer.WriteLine($"file '{escaped}'");
                }
            }

            // -f concat   → concat demuxer (joins without re-encoding)
            // -safe 0     → allow absolute paths in the list
            // -c copy     → stream copy (fast, lossless)
            // -movflags   → move index to front so the file is playable immediately
            string ffmpegArgs =
                $"-y -f concat -safe 0 -i \"{concatListPath}\" " +
                $"-c copy -movflags +faststart \"{outputPath}\"";

            Console.WriteLine($"[RecordingScript] Running: ffmpeg {ffmpegArgs}");

            using var ffmpeg = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = FfmpegPath,
                    Arguments = ffmpegArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            ffmpeg.OutputDataReceived += (_, e) =>
            {
                if (e.Data != null) Console.WriteLine("[FFmpeg] " + e.Data);
            };
            ffmpeg.ErrorDataReceived += (_, e) =>
            {
                if (e.Data != null) Console.WriteLine("[FFmpeg] " + e.Data);
            };

            ffmpeg.Start();
            ffmpeg.BeginOutputReadLine();
            ffmpeg.BeginErrorReadLine();

            bool finished = ffmpeg.WaitForExit(300_000); // 5-minute timeout for merge

            if (!finished)
            {
                ffmpeg.Kill();
                Console.WriteLine("[RecordingScript] ERROR – FFmpeg merge timed out!");
                return;
            }

            if (File.Exists(outputPath) && new FileInfo(outputPath).Length > 0)
                Console.WriteLine($"[RecordingScript] ✅ Final video saved to: {outputPath}");
            else
                Console.WriteLine("[RecordingScript] ❌ FFmpeg merge failed – check logs above.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // ADB HELPERS
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>Prepends the -s &lt;serial&gt; flag when a device serial is known.</summary>
        private static string BuildAdbArgs(string shellCommand)
        {
            string serial = string.IsNullOrWhiteSpace(_deviceSerial)
                            ? string.Empty
                            : $"-s {_deviceSerial} ";
            return $"{serial}{shellCommand}";
        }

        private static void RunAdbCommand(string shellArgs)
        {
            using var p = StartAdbProcess(BuildAdbArgs(shellArgs));
            p.WaitForExit(20_000);
        }

        private static Process StartAdbProcess(string arguments)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = AdbPath,
                    Arguments = arguments,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            p.Start();
            return p;
        }

        private static string GetDeviceSerial(AndroidDriver driver)
        {
            try
            {
                var caps = driver.Capabilities;
                if (caps["udid"] is string udid && !string.IsNullOrWhiteSpace(udid))
                    return udid;
            }
            catch { /* fall through */ }

            // Fallback: first device listed by  adb devices
            string output = RunAdbCommandWithOutput("devices");
            foreach (string line in output.Split('\n'))
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed)
                    && !trimmed.StartsWith("List")
                    && trimmed.Contains("\t"))
                {
                    return trimmed.Split('\t')[0].Trim();
                }
            }

            return string.Empty; // works when only one device is connected
        }

        private static string RunAdbCommandWithOutput(string arguments)
        {
            using var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = AdbPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit(10_000);
            return output;
        }

        // ─────────────────────────────────────────────────────────────────────
        // CLEANUP
        // ─────────────────────────────────────────────────────────────────────

        private static void CleanupTempFolder()
        {
            try
            {
                if (Directory.Exists(_sessionFolder))
                    Directory.Delete(_sessionFolder, recursive: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RecordingScript] Cleanup warning: {ex.Message}");
            }
        }

        private static string SanitiseFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}