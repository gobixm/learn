using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTraveller
{
    public interface IFinancialDataAquirer
    {
        Task<ICollection<Index>> GetHistoricalData(string symbol, DateTime startDate, DateTime endDate);
    }
}
