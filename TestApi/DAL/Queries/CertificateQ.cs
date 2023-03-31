using Microsoft.EntityFrameworkCore;
using TestApi.DAL.Response;
using TestApi.Models;

namespace TestApi.DAL.Queries
{
    public class CertificateQ
    {
        private readonly KRU_MIS_Context _context = new KRU_MIS_Context();

        public async Task<ResponsePagination> GetCertificate(int pageSize, int currentPage, string search)
        {
            var certificate = await _context.Certificate.Where(p => p.CertificateCode.Contains(search)).OrderBy(p => p.CertificateCode).ToListAsync();

            //var certificate = await _context.Certificate.Include(c => c.Project).Include(p => p.Registration)Where(p => p.CertificateCode.Contains(search)).OrderBy(p => p.CertificateCode).ToListAsync();


            int totalRow = 0;
            totalRow = certificate.Count();
            var totalPage = (double)totalRow / pageSize;
            var value = certificate.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            totalPage = (int)Math.Ceiling(totalPage);

            return new ResponsePagination
            {
                StatusCode = 200,
                TaskStatus = true,
                Message = "Success",
                Pagin = new
                {
                    Currentpage = currentPage,
                    Pagesize = pageSize,
                    Totalrows = totalRow,
                    Totalpages = totalPage
                },
                Data = certificate
            };
        }

        public async Task<ResponseData> GetPrintCertificate(int id)
        {
            var cert = await _context.Certificate.Where(a => a.Id == id).ToListAsync();
            var query = (from c in cert
                         join project in _context.Project on c.ProjectCode equals project.Code
                         join user in _context.Registration on c.UserCode equals user.Id
                         select new
                         {
                             CertificateCode = c.CertificateCode,
                             CertificateData = c.CertificateDate,
                             ProjectCode = c.ProjectCode,
                             ProjectName = project.ProjectName,
                             SignName = c.SignName,
                             Name = user.Name,
                             Lastname = user.Lastname,

                         }).ToList();
            return new ResponseData() { StatusCode = 200, TaskStatus = true, Message = "Success",Data = query};
        }


        public async Task<string> GenCertificate(string projectCode)
        {
            DateTime dateTime = DateTime.Now;
            string year = (dateTime.Year + 543).ToString();

            var data = await _context.Certificate.Where(a => a.ProjectCode == projectCode).OrderByDescending(a => a.CertificateCode).FirstOrDefaultAsync();

            if(data == null)
            {
                return year + projectCode + "000001";
            }

            var  lateID = data.CertificateCode.Substring(6);
            var autorun = (Convert.ToInt32(lateID) + 1).ToString("D6");

            return year + projectCode + "000001";
        }



        public async Task<ResponseMessages> CreateCertificate(Certificate certificateDT, string projectCode)
        {
            string certificateCode = await GenCertificate(projectCode);
            _context.Add(new Certificate()
            {
                CertificateCode = certificateCode,
                UserCode = certificateDT.UserCode,
                Status = "1",
                CertificateDate = DateTime.Now,
                ProjectCode = certificateDT.ProjectCode,
                SignName = certificateDT.SignName,
            });
            await _context.SaveChangesAsync();
            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }




        public async Task<ResponseMessages> EditCertificate(int id, Certificate certificateDT)
        {
            Certificate data = await _context.Certificate.FirstOrDefaultAsync(a => a.Id == id);
            if (data == null) return new ResponseMessages() { StatusCode = 200, TaskStatus = false, Message = "Code Not Found" };

            data.CertificateCode = certificateDT.CertificateCode;
            data.UserCode = certificateDT.UserCode;
            data.Status = "1";
            data.CertificateDate = DateTime.Now;
            data.SignName = certificateDT.SignName;

            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }

        public async Task<ResponseMessages> DeleteCertificate(int id)
        {
            Certificate data = await _context.Certificate.FirstOrDefaultAsync(p => p.Id == id);
            if (data == null)
                return new ResponseMessages() { StatusCode = 400, TaskStatus = false, Message = "Code Not Found" };
            data.Status = "0";

            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }
    }
}
