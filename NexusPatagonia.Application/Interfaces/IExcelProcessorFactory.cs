using NexusPatagonia.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Interfaces
{
    public interface IExcelProcessorFactory
    {
        IExcelStrategy GetProcessor(string docType);
    }
}
