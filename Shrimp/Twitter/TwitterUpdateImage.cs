
namespace Shrimp.Twitter
{
    public class TwitterUpdateImage
    {
        public string FileName { get; private set; }

        public byte[] Data { get; private set; }

        public string Status { get; private set; }

        public decimal InReplyToStatusId { get; private set; }

        public TwitterUpdateImage(string filename, byte[] data, string status, decimal inReplyToStatusId)
        {
            this.FileName = filename;
            this.Data = data;
            this.Status = status;
            this.InReplyToStatusId = inReplyToStatusId;
        }
    }
}
