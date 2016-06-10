using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    [DataContract]
    public enum Rotation : byte
    {
        [EnumMember]
        None,
        [EnumMember]
        A,
        [EnumMember]
        B,
        [EnumMember]
        C,
    }

    public static class RotationExtensions
    {

    }
}
