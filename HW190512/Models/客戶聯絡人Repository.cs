using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace HW190512.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Include(客 => 客.客戶資料).Where(a => !a.是否已刪除).OrderBy(s => s.客戶Id);
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
        public void DeleteByDeleteClient(IEnumerable<客戶聯絡人> contacts)
        {
            foreach (var contact in contacts.ToList())
            {
                Delete(contact.Id);
            }
        }
        public IQueryable<客戶聯絡人> Get關鍵字(string Searchstring)
        {
            return this.All().Where(s =>s.職稱.Contains(Searchstring)).OrderBy(s => s.客戶Id);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}