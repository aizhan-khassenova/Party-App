using PartyAppHw.Models;
using System;
using System.Linq;
using System.Net.Mail;

namespace PartyAppHw.Services
{
    public class DeliveryInformationRequestsService : IDisposable
    {
        private bool disposedValue;
        private readonly PartyEntities partyEntities;

        public DeliveryInformationRequestsService(Models.PartyEntities partyEntities)
        {
            this.partyEntities = partyEntities;
        }

        public IQueryable<DeliveryRequest> GetAll()
        {
            return from e in partyEntities.Deliveries
                   select new DeliveryRequest
                   {
                       NotificationEmail = e.NotificationEmail,
                       Name = e.Name,
                       Telephone = e.Telephone,
                       CallOnIssue = e.CallOnIssue,
                       Id = e.Id
                   };
        }

        internal void Save(DeliveryRequest model)
        {
            Delivery partyEvent = partyEntities.Deliveries.Create();

            partyEvent.CreatedAt = DateTime.Now;
            partyEvent.Name = model.Name;
            partyEvent.NotificationEmail = model.NotificationEmail;
            partyEvent.Telephone = model.Telephone;
            partyEvent.CallOnIssue = model.CallOnIssue.GetValueOrDefault();

            partyEntities.Deliveries.Add(partyEvent);

            partyEntities.SaveChanges();
        }

        internal void Notify(DeliveryRequest model)
        {
            //var emailClient = new SmtpClient
            //{
            //    //DeliveryMethod = SmtpDeliveryMethod.Network,
            //    //DeliveryFormat = SmtpDeliveryFormat.International,
            //    EnableSsl = false,
            //    Host = "127.0.0.1",
            //    Port = 1025
            //};
            var emailClient = new SmtpClient();

            var fromAddress = new MailAddress("rvspinform@local.tlp", "Форма регистрации на вечеринку");
            var toAddress = new MailAddress("kate@rvsp.com", "Хозяйка вечеринки");

            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Регистрация на вечеринку",
                Body = $"Зарегистрировался {model.Name} ({model.NotificationEmail})" +
                $".\r\n Его телефон - {model.Telephone}"
            };


            if (model.CallOnIssue == true)
            {
                mailMessage.Body += "\r\n Он сообщил, что придет!";
            }
            else
            {
                mailMessage.Body += "\r\n Он сообщил, что не придет!";
            }

            emailClient.Send(mailMessage);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    partyEntities.Dispose(); //ДОБАВИЛИ
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }



        // // TODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
        // ~PartyEventsService()
        // {
        //     // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        internal void Delete(int id)
        {
            var entity = partyEntities.Deliveries.Find(id);

            if (entity != null)
            {
                partyEntities.Deliveries.Remove(entity);
                partyEntities.SaveChanges();
            }
        }
    }
}