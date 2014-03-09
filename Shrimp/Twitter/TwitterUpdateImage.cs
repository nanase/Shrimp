using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Twitter
{
    public class TwitterUpdateImage
    {
        private readonly string _filename;
        private readonly byte[] _data;
        private readonly string _status;
        private readonly decimal _in_reply_to_status_id;

        public TwitterUpdateImage ( string filename, byte[] data, string status, decimal in_reply_to_status_id )
        {
            this._filename = filename;
            this._data = data;
            this._status = status;
            this._in_reply_to_status_id = in_reply_to_status_id;
        }

        public string filename
        {
            get { return this._filename; }
        }

        public byte[] data
        {
            get { return this._data; }
        }

        public string status
        {
            get { return this._status; }
        }

        public decimal in_reply_to_status_id
        {
            get { return this._in_reply_to_status_id; }
        }
    }
}
