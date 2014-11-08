using System.Web;

namespace TreeGecko.Library.Net.Handlers
{
    public abstract class AbstractHandler : IHttpHandler
    {
        protected AbstractHandler()
        {
            IsReusable = false;
        }

        public void ProcessRequest(HttpContext _context)
        {
            if (Validate(_context))
            {
                string method = _context.Request.HttpMethod.ToLower();

                switch (method)
                {
                    case "get":
                        HandleGet(_context);
                        break;
                    case "post":
                        HandlePost(_context);
                        break;
                    case "delete":
                        HandleDelete(_context);
                        break;
                }
            }
        }

        public bool IsReusable { get; private set; }

        public virtual bool Validate(HttpContext _context)
        {
            return true;
        }

        public virtual void HandleGet(HttpContext _context)
        {
            
        }

        public virtual void HandlePost(HttpContext _context)
        {

        }

        public virtual void HandleDelete(HttpContext _context)
        {

        }
    }
}
