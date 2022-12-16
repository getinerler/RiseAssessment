using Hangfire;
using Hangfire.Server;
using ReportMicroservice.Dtos;
using ReportMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportMicroservice.Hangfire
{
    public class GetReportRequestsJob
    {
        private HangfireLogHelper _hangfireLog;
        private readonly IHangfireService _hangfireService;

        public GetReportRequestsJob(IHangfireService hangfireService)
        {
            _hangfireService = hangfireService;
        }

        //Hangfire başarısız olan işlemi kuyruğa tekrar almasın
        [AutomaticRetry(Attempts = 0)]
        //Kuyruktaki işlem bitene kadar yeni işlem başlamasın.
        [DisableConcurrentExecution(timeoutInSeconds: 3600)]
        public async Task Run(IJobCancellationToken token, PerformContext performContext)
        {
            token.ThrowIfCancellationRequested();

            _hangfireLog = new HangfireLogHelper(performContext);

            _hangfireLog.InfoLog($"Zamanlanmış görev başlıyor...");

            try
            {
                List<RabbitMqMessage> requests = await _hangfireService.GetRequests();
                _hangfireLog.InfoLog($"Talepler çekildi.");

                await _hangfireService.CreateExcelFiles(requests);
                _hangfireLog.InfoLog($"Zamanlanmış görev başarıyla sonuçlandı.");
            }
            catch (Exception ex)
            {
                _hangfireLog.ErrorLog($"Hata: " + ex.Message);
            }
        }
    }
}
