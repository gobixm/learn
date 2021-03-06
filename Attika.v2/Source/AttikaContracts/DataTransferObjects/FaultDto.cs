﻿using System;
using System.Runtime.Serialization;

namespace AttikaContracts.DataTransferObjects
{
    [DataContract]
    public class FaultDto
    {
        public FaultDto(string message, string detail)
        {
            Message = message;
            Detail = detail;
        }

        [DataMember]
        public string Detail { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
