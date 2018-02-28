using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenrose.Data.Service
{
    public class BaseService: IDisposable
    {
        protected EdenroseEntities _context;
        public BaseService()
        {
            _context = new EdenroseEntities();
        }
        // Tất cả repository cần Disposable - important
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_context != null)
                {
                    _context.Dispose();
                }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
       
    }
}
