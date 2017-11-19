using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;

namespace webapi.Controllers
{
    public class ShipperController : ApiController
    {
        NORTHWNDEntities db = new NORTHWNDEntities();

        public HttpResponseMessage Get()
        {
            ICollection<Shippers> shippers = db.Shippers.ToList();

            if (shippers == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Aradığınız kayıtlar bulunamamıştır!!!");
            }

            return Request.CreateResponse(HttpStatusCode.Accepted, shippers);

        }

        public HttpResponseMessage Get(int ID)
        {
            Shippers shipper = db.Shippers.Where(x => x.ShipperID == ID).FirstOrDefault();

            if (shipper == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Aradığınız kayıt yok!!!");
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, shipper);
        }

        public HttpResponseMessage Post(Shippers ship)
        {
            db.Shippers.Add(ship);
            int EtkilenenSatirSayisi = db.SaveChanges();

            if (EtkilenenSatirSayisi > 0)
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Nesne oluşturulamadı");
        }
        public HttpResponseMessage Put(int ID,Shippers ship)
        {
            Shippers CurrentShip = db.Shippers.Where(x => x.ShipperID == ID).FirstOrDefault();

            CurrentShip.CompanyName = ship.CompanyName;
            CurrentShip.Phone = ship.Phone;

            int EtkilenenSatirSayisi = db.SaveChanges();

            if (EtkilenenSatirSayisi > 0)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Güncelleme işlemi sırasında hata oluştu");
        }

        public HttpResponseMessage Delete(int ID)
        {
            Shippers ship = db.Shippers.Where(x => x.ShipperID == ID).FirstOrDefault();
            db.Shippers.Remove(ship);
            int EtkilenenSatirSayisi = db.SaveChanges();

            if (EtkilenenSatirSayisi > 0)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Silme işlemi başarısız");
        }
    }
}
