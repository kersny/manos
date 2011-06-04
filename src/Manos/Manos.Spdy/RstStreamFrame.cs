using System;

namespace Manos.Spdy
{
	public class RstStreamFrame : ControlFrame
	{
		public int StreamID { get; set; }
		public RstStreamStatusCode StatusCode { get; set; }
		public RstStreamFrame ()
		{
		}
		public RstStreamFrame(byte[] data, int offset, int length)
		{
			this.Type = ControlFrameType.RST_STREAM;
			base.Parse(data, offset, length);
			this.StreamID = Util.BuildInt(data, offset + 8, 4);
			this.StatusCode = (RstStreamStatusCode)Util.BuildInt(data, offset + 12, 4);
		}
	}
	public enum RstStreamStatusCode
	{
		PROTOCOL_ERROR = 1,
		INVALID_STREAM = 2,
		REFUSED_STREAM = 3,
		UNSUPPORTED_VERSION = 4,
		CANCEL = 5,
		FLOW_CONTROL_ERROR = 6,
		// the two below are both listed as 6 in the spec...
		STREAM_IN_USE = 7,
		STREAM_ALREADY_CLOSED = 8
	}
}

