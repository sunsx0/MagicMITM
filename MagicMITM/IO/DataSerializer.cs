using MagicMITM.Net;

namespace MagicMITM.IO
{
    public abstract class DataSerializer
    {
        public virtual DataStream Deserialize(DataStream ds)
        {
            return ds;
        }
        public virtual DataStream Serialize(DataStream ds)
        {
            return ds;
        }
    }
}
