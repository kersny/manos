using System;

namespace Manos.Spdy
{
	public class SettingsFrame : ControlFrame
	{
		public int UploadBandwidth {
			get
			{
				return _UploadBandwidth;
			}
			set
			{
				_UploadBandwidth = value;
				_UploadBandwidthChanged = true;
			}
		}
		public int DownloadBandwidth {
			get
			{
				return _DownloadBandwidth;
			}
			set
			{
				_DownloadBandwidth = value;
				_DownloadBandwidthChanged = true;
			}
		}
		public int RoundTripTime {
			get
			{
				return _RoundTripTime;
			}
			set
			{
				_RoundTripTime = value;
				_RoundTripTimeChanged = true;
			}
		}
		public int MaxConcurrentStreams {
			get
			{
				return _MaxConcurrentStreams;
			}
			set
			{
				_MaxConcurrentStreams = value;
				_MaxConcurrentStreamsChanged = true;
			}
		}
		public int CWND {
			get
			{
				return _CWND;
			}
			set
			{
				_CWND = value;
				_CWNDChanged = true;
			}
		}

		private int _UploadBandwidth;
		private int _DownloadBandwidth;
		private int _RoundTripTime;
		private int _MaxConcurrentStreams;
		private int _CWND;
		private bool _UploadBandwidthChanged;
		private bool _DownloadBandwidthChanged;
		private bool _RoundTripTimeChanged;
		private bool _MaxConcurrentStreamsChanged;
		private bool _CWNDChanged;

		public SettingsFrame ()
		{
		}
		public SettingsFrame(byte[] data, int offset, int length)
		{
			this.Type = ControlFrameType.SETTINGS;
			base.Parse(data, offset, length);
			int numentries = Util.BuildInt(data, offset + 8, 4);
			int index = offset + 12;
			for (int i = 0; i < numentries; i++)
			{
				byte IDFlags = data[index];
				index++;
				int ID = Util.BuildInt(data, index, 3);
				index += 3;
				int val = Util.BuildInt(data, index, 4);
				switch (ID)
				{
				case 1:
					this.UploadBandwidth = val;
					break;
				case 2:
					this.DownloadBandwidth = val;
					break;
				case 3:
					this.RoundTripTime = val;
					break;
				case 4:
					this.MaxConcurrentStreams = val;
					break;
				case 5:
					this.CWND = val;
					break;
				}
			}
		}
	}
}

