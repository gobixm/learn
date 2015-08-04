﻿using System;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Messages;

namespace Infotecs.Attika.AttikaInfrastructure.Services.Contracts
{
    public interface IMessageSerializationService
    {
        byte[] Serialize(BaseMessage message);
        BaseMessage Deseriallize(byte[] message, Func<string, Type> messageTypeByNameFunc);
    }
}