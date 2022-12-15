using ReportMicroservice.Database;
using ReportMicroservice.Dtos;
using ReportMicroservice.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReportMicroservice.Service
{
    public class HangfireService : IHangfireService
    {

        private readonly IHangfireRepo _repo;

        public HangfireService(IHangfireRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<RabbitMqMessage>> GetRequests()
        {
            return RabbitMQHelper.RabbitMQReceiveHelper.ReceiveQueuedMessages();
        }

        public async Task CreateExcelFiles(List<RabbitMqMessage> requests)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles");

            foreach (RabbitMqMessage item in requests)
            {
                ContactMicroserviceHelper contactMicroserviceHelper = new ContactMicroserviceHelper();
                List<PhoneBookItem> phoneBookItems = contactMicroserviceHelper.GetPhoneBookItems(item);
                ExcelCreator creator = new ExcelCreator(phoneBookItems);
                byte[] file = creator.GetFile();
                string excelPath = Path.Combine(path, item.Guid + ".xlsx");
                File.WriteAllBytes(excelPath, file);
                await _repo.UpdateStatus(item.Guid);
            }
        }
    }
}
