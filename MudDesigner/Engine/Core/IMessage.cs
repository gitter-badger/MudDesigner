﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.Core
{
    public interface IMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        string Message { get; set; }
    }
}