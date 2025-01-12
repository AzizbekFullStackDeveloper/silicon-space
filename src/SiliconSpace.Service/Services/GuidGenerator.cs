using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.Service.Services
{
    public class GuidGenerator : IGuidGenerator
    {
        private Guid _guid;
        public GuidGenerator()
        {
            this._guid = Guid.NewGuid();
        }
        public Guid GenerateGuid()
        {
            return _guid;
        }
    }
}
