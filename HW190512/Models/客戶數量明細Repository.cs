using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HW190512.Models
{   
	public  class 客戶數量明細Repository : EFRepository<客戶數量明細>, I客戶數量明細Repository
	{

	}

	public  interface I客戶數量明細Repository : IRepository<客戶數量明細>
	{

	}
}