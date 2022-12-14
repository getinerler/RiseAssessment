using ReportMicroservice.Database;
using ReportMicroservice.Dtos;
using ReportMicroservice.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReportMicroservice.Service
{
    public class HangfireService
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
            string path = Directory.GetCurrentDirectory() + "/ExcelFiles/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (RabbitMqMessage item in requests)
            {
                ContactMicroserviceHelper contactMicroserviceHelper = new ContactMicroserviceHelper();
                List<PhoneBookItem> phoneBookItems = contactMicroserviceHelper.GetPhoneBookItems(item);
                ExcelCreator creator = new ExcelCreator(phoneBookItems);
                byte[] file = creator.GetFile();
                File.WriteAllBytes(path + item.Guid + ".xlsx", file);
                _repo.UpdateStatusAndPath(item.Guid);
            }
        }
    }
}
