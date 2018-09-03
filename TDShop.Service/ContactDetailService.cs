using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDShop.Data.Infrastructure;
using TDShop.Data.Repositories;
using TDShop.Model.Models;

namespace TDShop.Service
{
    public interface IContactDetailService
    {
        ContactDetail GetDefaultContact();
    }
    public class ContactDetailService : IContactDetailService
    {
        public IContactDetailRepository _contactRepository;
        public IUnitofWork _unitofWork;
        public ContactDetailService(IContactDetailRepository contactRepository, IUnitofWork unitofWork)
        {
            this._contactRepository = contactRepository;
            this._unitofWork = unitofWork;
        }
        public ContactDetail GetDefaultContact()
        {
            return _contactRepository.GetSingleByCondition(x => x.Status);
        }
    }
}
