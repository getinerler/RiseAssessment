using System;
using System.Threading.Tasks;

namespace ReportMicroservice.Database
{
    public interface IHangfireRepo
    {
        Task UpdateStatusAndPath(Guid guid);
    }
}
