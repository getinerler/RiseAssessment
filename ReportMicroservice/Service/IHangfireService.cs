using ReportMicroservice.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportMicroservice.Service
{
    public interface IHangfireService
    {
        Task<List<RabbitMqMessage>> GetRequests();
        Task CreateExcelFiles(List<RabbitMqMessage> requests);
    }
}
