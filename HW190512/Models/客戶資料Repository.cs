using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HW190512.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(a => !a.是否已刪除);
        }
        public IQueryable<客戶資料> Get關鍵字(string Searchstring)
        {
            return this.All().Where(p => p.客戶名稱.Contains(Searchstring));
        }
        public void Delete(int id)
        {
            var customer = Find(id);
            if (customer != null)
            {
                customer.是否已刪除 = true;

                var repoContact = RepositoryHelper.Get客戶聯絡人Repository(UnitOfWork);
                repoContact.DeleteByDeleteClient(customer.客戶聯絡人);

                var repoBank = RepositoryHelper.Get客戶銀行資訊Repository(UnitOfWork);
                repoBank.DeleteByDeleteClient(customer.客戶銀行資訊);

                UnitOfWork.Commit();
            }
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}