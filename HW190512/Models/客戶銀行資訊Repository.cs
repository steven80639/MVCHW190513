using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace HW190512.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Include(客 => 客.客戶資料).Where(a=>!a.是否已刪除).OrderBy(s => s.客戶Id);
        }
        public IQueryable<客戶銀行資訊> Get關鍵字(string Searchstring)
        {
            return this.All().Where(s => s.客戶資料.客戶名稱.Contains(Searchstring)).OrderBy(s => s.客戶Id);
        }
        public void Delete(int id)
        {
            var data = Find(id);
            if (data != null)
            {
                data.是否已刪除 = true;
                UnitOfWork.Commit();
            }
        }
        public void DeleteByDeleteClient(IEnumerable<客戶銀行資訊> banks)
        {
            foreach (var bank in banks.ToList())
            {
                Delete(bank.Id);
            }
        }

    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}