
namespace Shrimp.Plugin.Ref
{
    /// <summary>
    /// Shrimpのバージョン
    /// </summary>
    public class ShrimpVersion
    {
        public readonly uint version;
        public readonly double version_str;

        public ShrimpVersion()
        {
            this.version = 120;
            this.version_str = 1.20;
        }
    }
}
