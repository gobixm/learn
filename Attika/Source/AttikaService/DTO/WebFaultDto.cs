﻿using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaService.DTO
{
    [DataContract]
    public class WebFaultDto
    {
        public WebFaultDto(string message, string detail = "")
        {
            Message = message;
            Detail = detail;
        }

        [DataMember]
        public string Message { get; private set; }

        [DataMember]
        public string Detail { get; private set; }
    }
}