using ProtoBuf;

namespace Gobi.KafkaDumper
{
    [ProtoContract]
    public sealed class MessageDto
    {
        [ProtoMember(1)]
        public string Key { get; set; }

        [ProtoMember(2)]
        public byte[] Value { get; set; }
    }
}